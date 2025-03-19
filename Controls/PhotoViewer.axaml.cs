using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System;
using System.IO;

namespace DeconstructClassic;

public partial class PhotoViewer : UserControl, IDisposable {
    public PhotoViewer() : this(null) { }

    public PhotoViewer(byte[]? fileData) {
        InitializeComponent();

        if (fileData is null) {
            return;
        }

        ViewerImage.Source = new Bitmap(new MemoryStream(fileData));
    }



    public void Dispose() {
        ((Bitmap?)ViewerImage.Source)?.Dispose();
    }
}