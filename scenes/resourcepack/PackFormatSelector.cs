using Godot;
using ResourcePackMaker.core.utils;

namespace ResourcePackMaker.scenes.resourcepack;

[Tool]
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
        var selected = GetItemText((int) index).Replace("Minecraft ", "");
        var packFormat = PackFormat.GetPackFormat(selected);
        GD.Print("Selected pack format: " + packFormat);
    }
    
}