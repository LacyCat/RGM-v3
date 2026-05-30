using System;
// using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
// using Exiled.Events.EventArgs.Player;
// using InventorySystem.Items.MicroHID.Modules;
// using MEC;
using RGM.API.Features;

namespace RGM.Modes;

public static class PlayerEffects
{
    public static void Activate()
    {
        // Exiled.Events.Handlers.Player.ChangingMicroHIDState += OnChanging;
    }

    public static void DeActivate()
    {
        // Exiled.Events.Handlers.Player.ChangingMicroHIDState -= OnChanging;

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
                player.AddEffect(EffectType.Scp1853, SpeedStore.Count <= 5 ? SpeedStore.Count : 5);
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

    // private static void OnChanging(ChangingMicroHIDStateEventArgs ev)
    // {
    //     Timing.RunCoroutine(Run());
    //     return;  
    //     
    //     IEnumerator<float> Run()
    //     {
    //         while (ev.NewPhase != MicroHidPhase.Firing)
    //         {
    //             if (!(ev.MicroHID.WindUpProgress >= 1))
    //                 ev.MicroHID.WindUpProgress += 0.1f;
    //             else break;
    //             
    //             Log.Info($"Working, The WindUpProgress is {ev.MicroHID.WindUpProgress}" );
    //             
    //             yield return Timing.WaitForSeconds(10.0f - SpeedStore.Count * 0.1f);
    //         }
    //         
    //         ev.MicroHID.WindUpProgress = 0;
    //         yield break;
    //     }
    // }
}