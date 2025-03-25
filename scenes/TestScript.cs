using Godot;
using ResourcePackMaker.core.resources;
using ResourcePackMaker.core.utils;
using ResourcePackMaker.scenes.pack;
using ResourcePackMaker.scenes.scripts;

namespace ResourcePackMaker.scenes;

public partial class TestScript : HBoxContainer
{
    [Export] public ResourcePath Path { get; set; }
    [Export] public ItemBase Item { get; set; }

    [Export] public MainController MainController { get; set; }
    
    [Export] public TextEdit DescriptionLabel { get; set; }
    
    [Export] public TexturepackIcon TexturepackIcon { get; set; }
    
    [Export] public PackFormatSelector PackFormatSelector { get; set; }
    
    public override void _Ready()
    {
        MainController.FileButtonEventHandler += _OnSaveAllButtonPressed;
    }

    private void _OnSaveAllButtonPressed(FileMenuButton button)
    {
        if (button != FileMenuButton.Save) return;
        var acceptDialog = new AcceptDialog();

        var packData = new PackData(TexturepackIcon.GetTexture().GetImage(), DescriptionLabel.Text,
            PackFormatSelector.GetPackFormatSelected());

        var jsonObject = Item.ToJson();
        if (Path == null)
        {
            acceptDialog.DialogText = "Path is null!";
        }
        else if (jsonObject == null)
        {
            acceptDialog.DialogText = "Item is null!";
        }
        else
        {
            packData.SavePack();
            JsonUtils.SaveJson(Path, jsonObject);
            acceptDialog.DialogText = "Saved!";
        }

        acceptDialog.Popup();
        GetTree().Root.AddChild(acceptDialog);
    }
}