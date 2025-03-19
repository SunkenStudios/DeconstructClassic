using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using DeconstructClassic.ConstructData.AppBlock;
using DeconstructClassic.ConstructData.ImageBlock;
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

        private bool TryReadResource(PortableExecutable executable, ResourceCodes code, int id, out Resource? output)
        {
            output = executable.TryGetResource(new ResourceIdentifier(
                     ResourceType.FromString(code.ToString()),
                     ResourceName.FromCode(id),
                     new Language(0)
            ));
            return output != null;
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

                        ImageBank imageBank = new ImageBank(new ByteReader(ReadResource(executable, ResourceCodes.IMAGEBLOCK)));
                        TreeViewItem imageItem = new TreeViewItem();
                        imageItem.Header = "Images";
                        foreach(ImageEntry imageData in imageBank.Images)
                        {
                            TreeViewItem image = new TreeViewItem();
                            image.Header = "image" + imageData.ID.ToString("D4");
                            image.Tag = imageData;
                            imageItem.Items.Add(image);
                        }
                        appItem.Items.Add(imageItem);

                        TreeViewItem soundItem = new TreeViewItem();
                        soundItem.Header = "Sounds";
                        for (int i = (int)ResourceCodes.FILES; ; i++)
                        {
                            if (!TryReadResource(executable, ResourceCodes.FILES, i, out Resource? resource))
                                break;
                            ByteReader reader = new ByteReader(resource!.Data);
                            string header = reader.ReadAscii(4);
                            reader.Seek(0);
                            if (header == "RIFF") // wave file
                            {
                                BinaryFile sndFile = new BinaryFile(reader,BinaryFile.FileType.WAVE);
                                TreeViewItem soundFileItem = new TreeViewItem();
                                soundFileItem.Header = "sound" + i.ToString("D4");
                                soundFileItem.Tag = sndFile;
                                soundItem.Items.Add(soundFileItem);
                            }

                        }
                        appItem.Items.Add(soundItem);

                    }
                    break;
            }
        }
    }
}