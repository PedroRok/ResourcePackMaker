using Godot;
using ResourcePackMaker.global;

namespace ResourcePackMaker.scenes.pack;

[GodotClassName("PackIcon")]
public partial class TexturepackIcon : TextureRect
{
    public override void _GuiInput(InputEvent @event)
    {
        if(!@event.IsPressed()) return;
        if (@event is not InputEventMouseButton { ButtonIndex: MouseButton.Left }) return;
        
        var fileDialog = new FileDialog();
        fileDialog.FileMode = FileDialog.FileModeEnum.OpenFile;
        fileDialog.SetTitle("Select a icon to your texturepack");
        fileDialog.Filters = ["*.png"];
        fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        fileDialog.SetCurrentDir(Global.BasePath);
        GetTree().Root.AddChild(fileDialog);
        fileDialog.PopupCentered();

        fileDialog.FileSelected += _OnFileDialogFileSelected;
    }
    
    private void _OnFileDialogFileSelected(string path)
    {
        GD.Print("Selected file: " + path);
        // load the image
        var image = new Image();
        var err = image.Load(path);
        if (err != Error.Ok)
        {
            GD.PrintErr("Error when loading image: " + path);
            return;
        }
        
        var texture = new ImageTexture();
        texture.SetImage(image);
        Texture = texture;
    }
}