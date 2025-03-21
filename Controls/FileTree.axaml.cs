using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using DeconstructClassic.ConstructData.AppBlock;
using DeconstructClassic.ConstructData.ImageBlock;
using DeconstructClassic.ConstructData.LevelBlock;
using DeconstructClassic.Memory;
using System;
using System.IO;
using System.Text;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Control = Avalonia.Controls.Control;
using Image = Avalonia.Controls.Image;

namespace DeconstructClassic;

public partial class FileTree : UserControl {
    public FileTree() {
        InitializeComponent();
    }


    public TreeViewItem MakeLabelIcon(string text, byte[] iconData) {
        TreeViewItem item = new TreeViewItem();
        item.IsExpanded = false;

        StackPanel stack = new StackPanel();
        stack.Orientation = Orientation.Horizontal;


        BinaryWriter writer = new BinaryWriter(new MemoryStream(), Encoding.ASCII, true);
        writer.Write((short)0);
        writer.Write((short)1);
        writer.Write((short)1);
        writer.Write((byte)32);
        writer.Write((byte)64);
        writer.Write((short)0);
        writer.Write((short)1);
        writer.Write((short)32);
        writer.Write((int)iconData.Length);
        writer.Write((int)writer.BaseStream.Position + 4);
        writer.Write(iconData);

        MemoryStream memoryStream = (MemoryStream)writer.BaseStream;
        writer.Close();
        memoryStream.Position = 0;

        Image image = new Image();
        image.Source = new Bitmap(memoryStream);
        image.Width = 16;
        image.Height = 16;
        image.Margin = new Thickness(0, 0, 4, 0);
        Label lbl = new Label();
        lbl.Content = text;

        stack.Children.Add(image);
        stack.Children.Add(lbl);

        item.Header = stack;
        return item;
    }

    private void TreeView_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e) {
        if (sender is TreeView treeView) {
            if (treeView.SelectedItem is TreeViewItem item) {
                if (MainWindow.Instance.ContentPanel.Child is IDisposable disposable) {
                    disposable.Dispose();
                }

                if (item.Tag is AppWrapper app) {
                    MainWindow.Instance.ContentPanel.Child = new AppDataViewer(app.AppData);
                }
                else if (item.Tag is GlobalVariable[] globalVars) {
                    MainWindow.Instance.ContentPanel.Child = new GlobalValueViewer(globalVars);
                }
                else if (item.Tag is ImageEntry imageEntry) {
                    MainWindow.Instance.ContentPanel.Child = new PhotoViewer(imageEntry.Data);
                }
                else if (item.Tag is BinaryFile binaryFile) {
                    if (binaryFile.Type == BinaryFile.FileType.WAVE) {
                        MainWindow.Instance.ContentPanel.Child = new AudioPlayer(binaryFile);
                    }
                    else {
                        MainWindow.Instance.ContentPanel.Child = null;
                    }
                }
                else if (item.Tag is LayoutEntry layoutEntry) {
                    MainWindow.Instance.ContentPanel.Child = new LevelPreviewer(layoutEntry, MainWindow.GetControlApp(item));
                }
                else {
                    MainWindow.Instance.ContentPanel.Child = null;
                }
            }
        }
    }
}