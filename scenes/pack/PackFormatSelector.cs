using Godot;
using ResourcePackMaker.core.utils;
using ResourcePackMaker.global;

namespace ResourcePackMaker.scenes.pack;

[Tool]
[GodotClassName("PackFormatSelector")]
public partial class PackFormatSelector : OptionButton
{
    public override void _Ready()
    {
        PackFormat.GetPackFormats().ForEach(val =>
        {
            AddItem($"Minecraft {val}");
        } );
        
        ItemSelected += _OnItemSelected;
    }
    
    
    private void _OnItemSelected(long index)
    {
        var packFormat = GetPackFormatSelected();
        GD.Print("Selected pack format: " + packFormat);
        GD.Print(Global.BasePath);
    }

    public int GetPackFormatSelected()
    {
        var selected = GetItemText(Selected).Replace("Minecraft ", "");
        var packFormat = PackFormat.GetPackFormat(selected);
        return packFormat;
    }
    
}