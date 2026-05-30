using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using RGM.API.Features;

namespace RGM.Modes;

public static class PlayerEffects
{
    public static void DeActivate()
    {
        UnloadEffects();
    }

    internal static void AddEffects()
    {
        try
        {
            foreach (var player in PlayerManager.List.Where(player => !player.IsNonePlayer() && !player.IsDead && !player.IsNPC))
            {
                UnloadEffects();
                player.AddEffect(EffectType.MovementBoost, (byte)(SpeedStore.Count * 2));
                player.AddEffect(EffectType.Scp1853, Math.Min(SpeedStore.Count, (byte) 5));
            }
        }
        catch (Exception e)
        {
            Log.Error($"Error while adding effects, Deception: {e.Message}");
        }
    }

    internal static void UnloadEffects()
    {
        try
        {
            foreach (var player in PlayerManager.List.Where(player =>
                         player != null && !player.IsDead && !player.IsNonePlayer()))
            {
                player.RemoveEffect(EffectType.MovementBoost, 255);
                player.RemoveEffect(EffectType.Scp1853, 5);
            }
        }
        catch (Exception e)
        {
            Log.Error($"Error while removing effects, Deception: {e.Message}");
        }
    }
}