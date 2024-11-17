using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp0492;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp0492;

[Ability("당혹감", "목표물로 삼은 인간은 실명합니다.", AbilityCategory.Scp0492, AbilityType.SCP0492_CONFUSION)]
public class Confusion : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp0492.TriggeringBloodlust += OnTriggeringBloodlust;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp0492.TriggeringBloodlust -= OnTriggeringBloodlust;
    }

    public void OnTriggeringBloodlust(TriggeringBloodlustEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        ev.Target.EnableEffect(EffectType.Blinded, 1, 0.5f);
    }
}
