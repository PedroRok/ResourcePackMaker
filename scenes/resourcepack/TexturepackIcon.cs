using Godot;

namespace ResourcePackMaker.scenes.resourcepack;

public partial class TexturepackIcon : TextureRect
{
    public override void _GuiInput(InputEvent @event)
    {
        if(!@event.IsPressed()) return;
        if (@event is not InputEventMouseButton { ButtonIndex: MouseButton.Left }) return;
        
        var fileDialog = new FileDialog();
        fileDialog.Title = "Select a icon to your texturepack";
        fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
        fileDialog.Filters = ["*.png"];
        fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        GetTree().Root.AddChild(fileDialog);
        fileDialog.PopupCentered();
        fileDialog.FileSelected += _OnFileDialogFileSelected;
    }
    
    private void _OnFileDialogFileSelected(string path)
    {
        GD.Print("Selected file: " + path);
        // load the image
        Image image = new Image();
        Error err = image.Load(path);
        if (err != Error.Ok)
        {
            GD.PrintErr("Error when loading image: " + path);
            return;
        }
        
        ImageTexture texture = new ImageTexture();
        texture.SetImage(image);
        Texture = texture;
    }
}