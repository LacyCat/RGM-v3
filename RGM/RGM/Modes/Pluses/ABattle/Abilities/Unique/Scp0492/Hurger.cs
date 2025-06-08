using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp0492;

[Ability("허기", "인간을 섭취할 때 얻는 회복량이 100HP 추가됩니다.", AbilityCategory.Scp0492, AbilityType.SCP0492_HUNGER)]
public class Hurger : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp0492.ConsumingCorpse += OnConsumingCorpse;
    }
    
    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp0492.ConsumingCorpse -= OnConsumingCorpse;
    }

    public void OnConsumingCorpse(Exiled.Events.EventArgs.Scp0492.ConsumingCorpseEventArgs ev)
    {
        if (ev.Player != Owner || !ev.IsAllowed)
            return;

        Timing.CallDelayed(0.1f, () =>
        {
            ev.Player.Health += 100;
        });
    }
}
