namespace Useful;

public static class RandExtension 
{
    public static float NextFloat(this Random r) => r.NextSingle();

    public static float NextFloat(this Random r, float max) => r.FloatUniform(0, max);
    public static float FloatUniform(this Random r, float min, float max)
    {
        if (min > max) { return r.FloatUniform(max, min); }
        var tmp = r.NextFloat();
        return tmp * (max - min) + min;
    }

    public static int NextInt(this Random r, int max) => r.IntUniform(0, max);
    /// <summary>
    /// [min, max]
    /// </summary>
    public static int IntUniform(this Random r, int min, int max)
    {
        if (min > max) { return r.IntUniform(max, min); }
        return r.Next() % (max - min + 1) + min;
    }
}