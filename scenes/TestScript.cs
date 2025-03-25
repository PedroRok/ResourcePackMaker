using System;
using Godot;
using Godot.Collections;
using ResourcePackMaker.core.resources;

public partial class TestScript : HBoxContainer
{
    [Export] public ItemBase Item { get; set; }

    [Export] public Button SaveAllButton { get; set; }

    [Export] public FileDialog SaveFileDialog { get; set; }

    public override void _Ready()
    {
        if (SaveAllButton != null)
        {
            SaveFileDialog.FileSelected += _OnSaveAllButtonPressed;
        }
    }

    private void _OnSaveAllButtonPressed(string path)
    {
        var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);

        if (Item == null)
        {
            file.Close();
            return;
        }
        Godot.GD.Print("Saving item: " + Item.Name);
        GD.Print(Item.ToJson().ToString());
        file.StoreString(Item.ToJson().ToJsonString());
        file.Close();
    }
}