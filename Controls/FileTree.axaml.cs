using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media.Imaging;
using DeconstructClassic.ConstructData;
using DeconstructClassic.Memory;
using SixLabors.ImageSharp;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Image = Avalonia.Controls.Image;
using ISImage = SixLabors.ImageSharp.Image;
using SDBitmap = System.Drawing.Bitmap;

namespace DeconstructClassic;

public partial class FileTree : UserControl
{
    public FileTree()
    {
        InitializeComponent();
    }


    public TreeViewItem MakeLabelIcon(string text, byte[] iconData)
    {
        TreeViewItem item = new TreeViewItem();
        item.IsExpanded = false;

        StackPanel stack = new StackPanel();
        stack.Orientation = Orientation.Horizontal;


        BinaryWriter writer = new BinaryWriter(new MemoryStream(),Encoding.ASCII,true);
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

        //ISImage img = ISImage.Load(writer.BaseStream);
        //img.SaveAsPng(memoryStream);
        //img.Dispose();

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

    private void TreeView_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        if (sender is TreeView treeView)
        {
            if (treeView.SelectedItem is TreeViewItem item)
            {
                if (item.Tag is AppData appData)
                {
                    MainWindow.Instance.ContentPanel.Child = new AppDataViewer(appData);
                } else
                {
                    MainWindow.Instance.ContentPanel.Child = null;
                }


                /*if (item.Tag is FileData fileData)
                {
                    if (fileData.Type == EFileType.Folder)
                        return;

                    if (MainWindow.Instance.ContentPanel.Child is IDisposable disposable)
                        disposable.Dispose();

                    try
                    {
                        switch (fileData.Type)
                        {
                            case EFileType.Localization:
                                LocalizationFile loc = LocalizationFile.Load(new ByteReader(fileData.GetData()!));
                                MainWindow.Instance.ContentPanel.Child = new LocalizationBrowser(loc);
                                break;
                            case EFileType.Image:
                            case EFileType.BGRImage:
                                MainWindow.Instance.ContentPanel.Child = new PhotoViewer(fileData);
                                break;
                            case EFileType.Audio:
                                MainWindow.Instance.ContentPanel.Child = new AudioPlayer(fileData);
                                break;
                            case EFileType.Text:
                                MainWindow.Instance.ContentPanel.Child = new TextDisplay(fileData);
                                break;
                            case EFileType.Font:
                                MainWindow.Instance.ContentPanel.Child = new FontViewer(fileData);
                                break;
                            default:
                                if (fileData.Data is not null || fileData.RealFilePath is not null)
                                    MainWindow.Instance.ContentPanel.Child = new HexDisplay(fileData);
                                else
                                    MainWindow.Instance.ContentPanel.Child = null;
                                break;
                        }
                    }
                    catch
                    {
                        MainWindow.Instance.ContentPanel.Child = null;
                    }
                }*/
            }
        }
    }

    /*public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
            return;

        FileData fileData = new FileData(path);
        TreeViewItem fileItem = CreateItem(fileData);

        // Mass Sorting
        SortTreeViewItems(fileItem.Items);

        FileTreeView.Items.Clear();
        FileTreeView.Items.Add(fileItem);
    }*/

    /*public TreeViewItem CreateItem(FileData fileData)
    {
        TreeViewItem fileItem = new TreeViewItem()
        {
            Header = fileData.Name,
            Tag = fileData
        };

        switch (fileData.Type)
        {
            case EFileType.Archive:
                ArchiveFile arc_FileData = ArchiveFile.Load(new ByteReader(fileData.GetData()!));
                foreach (ArchiveEntry arc_entry in arc_FileData.Entries)
                {
                    TreeViewItem arc_parent = fileItem;
                    string[] arc_splitName = arc_entry.Name.Split(Path.DirectorySeparatorChar);
                    for (int i = 0; i < arc_splitName.Length - 1; i++)
                    {
                        if (arc_parent.Items.Any(x => x is TreeViewItem xTvi && xTvi.Header is string xTviH && xTviH == arc_splitName[i]))
                            arc_parent = (TreeViewItem)arc_parent.Items.First(x => x is TreeViewItem xTvi && xTvi.Header is string xTviH && xTviH == arc_splitName[i])!;
                        else
                        {
                            FileData arc_newParentData = new FileData(arc_splitName[i], EFileType.Folder, null);
                            TreeViewItem arc_newParent = CreateItem(arc_newParentData);
                            arc_parent.Items.Add(arc_newParent);
                            arc_parent = arc_newParent;
                        }
                    }

                    FileData arc_entryData = new FileData(arc_entry);
                    TreeViewItem arc_entryItem = CreateItem(arc_entryData);
                    arc_parent.Items.Add(arc_entryItem);
                }
                break;
            case EFileType.Package:
                PackageFile pck_FileData = PackageFile.Load(new ByteReader(fileData.GetData()!));
                foreach (PackageEntry pck_entry in pck_FileData.Entries)
                {
                    TreeViewItem pck_parent = fileItem;
                    string[] pck_splitName = pck_entry.Name.Split(Path.DirectorySeparatorChar);
                    for (int i = 0; i < pck_splitName.Length - 1; i++)
                    {
                        if (pck_parent.Items.Any(x => x is TreeViewItem xTvi && xTvi.Header is string xTviH && xTviH == pck_splitName[i]))
                            pck_parent = (TreeViewItem)pck_parent.Items.First(x => x is TreeViewItem xTvi && xTvi.Header is string xTviH && xTviH == pck_splitName[i])!;
                        else
                        {
                            FileData pck_newParentData = new FileData(pck_splitName[i], EFileType.Folder, null);
                            TreeViewItem pck_newParent = CreateItem(pck_newParentData);
                            pck_parent.Items.Add(pck_newParent);
                            pck_parent = pck_newParent;
                        }
                    }

                    FileData pck_entryData = new FileData(pck_FileData, pck_entry);
                    TreeViewItem pck_entryItem = CreateItem(pck_entryData);
                    pck_parent.Items.Add(pck_entryItem);
                }
                break;
            case EFileType.SoundBank:
                SoundBankFile msscmp_FileData = SoundBankFile.Load(new ByteReader(fileData.GetData()!));
                foreach (SoundBankEntry msccmp_entry in msscmp_FileData.Entries)
                {
                    TreeViewItem msscmp_parent = fileItem;
                    string[] msscmp_splitName = msccmp_entry.FilePath.Split('/');
                    for (int i = 0; i < msscmp_splitName.Length - 1; i++)
                    {
                        if (msscmp_parent.Items.Any(x => x is TreeViewItem xTvi && xTvi.Header is string xTviH && xTviH == msscmp_splitName[i]))
                            msscmp_parent = (TreeViewItem)msscmp_parent.Items.First(x => x is TreeViewItem xTvi && xTvi.Header is string xTviH && xTviH == msscmp_splitName[i])!;
                        else
                        {
                            FileData msscmp_newParentData = new FileData(msscmp_splitName[i], EFileType.Folder, null);
                            TreeViewItem msscmp_newParent = CreateItem(msscmp_newParentData);
                            msscmp_parent.Items.Add(msscmp_newParent);
                            msscmp_parent = msscmp_newParent;
                        }
                    }

                    FileData msscmp_entryData = new FileData(msccmp_entry);
                    TreeViewItem msscmp_entryItem = CreateItem(msscmp_entryData);
                    msscmp_parent.Items.Add(msscmp_entryItem);
                }
                break;
            case EFileType.FourJUserInterface:
                FUIFile fui_FileData = FUIFile.Load(new ByteReader(fileData.GetData()!));
                foreach (FUIBitmap fui_Bitmap in fui_FileData.Bitmaps)
                {
                    FileData fui_EntryData = new FileData(fui_FileData, fui_Bitmap);
                    TreeViewItem fui_EntryItem = CreateItem(fui_EntryData);
                    fileItem.Items.Add(fui_EntryItem);
                }
                break;
            case EFileType.Folder:
                if (fileData.RealFilePath is not null && Directory.Exists(fileData.RealFilePath))
                {
                    foreach (string dir_dir in Directory.GetDirectories(fileData.RealFilePath))
                    {
                        FileData dir_dirData = new FileData(dir_dir);
                        TreeViewItem dir_dirItem = CreateItem(dir_dirData);
                        fileItem.Items.Add(dir_dirItem);
                    }
                    foreach (string dir_file in Directory.GetFiles(fileData.RealFilePath))
                    {
                        FileData dir_fileData = new FileData(dir_file);
                        TreeViewItem dir_fileItem = CreateItem(dir_fileData);
                        fileItem.Items.Add(dir_fileItem);
                    }
                }
                break;
        }
        return fileItem;
    }*/

    /*public void SortTreeViewItems(ItemCollection items)
    {
        List<object?> sortedItems = items
                                    .OrderBy(x => !(x is TreeViewItem xTvi && xTvi.Tag is FileData xTviFd && xTviFd.Type == EFileType.Folder)) // Folders first
                                    .ThenBy(x => x is TreeViewItem xTvi ? xTvi.Header.ToString() : string.Empty, StringComparer.Ordinal) // Alphabetical order
                                    .ToList();

        items.Clear();
        foreach (var item in sortedItems)
        {
            if (item is TreeViewItem xTvi && xTvi.Items.Count > 0)
                SortTreeViewItems(xTvi.Items);
            items.Add(item);
        }
    }*/
}