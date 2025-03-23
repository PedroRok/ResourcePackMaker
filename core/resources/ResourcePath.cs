using System;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class ResourcePath : Resource
{
    [Export]
    public PathType PathType { get; set; }
    [Export]
    public string Parent { get; set; }
    [Export]
    public string Path { get; set; }

    /// <summary>
    /// Use parent as null if the path is implied to be "minecraft"
    /// </summary>    
    public ResourcePath(PathType pathType, string parent, string path)
    {
        PathType = pathType;
        Parent = parent;
        Path = path;
    }

    public ResourcePath(string path, PathType pathType)
    {
        string[] splitParent = path.Split(':');
        if (splitParent.Length == 2)
        {
            Parent = splitParent[0];   
            path = splitParent[1];
        }
        
        string[] splitPath = path.Split('/');
        PathType newPathType = splitPath[0] switch
        {
            "models" => PathType.Model,
            "textures" => PathType.Texture,
            "blockstates" => PathType.Blockstate,
            _ => PathType.Unknown
        };
        PathType = newPathType == PathType.Unknown ? pathType : newPathType;
        
    }

    private string GetParent()
    {
        return Parent == null ? "" : $"{Parent}:";
    }

    public string GetPathExplicit()
    {
        return $"{GetParent()}{PathType.GetValue()}/{Path}";
    }

    public string Get()
    {
        return $"{GetParent()}{Path}";
    }
}

public enum PathType
{
    Model,
    Texture,
    Blockstate,
    Unknown
}


public static class PathTypeExtensions
{
    public static string GetValue(this PathType pathType)
    {
        return pathType switch
        {
            PathType.Model => "models",
            PathType.Texture => "textures",
            PathType.Blockstate => "blockstates",
            PathType.Unknown => throw new ArgumentOutOfRangeException(nameof(pathType)),
            _ => throw new ArgumentOutOfRangeException(nameof(pathType))
        };
    }
}