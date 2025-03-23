using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using DeconstructClassic.ConstructData;
using DeconstructClassic.ConstructData.ImageBlock;
using DeconstructClassic.ConstructData.LevelBlock;
using DeconstructClassic.ConstructData.ObjectTypes;
using DeconstructClassic.Memory;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;

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

        LoadLayout(null, null);
        //Loaded += LoadLayout;
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
                                        imgControl.Source = img.GetBitmap();
                                        imgControl.Width = Math.Abs(layerObject.Width);
                                        imgControl.Height = Math.Abs(layerObject.Height);
                                        LevelCanvas.Children.Add(imgControl);
                                        Canvas.SetLeft(imgControl, layerObject.Position.X - img.Hotspot.X * (layerObject.Width / imgControl.Source.Size.Width));
                                        Canvas.SetTop(imgControl, layerObject.Position.Y - img.Hotspot.Y * (layerObject.Height / imgControl.Source.Size.Height));
                                        ToolTip.SetTip(imgControl, layerObjectType.Name);
                                    }
                                }
                            }
                        }
                        break;
                    case "Text": {
                            CText text = new CText(new ByteReader(layerObject.ObjectData));
                            if (text.Version != 2) {
                                Debug.WriteLine("Unknown text version: " + text.Version);
                                break;
                            }

                            Screen? screen = MainWindow.Instance.Screens.Primary;
                            double scalingFactor = screen?.Scaling ?? 1.0;
                            double dpi = scalingFactor * 96;

                            TextBlock textControl = new TextBlock();
                            textControl.Text = text.Text;
                            if (Application.Current.Resources.ContainsKey(text.FontName[0..1].ToUpper() + text.FontName[1..].ToLower())) {
                                textControl.FontFamily = (FontFamily)Application.Current.Resources[text.FontName];

                                switch (text.FontName.ToLower()) {
                                    case "system": // Correct from the minimal uses of System I've seen
                                        textControl.FontSize = (text.FontSize * 0.9) * (dpi / 96.0);
                                        break;
                                    case "terminal": // Incorrect, needs better algorithm than just mutiplying
                                        textControl.FontSize = (text.FontSize * 1.0) * (dpi / 96.0);
                                        break;
                                    case "fixedsys": // Unverified
                                        textControl.FontSize = (text.FontSize * 1.0) * (dpi / 96.0);
                                        break;
                                }
                            }
                            else if (CheckFontExists(text.FontName))  {
                                textControl.FontFamily = new FontFamily($"{text.FontName}, {GetFixedFontName(text.FontName)}");

                                Typeface typeface = new Typeface(textControl.FontFamily);
                                var glyphTypeface = typeface.GlyphTypeface;
                                double lineHeight = Math.Abs(glyphTypeface.Metrics.Ascent) - glyphTypeface.Metrics.Descent;
                                double fontSize = text.FontSize * (lineHeight / 2048.0) * 1.25 * (dpi / 96.0);

                                textControl.FontSize = fontSize;
                            }
                            textControl.FontWeight = text.Bold ? FontWeight.Bold : FontWeight.Normal;
                            textControl.FontStyle = text.Italic ? FontStyle.Italic : FontStyle.Normal;
                            textControl.Foreground = text.TextColor.ToBrush();
                            textControl.Opacity = text.Opacity;
                            textControl.TextAlignment = text.HorizontalAlignment switch {
                                Enums.TextHorizontalAlignment.Left => TextAlignment.Left,
                                Enums.TextHorizontalAlignment.Center => TextAlignment.Center,
                                Enums.TextHorizontalAlignment.Right => TextAlignment.Right,
                                _ => TextAlignment.Left
                            };
                            textControl.Width = Math.Abs(layerObject.Width);
                            // Inaccurate to Construct but it's better for a previewer
                            // textControl.Height = Math.Abs(layerObject.Height);
                            textControl.TextWrapping = TextWrapping.Wrap;
                            textControl.ClipToBounds = false; // Inaccurate to Construct but it's better for a previewer
                            LevelCanvas.Children.Add(textControl);
                            Canvas.SetLeft(textControl, layerObject.Position.X);
                            Canvas.SetTop(textControl, layerObject.Position.Y);
                            ToolTip.SetTip(textControl, layerObjectType.Name);
                        }
                        break;
                    case "Tiled Background": {
                            CTiledBackground tiledBackground = new CTiledBackground(new ByteReader(layerObject.ObjectData));
                            ImageEntry? img = App.ImageBank?.GetImage(tiledBackground.ImageID);
                            if (img != null) {
                                Bitmap bmp = img.GetBitmap();
                                ImageBrush imgBrush = new ImageBrush();
                                imgBrush.Source = bmp;
                                imgBrush.TileMode = TileMode.Tile;
                                imgBrush.DestinationRect = new RelativeRect(0, 0, bmp.Size.Width, bmp.Size.Height, RelativeUnit.Absolute);
                                Panel imgPanel = new Panel();
                                imgPanel.Background = imgBrush;
                                imgPanel.Width = Math.Abs(layerObject.Width);
                                imgPanel.Height = Math.Abs(layerObject.Height);
                                LevelCanvas.Children.Add(imgPanel);
                                Canvas.SetLeft(imgPanel, layerObject.Position.X);
                                Canvas.SetTop(imgPanel, layerObject.Position.Y);
                                ToolTip.SetTip(imgPanel, layerObjectType.Name);
                            }
                        }
                        break;
                    default:
#if DEBUG
                        Debug.WriteLine("Unknown object type: " + dllFileInfo.ObjectName);
                        if (!Directory.Exists("Debug")) {
                            Directory.CreateDirectory("Debug");
                        }
                        File.WriteAllBytes("Debug\\" + dllFileInfo.ObjectName + ".bin", layerObject.ObjectData);
#endif
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Skia seems to omit "Extended" from the ends of font names, so we need to remove it.
    /// </summary>
    private string GetFixedFontName(string fontName) {
        List<string> strings = fontName.Split(' ').ToList();
        strings.Remove("Extended");
        return string.Join(' ', strings);
    }

    private bool CheckFontExists(string fontName) {
        using (var fontsCollection = new InstalledFontCollection()) {
            return fontsCollection.Families.Any(font => font.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase));
        }
    }

}