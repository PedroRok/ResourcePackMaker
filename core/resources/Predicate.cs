using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Godot;

[GlobalClass]
public partial class Predicate: Resource
{
    [Export]
    public string ModelPath { get; set; }
    [Export]
    public PredicateType Type { get; set; }
    [Export]
    public double Value { get; set; }
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