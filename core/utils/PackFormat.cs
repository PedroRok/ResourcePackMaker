﻿using System.Collections.Generic;
using System.Linq;

namespace ResourcePackMaker.core.utils;

public static class PackFormat
{
    private static readonly Dictionary<string, int> PackFormats = new()
    {
        {"1.6.1 - 1.8.9", 1},
        {"1.9 - 1.10.2", 2},
        {"1.11 - 1.12.2", 3},
        {"1.13 - 1.14.4", 4},
        {"1.15 - 1.16.1", 5},
        {"1.16.2 - 1.16.5", 6},
        {"1.17 - 1.17.1", 7},
        {"1.18 - 1.18.2", 8},
        {"1.19 - 1.19.2", 9},
        {"1.19.3", 12},
        {"1.19.4", 13},
        {"1.20 - 1.20.1", 15},
        {"1.20.2", 18},
        {"1.20.3 - 1.20.4", 22},
        {"1.20.5+", 32},
        {"1.21+", 34},
        {"1.21.4+", 46}
    };
    
    public static List<string> GetPackFormats()
    {
        return PackFormats.Select(packFormat => packFormat.Key).ToList();
    }
    
    public static int GetPackFormat(string packFormat)
    {
        return PackFormats[packFormat];
    }
}