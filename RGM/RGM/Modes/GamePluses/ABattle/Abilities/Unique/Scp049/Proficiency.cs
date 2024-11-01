using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp049;

[Ability("능수능란", "좀비 소생 시간이 50% 줄어듭니다.", AbilityCategory.Scp049, AbilityType.SCP049_PROFICIENCY)]
public class Proficiency : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp049.StartingRecall += OnStartingRecall;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp049.StartingRecall -= OnStartingRecall;
    }

    public void OnStartingRecall(Exiled.Events.EventArgs.Scp049.StartingRecallEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        Timing.CallDelayed(0.1f, () =>
        {
            ev.Scp049.RemainingCallDuration /= 2;
        });
    }
}
