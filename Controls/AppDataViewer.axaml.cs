using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DeconstructClassic.ConstructData.AppBlock;
using System;
using System.Collections.ObjectModel;
using static DeconstructClassic.ConstructData.Enums;

namespace DeconstructClassic;

public partial class AppDataViewer : UserControl
{
    public ObservableCollection<AppDataViewerPropertyRow> Properties { get; set;  } = new();
    public AppDataViewer() : this(null) {}

    public AppDataViewer(AppData? appData)
    {
        InitializeComponent();
        extGrid.ItemsSource = Properties;

        if (appData is null)
        {
            return;
        }
        AddProperty("App Name",appData.AppName);
        AddProperty("Window Width",appData.WindowWidth);
        AddProperty("Window Height",appData.WindowHeight);
        AddProperty("Eye Distance",appData.EyeDistance);
        AddProperty("Show Menu",appData.ShowMenu);
        AddProperty("Is Screensaver",appData.IsScreensaver);
        AddProperty("FPS Mode",appData.FPSMode);
        AddProperty("FPS",appData.FPS);
        AddProperty("Fullscreen",appData.Fullscreen);
        AddProperty("Sampler",appData.Sampler);
        AddProperty("Global Variables",appData.GlobalVariables);
        AddProperty("Controls",appData.Controls);
        AddProperty("Disable Windows Key",appData.DisableWindowsKey);
        AddProperty("Key Data",appData.KeyData);
        AddProperty("Simulate Shaders",appData.SimulateShaders);
        AddProperty("File Path",appData.FilePath);
        AddProperty("FPS In Caption",appData.FPSInCaption);
        AddProperty("Use Motion Blur",appData.UseMotionBlur);
        AddProperty("Motion Blur Steps",appData.MotionBlurSteps);
        AddProperty("Text Rendering Mode",appData.TextRenderingMode);
        AddProperty("Override Deltatime",appData.OverrideTimeDelta);
        AddProperty("Deltatime Override",appData.TimeDeltaOverride);
        AddProperty("Caption",appData.Caption);
        AddProperty("Minimize Box",appData.MinimizeBox);
        AddProperty("Resize Setting",appData.ResizeSetting);
        AddProperty("Maximize Box",appData.MaximizeBox);
        AddProperty("Minimum FPS",appData.MinimumFPS);
        AddProperty("Layout ID",appData.LayoutID);
        AddProperty("Multi Samples",appData.MultiSamples);
        AddProperty("Texture Loading", appData.TextureLoading);


    }

    public void AddProperty(string name, object? value)
    {
        Properties.Add(new AppDataViewerPropertyRow { Name = name, Value = value!.ToString()! });
    }

}

public class AppDataViewerPropertyRow
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}