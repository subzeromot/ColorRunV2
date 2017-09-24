using UnityEngine;
using System.Collections;

public static class ColorExtension {
    /// <summary>
    /// New color from current color with modifies
    /// </summary>
    /// <param name="color">current color</param>
    /// <param name="r">red</param>
    /// <param name="g">green</param>
    /// <param name="b">blue</param>
    /// <param name="a">alpha</param>
    /// <returns>new color</returns>
    public static Color modify(this Color color, float r = -1f, float g = -1f, float b = -1f, float a = -1f) {
        return new Color(r != -1f ? r : color.r, g != -1f ? g : color.g, b != -1f ? b : color.b, a != -1f ? a : color.a);
    }
    public static string toHex(this Color color) {
        return ColorUtility.ToHtmlStringRGBA(color);
    }
}
