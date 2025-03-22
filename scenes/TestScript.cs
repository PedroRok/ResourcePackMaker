using Godot;
using Godot.Collections;

public partial class TestScript : HBoxContainer
{
    [Export]
    public Array<ItemBase> Item { get; set; }
}
