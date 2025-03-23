using System.Collections.Generic;
using System.Text.Json.Nodes;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class ItemBase : Resource
{
    [Export]
    public string Name { get; set; }
    [Export]
    public ResourcePath Parent { get; set; }
    [Export]
    public Godot.Collections.Dictionary<string, ResourcePath> Textures { get; set; }
    [Export]
    public Array<Predicate> PredicateList { get; set; }
    
    public JsonObject ToJson()
    {
        JsonObject jsonObject = new JsonObject();
        jsonObject.Add("parent", Parent.Get());
        if (Textures.Count != 0)
        {
            JsonObject textureJsonObject = new JsonObject();
            foreach (KeyValuePair<string, ResourcePath> texture in Textures)
            {
                textureJsonObject.Add(texture.Key, texture.Value.Get());
            }
            jsonObject.Add("textures", textureJsonObject);
        }

        if (PredicateList.Count != 0)
        {
            JsonArray predicateJsonArray = new JsonArray();
            foreach (Predicate predicate in PredicateList)
            {
                predicateJsonArray.Add(predicate.ToJson());
            }
            jsonObject.Add("predicates", predicateJsonArray);
        }
        return jsonObject;
    }
}
