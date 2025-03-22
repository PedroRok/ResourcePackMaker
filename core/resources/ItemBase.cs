using Godot;
using Godot.Collections;

[GlobalClass]
public partial class ItemBase : Resource
{
    [Export]
    public string Name { get; set; }
    [Export]
    public string Parent { get; set; }
    [Export]
    public Dictionary<string, string> Textures { get; set; }
    [Export]
    public Array<Predicate> PredicateList { get; set; }
    
    
    
}
