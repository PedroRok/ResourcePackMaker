using System;
using global::ResourcePackMaker.global;
using Godot;
using Godot.Collections;

namespace ResourcePackMaker.scenes.scripts;

[GlobalClass]
public partial class MainController : Control
{
    [Export(PropertyHint.GlobalDir)]
    public string BasePath
    {
        get => Global.BasePath; 
        set => Global.BasePath = value;
    }
    
    public Action<FileMenuButton> FileButtonEventHandler;

    [Export] public PopupMenu FileMenu;

    public override void _Ready()
    {
        FileMenu.IndexPressed += FileButtonPressed;
    }

    private void FileButtonPressed(long index)
    {
        var fileMenuButton = (FileMenuButton) index;
        FileButtonEventHandler?.Invoke(fileMenuButton);
    }
}

public enum FileMenuButton
{
    Save = 0,
    SaveAsZip = 1
}
