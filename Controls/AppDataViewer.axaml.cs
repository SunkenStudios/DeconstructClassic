using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DeconstructClassic.ConstructData;
using System.Collections.ObjectModel;

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

        AddProperty("App Name", appData.AppName);
        AddProperty("Fullscreen", appData.Fullscreen);
        AddProperty("FPS Mode", appData.FPSMode);
        AddProperty("Window Width", appData.WindowWidth);
        AddProperty("Window Height", appData.WindowHeight);
        AddProperty("Is Screensaver", appData.IsScreensaver);
        AddProperty("Eye Distance", appData.EyeDistance);


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