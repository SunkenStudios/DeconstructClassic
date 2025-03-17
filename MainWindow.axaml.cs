using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Ressy;
using System.Diagnostics;

namespace DeconstructClassic
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public enum ResourceCodes : int
        {
            FILES = 1,
            PYTHONLIBS = 992,
            MENUBLOCK = 993,
            HLSL = 994,
            IMAGEBLOCK = 995,
            APPBLOCK = 997,
            LEVELBLOCK = 998,
            EVENTBLOCK = 999,
            DLLBLOCK = 1000
        }

        private void ReadResources(PortableExecutable executable,ResourceCodes code)
        {
            var resBlock = executable.GetResource(new ResourceIdentifier(
                 ResourceType.FromString(code.ToString()),
                 ResourceName.FromCode((int)code),
                 new Language(0)
            ));
            Debug.WriteLine(resBlock.Data.Length);
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
                        Debug.WriteLine(file[0].Path.LocalPath);
                        ReadResources(new PortableExecutable(file[0].Path.LocalPath),ResourceCodes.FILES);
                    }
                    break;
            }
        }
    }
}