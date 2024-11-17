using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp049;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp049;

[Ability("유능한 의사", "소생된 좀비의 체력이 50% 추가됩니다.", AbilityCategory.Scp049, AbilityType.SCP049_COMPETENTDOCTOR)]
public class CompetentDoctor : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp049.FinishingRecall += OnFinishingRecall;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp049.FinishingRecall -= OnFinishingRecall;
    }

    public void OnFinishingRecall(FinishingRecallEventArgs ev)
    {
        if (ev.Player != Owner)
            return;
        
        Timing.CallDelayed(0.1f, () =>
        {
            ev.Target.MaxHealth *= 3 / 2;
            ev.Target.Health = ev.Target.MaxHealth;
        });
    }
}
