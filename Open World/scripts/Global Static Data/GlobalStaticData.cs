using System;
using UnityEngine;

public static class GlobalStaticData
{
    public static string MainDir => $"{Application.persistentDataPath}";
    public static string DocumentsDir => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    public static string DocumentsDirWithAppName => MainDir;
    public static string ApplicationCustomMusicLocation => $"{DocumentsDirWithAppName}/Custom Music";
    public static string ApplicationSettingLocation => $"{DocumentsDirWithAppName}/GameSettings";
    public static string ApplicationSettingJsonLocation => $"{ApplicationSettingLocation}/Settings.json";

    public static void Init()
    {
        DocumentsDirWithAppName.CheckCreateDirectory();
        ApplicationSettingLocation.CheckCreateDirectory();
    }
}