using UnityEngine;

public static class ColorExtensions
{
    private static string[] _mainColorsNames = new string[]
    {
        "_Color",
        "_MainColor",
        "_BaseColor",
        "_FaceColor",
    };

    public static Color SetAlfa(this Color color, float alfa) => new Color(color.r, color.g, color.b, alfa);
    public static Color GetMainColor(this Material material)
    {
        foreach (string name in _mainColorsNames)
            if (material.HasProperty(name))
                return material.GetColor(name);

        return Color.white;
    }

    public static void SetMainColor(this Material material, Color color)
    {
        foreach (string name in _mainColorsNames)
            if (material.HasProperty(name))
                material.SetColor(name, color);
    }
}
