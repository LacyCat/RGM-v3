using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp106;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp939;

[Ability("실명", "섬광탄 효과를 반감시킵니다.", AbilityCategory.Scp939, AbilityType.SCP939_NOEYES)]
public class NoEyes : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.ReceivingEffect += OnReceivingEffect;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.ReceivingEffect -= OnReceivingEffect;
    }

    public IEnumerator<float> OnReceivingEffect(ReceivingEffectEventArgs ev)
    {
        if (ev.Player != Owner)
            yield break;

        yield return Timing.WaitForOneFrame;

        if (ev.Effect.GetEffectType() == EffectType.Flashed)
        {
            ev.Effect.ServerChangeDuration(ev.Effect.Duration * 0.5f);
        }
    }
}
