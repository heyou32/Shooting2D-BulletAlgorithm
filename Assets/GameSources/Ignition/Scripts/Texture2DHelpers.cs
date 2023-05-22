using UnityEngine;

public class Texture2DHelpers
{
    public static Color32 GetDominantColor(Texture2D texture)
    {
        var colors = texture.GetPixels32();

        var count = colors.Length;

        float r = 0;
        float g = 0;
        float b = 0;

        for (int i = 0; i < count; i++)
        {
            r += colors[i].r;
            g += colors[i].g;
            b += colors[i].b;
        }

        return new Color32((byte)(r / count), (byte)(g / count), (byte)(b / count), 255);
    }
}