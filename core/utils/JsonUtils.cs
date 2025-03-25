using System;
using System.IO;
using System.Text.Json.Nodes;
using global::ResourcePackMaker.global;
using Godot;
using ResourcePackMaker.core.resources;
using FileAccess = Godot.FileAccess;

namespace ResourcePackMaker.core.utils;

public static class JsonUtils
{
    public static void SaveJson(ResourcePath path, JsonObject element, string extension = "json")
    {
        var finalPath = $"{Global.BasePath}/assets/{path.GetRelativeFilePath()}";
        SaveJson(finalPath, element, extension);
    }
    
    public static void SaveJson(string path, JsonObject jsonObject, string extension = "json")
    {
        var finalPath = _getFinalPath(path, extension);
        var jsonString = jsonObject.ToString();
        Directory.CreateDirectory(finalPath[..finalPath.LastIndexOf('/')]);
        var file = FileAccess.Open(finalPath, FileAccess.ModeFlags.Write);
        file.StoreString(jsonString);
        file.Close();
    }

    public static JsonObject LoadJson(ResourcePath path, string extension = "json")
    {
        var finalPath = $"{Global.BasePath}/assets/{path.GetRelativeFilePath()}";
        return LoadJson(finalPath, extension);
    }
    
    public static JsonObject LoadJson(string path, string extension = "json")
    {
        var finalPath = _getFinalPath(path, extension);
        if (!FileAccess.FileExists(finalPath)) return null;
        var file = FileAccess.Open(finalPath, FileAccess.ModeFlags.Read);
        var jsonString = file.GetAsText();
        file.Close();
        var jsonNode = JsonNode.Parse(jsonString);
        return jsonNode as JsonObject;
    }

    private static string _getFinalPath(string path, string extension)
    {
        var finalPath = $"{path}";
        if (!path.EndsWith(extension))
        {
            finalPath = finalPath.Split(".")[0];
            finalPath = $"{finalPath}.{extension}";
        }
        return finalPath;
    }
}