using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using DeconstructClassic.ConstructData;
using DeconstructClassic.Memory;
using Ressy;
using System;
using System.Diagnostics;

namespace DeconstructClassic
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance = null!;

        public MainWindow()
        {
            Instance = this;
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

        private byte[] ReadResource(PortableExecutable executable,ResourceCodes code)
        {
            var resBlock = executable.GetResource(new ResourceIdentifier(
                 ResourceType.FromString(code.ToString()),
                 ResourceName.FromCode((int)code),
                 new Language(0)
            ));
            return resBlock.Data;
        }

        private byte[] GetIcon(PortableExecutable executable)
        {
            var resBlock = executable.GetResource(new ResourceIdentifier(
                 ResourceType.Icon,
                 ResourceName.FromCode(1),
                 new Language(1033)
            ));
            
            return resBlock.Data;
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
                        PortableExecutable executable = new PortableExecutable(file[0].Path.LocalPath);
                        AppData appData = new AppData(new ByteReader(ReadResource(executable, ResourceCodes.APPBLOCK)));
                        TreeViewItem appItem = FileTree.MakeLabelIcon(appData.AppName, GetIcon(executable));
                        appItem.Tag = appData;
                        FileTree.FileTreeView.Items.Add(appItem);

                        if (appData.GlobalVariables.Length > 0)
                        {
                            TreeViewItem globalVars = new TreeViewItem();
                            globalVars.Header = "Global Variables";
                            globalVars.Tag = appData.GlobalVariables;
                            appItem.Items.Add(globalVars);
                        }

                    }
                    break;
            }
        }
    }
}