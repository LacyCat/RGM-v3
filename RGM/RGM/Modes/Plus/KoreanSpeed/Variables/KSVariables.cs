namespace RGM.Modes;

public class SpeedStore
{
    public static byte Count;
    public static bool isEnabled = false;
    public static void Clear()
    {
        Count = 0;
    }
}