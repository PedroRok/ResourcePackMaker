using System.Text.Json.Nodes;
using Godot;
using Godot.Collections;

namespace ResourcePackMaker.core.resources;

[GlobalClass]
public partial class ItemBase : Resource
{
    [Export] public string Name { get; set; }
    [Export] public ResourcePath Parent { get; set; }
    [Export] public Dictionary<string, ResourcePath> Textures { get; set; }
    [Export] public Array<Predicate> PredicateList { get; set; }

    public JsonObject ToJson()
    {
        var jsonObject = new JsonObject();

        if (Parent != null)
        {
            jsonObject.Add("parent", Parent.Get());
        }

        if (Textures is { Count: > 0 })
        {
            var textureJsonObject = new JsonObject();
            foreach (var texture in Textures)
            {
                if (texture.Value == null) continue;
                textureJsonObject.Add(texture.Key, texture.Value.Get());
            }

            if (textureJsonObject.Count > 0)
            {
                jsonObject.Add("textures", textureJsonObject);
            }
        }

        if (PredicateList is not { Count: > 0 }) return jsonObject;

        var predicateJsonArray = new JsonArray();
        foreach (var predicate in PredicateList)
        {
            if (predicate == null) continue;
            predicateJsonArray.Add(predicate.ToJson());
        }

        if (predicateJsonArray.Count > 0)
        {
            jsonObject.Add("overrides", predicateJsonArray);
        }

        return jsonObject;
    }
}