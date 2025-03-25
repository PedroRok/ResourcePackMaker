using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Godot;

namespace ResourcePackMaker.core.resources;

[GlobalClass]
public partial class Predicate : Resource
{
    [Export] public ResourcePath ModelPath { get; set; }
    [Export] public PredicateType Type { get; set; }
    [Export] public double Value { get; set; }


    public static Predicate FromJson(JsonElement jsonElement)
    {
        if (!jsonElement.TryGetProperty("model", out var modelElement) ||
            !jsonElement.TryGetProperty("predicate", out var predicateElement))
        {
            return null;
        }

        var modelPath = modelElement.GetString();
        foreach (PredicateType type in Enum.GetValues(typeof(PredicateType)))
        {
            if (predicateElement.TryGetProperty(type.GetValue(), out JsonElement value))
            {
                return new Predicate
                {
                    ModelPath = new ResourcePath(modelPath, PathType.Model),
                    Type = type,
                    Value = value.GetDouble()
                };
            }
        }

        return null;
    }

    public JsonObject ToJson()
    {
        var jsonObject = new JsonObject();
        
        if (ModelPath == null)
        {
            return jsonObject;
        }
        
        jsonObject.Add("model", ModelPath.Get());
        
        var predicateObject = new JsonObject { { Type.GetValue(), Value } };

        jsonObject.Add("predicate", predicateObject);

        return jsonObject;
    }
}

public enum PredicateType
{
    CustomModelData,
    TrimType
}

public static class PredicateTypeExtensions
{
    public static string GetValue(this PredicateType predicateType)
    {
        return predicateType switch
        {
            PredicateType.CustomModelData => "custom_model_data",
            PredicateType.TrimType => "trim_type",
            _ => throw new ArgumentOutOfRangeException(nameof(predicateType))
        };
    }

    public static Type GetType(this PredicateType predicateType)
    {
        return predicateType switch
        {
            PredicateType.CustomModelData => typeof(int),
            PredicateType.TrimType => typeof(float),
            _ => throw new ArgumentOutOfRangeException(nameof(predicateType))
        };
    }
}