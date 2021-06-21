using System.Collections;
using System.IO;
using System;
using UnityEngine;
using System.Diagnostics;

public static class SaveTextureToFile
{
    public static string dirName = "Saved2D_Textures";
    public static string fileName = "textToPng";


    public static void SaveTxt2D(this Texture2D savedTexture, string dirPath, string fileName)
    {
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }
        DirectoryInfo di = new DirectoryInfo(dirPath);
        fileName = $"{fileName}_{DirCount(di)}.png";
        string fileLocation = $"{Application.persistentDataPath}/{dirPath}/{fileName}";
        File.WriteAllBytes(fileLocation, savedTexture.EncodeToPNG());
    }

    static int DirCount(DirectoryInfo dir) => dir.GetFiles().Length;



    public static string TimeSinceStart()
    {
        DateTime UtcNow = Process.GetCurrentProcess().StartTime.ToUniversalTime();
        return $"{UtcNow.Hour}:{UtcNow.Minute}:{UtcNow.Second}";
    }
}
