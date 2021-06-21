using System;
using System.IO;
using UnityEngine;

public static class StringExtensions
{
    public static void CheckCreateDirectory(this string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }



    public static string RemoveClone(this string text) => text.Replace("(Clone)", "");
    public static string AddCharToEnd(this string text, char c) => text += $"{c}";
    public static string AddColor(this string text, Color col) => $"<color={ColorHexFromUnityColor(col)}>{text}</color>";
    public static string ColorHexFromUnityColor(this Color unityColor) => $"#{ColorUtility.ToHtmlStringRGBA(unityColor)}";
    public static string AddSubscript(this string text, string sub) => $"{text}<sub>{sub}</sub>";
    public static string AddSuperscript(this string text, string super) => $"{text}<sup>{super}</sup>";
    public static string AlignFromPos(this string text, float pos) => $" <pos={pos}>{text}</pos>";
    public static string AlignCenter(this string text) => $" <align=center>{text}</align>";
    public static string AlignLeft(this string text) => $" <align=left>{text}</align>";
    public static string AlignRight(this string text) => $" <align=right>{text}</align>";
    public static string FileSaveTime(this string jsonFilePath) => FileSaveTimeReadable(File.GetLastWriteTimeUtc(jsonFilePath));
    public static string FileSaveTimeReadable(this DateTime dt) => $"{dt.Hour.LeadingZero()} : {dt.Minute.LeadingZero()} : {dt.Second.LeadingZero()}";
    public static string LeadingZero(this int num) => num.ToString().PadLeft(2, '0');
    public static string RemoveLastChar(this string text) => text.Remove(text.Length - 1, 1);
    public static string SetBold(this string text) => $"<b>{text}</b>";
    public static string SetItalic(this string text) => $"<i>{text}</i>";
    public static string SetSize(this string text, int size) => $"<size={size}>{text}</size>";



    public static string GetBytesReadable(this long i)
    {
        // Get absolute value
        long absolute_i = (i < 0 ? -i : i);
        // Determine the suffix and readable value
        string suffix;
        double readable;
        if (absolute_i >= 0x1000000000000000) // Exabyte
        {
            suffix = "EB";
            readable = (i >> 50);
        }
        else if (absolute_i >= 0x4000000000000) // Petabyte
        {
            suffix = "PB";
            readable = (i >> 40);
        }
        else if (absolute_i >= 0x10000000000) // Terabyte
        {
            suffix = "TB";
            readable = (i >> 30);
        }
        else if (absolute_i >= 0x40000000) // Gigabyte
        {
            suffix = "GB";
            readable = (i >> 20);
        }
        else if (absolute_i >= 0x100000) // Megabyte
        {
            suffix = "MB";
            readable = (i >> 10);
        }
        else if (absolute_i >= 0x400) // Kilobyte
        {
            suffix = "KB";
            readable = i;
        }
        else
        {
            return i.ToString("0 B"); // Byte
        }
        // Divide by 1024 to get fractional value
        readable /= 1024;
        // Return formatted number with suffix
        return readable.ToString("0.### ") + suffix;
    }

}