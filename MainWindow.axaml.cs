using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using DeconstructClassic.ConstructData.AppBlock;
using DeconstructClassic.ConstructData.ImageBlock;
using DeconstructClassic.ConstructData.LevelBlock;
using DeconstructClassic.Memory;
using Ressy;
using System.Diagnostics;
using System.IO;
using Control = Avalonia.Controls.Control;

namespace DeconstructClassic {
    public partial class MainWindow : Window {
        public static MainWindow Instance = null!;

        public MainWindow() {
            Instance = this;
            InitializeComponent();
        }

        public enum ResourceCodes : int {
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

        private byte[] ReadResource(PortableExecutable executable, ResourceCodes code) {
            var resBlock = executable.GetResource(new ResourceIdentifier(
                 ResourceType.FromString(code.ToString()),
                 ResourceName.FromCode((int)code),
                 new Language(0)
            ));
            return resBlock.Data;
        }

        private bool TryReadResource(PortableExecutable executable, ResourceCodes code, out Resource output) {
            output = executable.TryGetResource(new ResourceIdentifier(
                     ResourceType.FromString(code.ToString()),
                     ResourceName.FromCode((int)code),
                     new Language(0)
            ))!;
            return output != null;
        }

        private bool TryReadResource(PortableExecutable executable, ResourceCodes code, int id, out Resource output) {
            output = executable.TryGetResource(new ResourceIdentifier(
                     ResourceType.FromString(code.ToString()),
                     ResourceName.FromCode(id),
                     new Language(0)
            ))!;
            return output != null;
        }

        private byte[] GetIcon(PortableExecutable executable) {
            var resBlock = executable.GetResource(new ResourceIdentifier(
                 ResourceType.Icon,
                 ResourceName.FromCode(1),
                 new Language(1033)
            ));

            return resBlock.Data;
        }

        private void Menu_PointerPressed(object? sender, PointerPressedEventArgs e) {
            if (sender is Border) {
                BeginMoveDrag(e);
            }
        }

        private void MenuItem_Click(object? sender, RoutedEventArgs e) {
            MenuItem item = (MenuItem)sender!;
            switch (item.Name) {
                case "OpenFile":
                    var file = TopLevel.GetTopLevel(this).StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions {
                        Title = "Open File",
                        AllowMultiple = false
                    }).Result;

                    if (file.Count >= 1) {
                        PortableExecutable executable = new PortableExecutable(file[0].Path.LocalPath);
                        AppWrapper app = new AppWrapper();
                        TreeViewItem appItem = ReadAppData(executable, app);
                        ReadGlobalVariables(executable, appItem);
                        ReadImageBank(executable, appItem, app);
                        ReadSounds(executable, appItem, app);
                        ReadLayouts(executable, appItem, app);
                        ReadDLLs(executable, app);
                    }
                    break;
            }
        }

        private TreeViewItem ReadAppData(PortableExecutable PE, AppWrapper app) {
            if (TryReadResource(PE, ResourceCodes.APPBLOCK, out Resource AppResource)) {
                AppData appData = new AppData(new ByteReader(AppResource.Data));
                TreeViewItem appItem = FileTree.MakeLabelIcon(appData.AppName, GetIcon(PE));
                appItem.Tag = app;
                app.AppData = appData;
                FileTree.FileTreeView.Items.Add(appItem);
                return appItem;
            }
            else {
                TreeViewItem appItem = FileTree.MakeLabelIcon(Path.GetFileNameWithoutExtension(PE.FilePath), GetIcon(PE));
                FileTree.FileTreeView.Items.Add(appItem);
                return appItem;
            }
        }

        private TreeViewItem ReadGlobalVariables(PortableExecutable PE, TreeViewItem appItem) {
            if (TryGetControlApp(appItem, out AppWrapper? app) && app?.AppData != null) {
                TreeViewItem globalVars = new TreeViewItem();
                globalVars.Header = $"Global Variables ({app.AppData.GlobalVariables.Length})";
                globalVars.Tag = app.AppData.GlobalVariables;
                appItem.Items.Add(globalVars);
                return globalVars;
            }
            else {
                TreeViewItem globalVars = new TreeViewItem();
                globalVars.Header = "Global Variables (0)";
                appItem.Items.Add(globalVars);
                return globalVars;
            }
        }

        private TreeViewItem ReadImageBank(PortableExecutable PE, TreeViewItem appItem, AppWrapper app) {
            if (TryReadResource(PE, ResourceCodes.IMAGEBLOCK, out Resource AppResource)) {
                ImageBank imageBank = new ImageBank(new ByteReader(AppResource.Data));
                TreeViewItem imageItem = new TreeViewItem();
                imageItem.Header = $"Images ({imageBank.Images.Length})";
                foreach (ImageEntry imageData in imageBank.Images) {
                    TreeViewItem image = new TreeViewItem();
                    image.Header = "image" + imageData.ID.ToString("D4");
                    image.Tag = imageData;
                    imageItem.Items.Add(image);
                }
                app.ImageBank = imageBank;
                appItem.Items.Add(imageItem);
                return imageItem;
            }
            else {
                TreeViewItem imageItem = new TreeViewItem();
                imageItem.Header = "Images (0)";
                appItem.Items.Add(imageItem);
                return imageItem;
            }
        }

        private TreeViewItem ReadSounds(PortableExecutable PE, TreeViewItem appItem, AppWrapper app) {
            TreeViewItem soundItem = new TreeViewItem();
            int soundCnt = 0;
            for (int i = (int)ResourceCodes.FILES; ; i++) {
                if (!TryReadResource(PE, ResourceCodes.FILES, i, out Resource resource)) {
                    break;
                }

                ByteReader reader = new ByteReader(resource!.Data);
                string header = reader.ReadAscii(4);
                reader.Seek(0);
                if (header == "RIFF") // wave file
                {
                    BinaryFile sndFile = new BinaryFile(reader, BinaryFile.FileType.WAVE);
                    TreeViewItem soundFileItem = new TreeViewItem();
                    soundFileItem.Header = "sound" + i.ToString("D4");
                    soundFileItem.Tag = sndFile;
                    app.BinaryFiles.Add(sndFile);
                    soundItem.Items.Add(soundFileItem);
                }

                soundCnt++;
            }
            soundItem.Header = $"Sounds ({soundCnt})";
            appItem.Items.Add(soundItem);
            return soundItem;
        }

        private TreeViewItem ReadLayouts(PortableExecutable PE, TreeViewItem appItem, AppWrapper app) {
            if (TryReadResource(PE, ResourceCodes.LEVELBLOCK, out Resource AppResource)) {
                LayoutBank layoutBank = new LayoutBank(new ByteReader(AppResource.Data));
                TreeViewItem layouts = new TreeViewItem();
                layouts.Header = $"Layouts ({layoutBank.Layouts.Length})";
                layouts.Tag = layoutBank;
                foreach (LayoutEntry layoutData in layoutBank.Layouts) {
                    TreeViewItem image = new TreeViewItem();
                    image.Header = layoutData.Name;
                    image.Tag = layoutData;
                    layouts.Items.Add(image);
                }
                app.LayoutBank = layoutBank;
                appItem.Items.Add(layouts);
                return layouts;
            }
            else {
                TreeViewItem layouts = new TreeViewItem();
                layouts.Header = "Layouts (0)";
                appItem.Items.Add(layouts);
                return layouts;
            }
        }

        private void ReadDLLs(PortableExecutable PE, AppWrapper app) {
            for (int i = (int)ResourceCodes.DLLBLOCK; ; i++) {
                if (!TryReadResource(PE, ResourceCodes.DLLBLOCK, i, out Resource resource)) {
                    break;
                }

                string tempFilePath = Path.GetTempFileName();
                File.WriteAllBytes(tempFilePath, resource.Data);
                PortableExecutable dllPE = new PortableExecutable(tempFilePath);
                DLLFileInfo dllInfo = new DLLFileInfo(dllPE);
                app.DLLFileInfos.Add(dllInfo);
                File.Delete(tempFilePath);
            }
        }

        public static AppWrapper GetControlApp(Control subControl) {
            Control? control = subControl;
            while (control != null) {
                if (control.Tag is AppWrapper app) {
                    return app;
                }
                control = control.Parent is Control nextControl ? nextControl : null;
            }
            return null!;
        }

        public static bool TryGetControlApp(Control subControl, out AppWrapper? app) {
            Control? control = subControl;
            while (control != null) {
                if (control.Tag is AppWrapper appInst) {
                    app = appInst;
                    return true;
                }
                control = control.Parent is Control nextControl ? nextControl : null;
            }
            app = null;
            return false;
        }
    }
}