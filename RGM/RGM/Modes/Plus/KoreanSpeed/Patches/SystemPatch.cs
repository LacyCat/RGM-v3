using System;
using Exiled.API.Features;

namespace RGM.Modes.Patches;

public static class SystemPatch
{
    public static void Scp1853MaxIntensityPostfix(ref byte __result)
    {
        try
        {
            __result = 100;
        }
        catch (Exception e)
        {
            Log.Error($"[KoreanSpeed/Scp1853MaxIntensityPostfix] Exception: {e}");
        }
    }
}