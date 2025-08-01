using Godot;

public static class ColorLerp
{
    public static Color OkHsl(Color a, Color b, float t)
    {
        return Color.FromOkHsl(
            Mathf.Lerp(a.OkHslH, b.OkHslH, t),
            Mathf.Lerp(a.OkHslS, b.OkHslS, t),
            Mathf.Lerp(a.OkHslL, b.OkHslL, t)
            );
    }
}
