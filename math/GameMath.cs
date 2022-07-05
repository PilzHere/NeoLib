using Raylib_cs;

namespace NeoLib.math;

public static class GameMath
{
    public static bool MaskContainsBits(int mask, int bits)
    {
        return (mask & bits) == bits;
    }

    public static bool IsEven(int number)
    {
        return ((number & 1) != 1);
    }

    public static bool IsOdd(int number)
    {
        return ((number & 1) == 1);
    }

    public static float Limit(float number, float minimum, float maximum)
    {
        if (number > maximum)
            number = maximum;
        else if (number < minimum)
            number = minimum;

        return number;
    }

    public static bool InRange(float number, float minimum, float maximum)
    {
        if (number > minimum && number < maximum)
            return true;

        return false;
    }
}