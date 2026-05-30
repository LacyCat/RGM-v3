using System;
using Exiled.API.Features;

namespace RGM.Modes.Patches;

public static class ScpPatch
{
    private const float Scp049ResurrectDuration = 7;

    public static void Scp049Postfix(ref float __result)
    {
        try
        {
            __result = Math.Max(0.1f, Scp049ResurrectDuration - SpeedStore.Count * .1f);
        }
        catch (Exception e)
        {
            Log.Error($"[KoreanSpeed/Scp049Postfix] Exception: {e}");
        }
    }
    
    
}