using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static class StringExtensions
{
    public static string AddSpacesToSentence(this string text)
    {
        List<char> chars = new List<char>();

        text = text.TrimStart().TrimEnd();

        for (int i = 1; i < text.Length - 1; i++)
        {
            if (i < text.Length - 1)
            {
                if (char.IsUpper(text[i]) && !char.IsUpper(text[i - 1]))
                {
                    if (!chars.Contains(text[i]))
                    {
                        chars.Add(text[i]);
                    }
                }
            }
        }

        string after = text;
        foreach (var C in chars)
        {
            after = after.Replace($"{C}", $" {C}");
        }
        return after;
    }
    static string BytesToString(this long byteCount)
    {
        string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
        if (byteCount == 0)
        {
            return "0" + suf[0];
        }
        long bytes = Math.Abs(byteCount);
        int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
        double num = Math.Round(bytes / Math.Pow(1024, place), 1);
        return (Math.Sign(byteCount) * num).ToString() + suf[place];
    }
    public static string GetFileSizeReadable(this string FilePath)
    {
        if (File.Exists(FilePath))
        {
            return new FileInfo(FilePath).Length.BytesToString();
        }
        return $"No File at path {FilePath}";
    }
    public static string GetLine(this string text, int lineNo)
    {
        string[] lines = text.GetLines();
        if (lines.Length - 1 < lineNo) { return ""; }
        return lines[lineNo];
    }
    public static string[] GetLines(this string text) => text.Replace("\r", "").Split('\n');
    public static string HtmlOpenClose(this string text, string idenifier, params string[] args)
    {
        string x = $"";
        foreach (var item in args)
        {
            x += item;
        }
        return $"<{idenifier} {x}>{text}</{idenifier}>";
    }
    public static string ReverseString(this string input) => new String(input.ToCharArray().Reverse().ToArray());
    public static string SafeHtmlString(this string text) => System.Net.WebUtility.HtmlEncode(text);
    public static string[] SplitWithString(this string text, string msg) => text.Split(new string[] { msg }, StringSplitOptions.None);
}