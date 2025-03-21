using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using DeconstructClassic.ConstructData.ImageBlock;
using DeconstructClassic.ConstructData.LevelBlock;
using DeconstructClassic.ConstructData.ObjectTypes;
using DeconstructClassic.Memory;
using System.Diagnostics;
using System.IO;

namespace DeconstructClassic;

public partial class LevelPreviewer : UserControl {
    public LayoutEntry Layout;
    public AppWrapper App;

    public LevelPreviewer() {
        InitializeComponent();
        Layout = null!;
        App = null!;
    }

    public LevelPreviewer(LayoutEntry layout, AppWrapper app) {
        InitializeComponent();

        LevelCanvas.Width = layout.Width;
        LevelCanvas.Height = layout.Height;
        LevelCanvas.Background = layout.Color.ToBrush();
        Layout = layout;
        App = app;

        Loaded += LoadLayout;
    }

    private void LoadLayout(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
        foreach (Layer layer in Layout.Layers) {
            foreach (LayerObject layerObject in layer.LayerObjects) {
                ObjectType layerObjectType = App.LayoutBank!.GetObjectType(layerObject.ObjectID);
                DLLFileInfo dllFileInfo = App.DLLFileInfos[layerObjectType.DLLIndex];
                switch (dllFileInfo.ObjectName) {
                    case "Sprite": {
                            CSprite sprite = new CSprite(new ByteReader(layerObject.ObjectData));
                            Animation? spriteAnimation = App.LayoutBank!.GetAnimation(sprite.AnimationRoot);
                            if (spriteAnimation != null && App.ImageBank != null) {
                                AnimationImage? animationImage = spriteAnimation.GetFirstImage();
                                if (animationImage != null) {
                                    ImageEntry? img = App.ImageBank.GetImage(animationImage.Image);
                                    if (img != null) {
                                        Image imgControl = new Image();
                                        imgControl.Stretch = Stretch.Fill;
                                        imgControl.Source = new Bitmap(new MemoryStream(img.Data));
                                        imgControl.Width = layerObject.Width;
                                        imgControl.Height = layerObject.Height;
                                        LevelCanvas.Children.Add(imgControl);
                                        Canvas.SetLeft(imgControl, layerObject.Position.X - img.Hotspot.X * (layerObject.Width / imgControl.Source.Size.Width));
                                        Canvas.SetTop(imgControl, layerObject.Position.Y - img.Hotspot.Y * (layerObject.Height / imgControl.Source.Size.Height));
                                        ToolTip.SetTip(imgControl, layerObjectType.Name);
                                    }
                                }
                            }
                        }
                        break;
                }
            }
        }
    }
}