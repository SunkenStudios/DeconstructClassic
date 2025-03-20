using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using DeconstructClassic.Memory;
using NAudio.Wave;
using System;
using System.Threading.Tasks;

namespace DeconstructClassic;

public partial class AudioPlayer : UserControl, IDisposable {
    public WaveOutEvent SongOut = null!;
    public RawSourceWaveStream SongReader = null!;
    public DispatcherTimer TickTimer = null!;
    private bool _movingCursor = false;

    public AudioPlayer() : this(null) { }
    public AudioPlayer(BinaryFile? fileData) {
        InitializeComponent();
        if (fileData == null || fileData.Type != BinaryFile.FileType.WAVE) {
            return;
        }

        LoadSong(fileData).ContinueWith((t) => {
            TickTimer = new DispatcherTimer(DispatcherPriority.MaxValue);
            TickTimer.Interval = TimeSpan.FromSeconds(0.05);
            TickTimer.Tick += Tick;
            TickTimer.Start();
        });
    }

    private void Tick(object? sender, EventArgs e) {
        if (SongOut is null || SongReader is null || TickTimer is null) {
            return;
        }

        // Controls
        string pauseValue = SongOut?.PlaybackState == PlaybackState.Playing ? "fa-pause" : "fa-play";
        if (PauseBtn.Value != pauseValue) {
            PauseBtn.Value = pauseValue;
        }

        // Progress
        if (!_movingCursor) {
            SongProgress.Value = SongReader.CurrentTime.TotalSeconds;
            ProgressCurTime.Text = SongReader.CurrentTime.Minutes + ":" + SongReader.CurrentTime.Seconds.ToString("D2");
            ProgressEndTime.Text = SongReader.TotalTime.Minutes + ":" + SongReader.TotalTime.Seconds.ToString("D2");
        }
    }

    public Task LoadSong(BinaryFile fileData) {

        SongName.Text = string.Empty;

        ByteReader reader = fileData.Data;
        reader.Seek(4); // Header
        int dataSize = reader.ReadInt();
        reader.Seek(22);
        int channels = reader.ReadUShort();
        int sampleRate = reader.ReadInt();
        reader.Skip(6);
        int bitsPSample = reader.ReadUShort();
        reader.Seek(0);

        SongReader = new RawSourceWaveStream(reader.BaseStream, new WaveFormat(sampleRate, bitsPSample, channels));
        SongOut = new WaveOutEvent();
        SongOut.Init(SongReader); // Ready for playback, but not playing yet

        SongProgress.Maximum = (dataSize / (channels * (bitsPSample / 8.0))) / sampleRate;
        return Task.CompletedTask;
    }

    private void PauseBtnPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e) {
        if (SongOut.PlaybackState == PlaybackState.Playing) {
            SongOut.Pause();
        }
        else if (SongOut.PlaybackState != PlaybackState.Playing) {
            if (SongOut.PlaybackState == PlaybackState.Stopped) {
                SongReader.CurrentTime = TimeSpan.Zero;
            }
            SongOut.Play();
        }
    }

    private void ProgressPressed(object? sender, PointerPressedEventArgs e) {
        _movingCursor = true;
        UpdateProgress(e.GetPosition(ProgressHit));
    }

    private void ProgressMoved(object? sender, PointerEventArgs e) {
        _movingCursor = e.GetCurrentPoint(sender as Control).Properties.IsLeftButtonPressed;
        if (_movingCursor) {
            UpdateProgress(e.GetPosition(ProgressHit));
        }
    }

    private void ProgressReleased(object? sender, PointerReleasedEventArgs e) {
        _movingCursor = false;
        UpdateProgress(e.GetPosition(ProgressHit), true);
    }

    private void UpdateProgress(Point pointerPos, bool setProgress = false) {
        double progress = Math.Clamp(pointerPos.X / ProgressHit.Bounds.Width, 0, 1);
        TimeSpan progressTime = SongReader.TotalTime * progress;
        SongProgress.Value = progressTime.TotalSeconds;
        ProgressCurTime.Text = progressTime.Minutes + ":" + progressTime.Seconds.ToString("D2");
        if (setProgress) {
            SongReader.CurrentTime = progressTime;
        }
    }

    public void Dispose() {
        SongOut?.Stop();
        SongOut?.Dispose();
        SongReader?.Dispose();
        TickTimer.Stop();
    }

    private string CapitalizeName(string name) {
        string[] strings = name.Split('_');
        for (int i = 0; i < strings.Length; i++) {
            strings[i] = strings[i][..1].ToUpper() + strings[i][1..];
        }

        return string.Join(' ', strings);
    }
}