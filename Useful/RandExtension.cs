namespace Useful;

public static class RandExtension 
{
    public static double DoubleUniform(this Random r, double max) => r.DoubleUniform(0, max);
    public static double DoubleUniform(this Random r, double min, double max)
    {
        if (min > max) { return r.DoubleUniform(max, min); }
        return r.NextDouble() % (max - min) + min;
    }

    public static double IntUniform(this Random r, int min, int max)
    {
        if (min > max) { return r.DoubleUniform(max, min); }
        return r.Next() % (max - min) + min;
    }
}