using CodeBureau;
using GAMO.Hesman.Utilities;
using System;
using UnityEngine;

public static class StringExtension
{
    private static readonly string[] _MetricPrefix = new[] { "", "K", "M", "B", "T", "P", "E", "Z", "Y" };
    public static string random(this string str, int length)
    {
        string b = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringFast sf = new StringFast(length);
        for (int i = 0; i < length; i++)
        {
            sf.Append(b[UnityEngine.Random.Range(0, b.Length - 1)].ToString());
        }
        return sf.ToString();
    }
    /// <summary>
    /// Truncate a string (cut it short)
    /// </summary>
    /// <param name="value">string value</param>
    /// <param name="maxLength">maximum length allowed</param>
    /// <returns>truncated string to maximum length, if need be</returns>
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    public static string ShortNumber(float number)
    {

#if UNITY_EDITOR
        Debug.LogError("DO NOT USE THIS FUNCTIONS");
#endif

        string result = number.ToString();
        string tail = null;
        int index = 0;
        while (number >= System.Math.Pow(1000, index + 1))
        {
            index++;
        }
        switch (index)
        {
            case 0: tail = string.Empty; break;
            case 1: tail = "K"; break;
            case 2: tail = "M"; break;
            case 3: tail = "B"; break;
            default: index = 3; tail = "B"; break;
        }

        result = System.Math.Round((number / (System.Math.Pow(1000, index))), 2).ToString("#,#.#");
        if (string.IsNullOrEmpty(result)) result = "0";
        return result + (string.IsNullOrEmpty(tail) ? string.Empty : " " + tail);
    }
    /// <summary>
    /// Shorten number to a specific length by replace additional length by Greek letter
    /// </summary>
    /// <param name="value">number want to short</param>
    /// <param name="length">maximum length allowed</param>
    /// <param name="format">whether to format return string</param>
    /// <returns>shortened string (i.e 1000 -> 1K)</returns>
    public static string Shorten(double value, int length, bool format = true)
    {
        int index = 0;
        while (Math.Round(value, 2).ToString().Replace(".", "").Length > length)
        {
            if (value >= 1000)
            {
                value /= 1000;
                index++;
            }
            else
                break;
        }
        StringFast stringFast = new StringFast();
        if (format)
            stringFast.Append(Math.Round(value, 2, MidpointRounding.ToEven).ToString("#,#.#"));
        else
            stringFast.Append(Math.Round(value, 2));
        return stringFast.Append(_MetricPrefix[index]).ToString();
    }
    public static T toEnum<T>(this string str, bool ignoreCase = true)
    {
        return (T)System.Enum.Parse(typeof(T), str, ignoreCase);
    }
    public static T fromObscuredToEnum<T>(this string str, bool ignoreCase = true)
    {
        return str.ToString().toEnum<T>(ignoreCase);
    }
    public static bool tryToEnum<T>(this string str, out T result, bool ignoreCase = true)
    {
        try
        {
            result = str.toEnum<T>(ignoreCase);
            return true;
        }
        catch (System.Exception ex)
        {
            result = default(T);
            return false;
        }
    }
    public static bool tryFromObscuredToEnum<T>(this string str, out T result, bool ignoreCase = true)
    {
        try
        {
            result = str.fromObscuredToEnum<T>(ignoreCase);
            return true;
        }
        catch (System.Exception ex)
        {
            result = default(T);
            return false;
        }
    }
    public static T toStringEnum<T>(this string obscuredString)
    {
        return (T)StringEnum.Parse(typeof(T), obscuredString, true);
    }
    public static Color toColor(this string hex)
    {
        Color result = Color.white;
        ColorUtility.TryParseHtmlString(hex, out result);
        return result;
    }
}
