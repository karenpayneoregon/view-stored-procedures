using System.ComponentModel;

using CommonLibrary;
using ConsoleConfigurationLibrary.Classes;
using GetStoredProceduresAppVisual.Classes;
using GetStoredProceduresAppVisual.Models;
using Microsoft.Extensions.Configuration;
using SqlServerLibrary.Classes;
using SqlServerLibrary.Extensions;

using static System.DateTime;

namespace GetStoredProceduresAppVisual;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private ListDictionary _listDictionary;
    private BindingList<ProcItem> _bindingList = new();
    private StoredProcedureHelpers _helpers = new();
    
    private async void Form1_Load(object sender, EventArgs e)
    {

        SaveButton.Enabled = false;
        
        _listDictionary = await Operations.GetStoredProcedureDetails();

        List<ProcItem> data = _listDictionary.Dictionary
            .Select(x => new ProcItem(x.Key, x.Value))
            .OrderBy(x => x.Name)
            .ToList();
        
        _bindingList = new BindingList<ProcItem>(data);
        DatabaseComboBox.DataSource = _bindingList;
        
        PopulateProcComboBox();
        
        DatabaseComboBox.SelectedIndexChanged += DatabaseComboBox_SelectedIndexChangedAsync;
        ProceduresComboBox.SelectedIndexChanged += ProceduresComboBox_SelectedIndexChanged;
        ProcedureTextBox.TextChanged += ProcedureTextBox_TextChanged;
        
        Colorizer.Execute(ProcedureTextBox);
        ActiveControl = DatabaseComboBox;
        
        SaveButton.Enabled = true;
    }

    private void ProcedureTextBox_TextChanged(object? sender, EventArgs e)
    {
        Colorizer.Execute(ProcedureTextBox);
    }

    private void ProceduresComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        PresentProcedure();
    }

    private void PresentProcedure()
    {
        var current = _bindingList[DatabaseComboBox.SelectedIndex];
        
        ProceduresComboBox.DataSource = current.List;

        var dbName = DatabaseComboBox.Text;
        var selectedProc = current.List[ProceduresComboBox.SelectedIndex];
        
        ProcedureTextBox.Text = _helpers.GetStoredProcedureDefinition(dbName, selectedProc);
    }

    private void DatabaseComboBox_SelectedIndexChangedAsync(object? sender, EventArgs e)
    {
        PopulateProcComboBox();
    }

    private void PopulateProcComboBox()
    {
        PresentProcedure();
    }

    /// <summary>
    /// Save all stored procedures to a folder for the currently selected database
    /// </summary>
    private void SaveButton_Click(object sender, EventArgs e)
    {
        var current = _bindingList[DatabaseComboBox.SelectedIndex];
        var _configuration = Configuration.JsonRoot();
        var serverName = _configuration.GetValue<string>("Server:Name").CleanFileName();
        
        var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts",
            $"{Now.Year}-{Now.Month:d2}-{Now.Day:d2}" );

        foreach (var item in current.List)
        {
            File.WriteAllText(Path.Combine(folder, $"{serverName}_{current.Name}_{item}.sql"), 
                _helpers.GetStoredProcedureDefinition(DatabaseComboBox.Text, item));
        }
    }
}