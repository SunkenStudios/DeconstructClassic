using Avalonia.Controls;
using DeconstructClassic.ConstructData.AppBlock;
using System.Collections.ObjectModel;

namespace DeconstructClassic;

public partial class GlobalValueViewer : UserControl {
    public ObservableCollection<GlobalValueViewerPropertyRow> Properties { get; set; } = new();
    public GlobalValueViewer() : this(null) { }

    public GlobalValueViewer(GlobalVariable[]? globalVars) {
        InitializeComponent();
        extGrid.ItemsSource = Properties;

        if (globalVars is null) {
            return;
        }

        foreach (var globalVar in globalVars) {
            AddProperty(globalVar.Name, globalVar.Value);
        }


    }

    public void AddProperty(string name, object? value) {
        Properties.Add(new GlobalValueViewerPropertyRow { Name = name, Value = value!.ToString()! });
    }

}

public class GlobalValueViewerPropertyRow {
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}