using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;

namespace DeconstructClassic
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Menu_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (sender is Border)
                BeginMoveDrag(e);
        }

        private void MenuItem_Click(object? sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender!;
            switch (item.Name)
            {
                case "OpenFile":
                    var file = TopLevel.GetTopLevel(this).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
                    {
                        Title = "Open File",
                        AllowMultiple = false
                    }).Result;

                    if (file.Count >= 1)
                    {
                        //FileTree.LoadFromFile(file[0].Path.LocalPath);
                    }
                    break;
            }
        }
    }
}