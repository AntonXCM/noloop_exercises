public static class UnsafeMath
{
    static uint a = 1;
    //  Public static INSANE code in the project B)🎸🛹
    public static unsafe float Invert(this float value)
    {
        a = a << 1;
        *(uint*)&value ^= a;
        return value;
    }
}
