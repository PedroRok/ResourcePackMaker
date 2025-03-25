using System.IO;
using System.Text.Json.Nodes;
using global::ResourcePackMaker.global;
using Godot;
using ResourcePackMaker.core.utils;

namespace ResourcePackMaker.core.resources;

public class PackData(Image image, string description, int packFormat, string packName = "pack")
{
    private string _packName = packName;


    public void SavePack()
    {
        SaveImage();
        SaveMcMeta();
    }

    private void SaveImage()
    {
        var basePath = Global.BasePath + "\\pack.png";
        if (File.Exists(basePath))
        {
            File.Delete(basePath);
        }
        image.SetName("pack.png");
        var savePng = image.SavePng(Global.BasePath);
        if (savePng != Error.Ok)
        {
            GD.PrintErr("Failed to save image");
        }
    }

    private void SaveMcMeta()
    {

        var jsonDescription = new JsonArray();
        var jsonText = new JsonObject
        {
            { "text", description },
            { "color", "#FFAA23" }
        };
        jsonDescription.Add(jsonText);
        
        
        var jsonObject = new JsonObject
        {
            {
                "pack", new JsonObject
                {
                    {
                        "pack_format", packFormat
                    },
                    {
                        "description", jsonDescription
                    }
                }
            }
        };
        JsonUtils.SaveJson(Global.BasePath+"\\pack.mcmeta", jsonObject, "mcmeta");
    }
}