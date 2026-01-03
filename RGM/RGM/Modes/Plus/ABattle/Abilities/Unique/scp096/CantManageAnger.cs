using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp096;

[Ability("분노 조절 문제", "분노 시간에 제한이 사라집니다.", AbilityCategory.Scp096, AbilityType.SCP096_CANTMANAGEANGER)]
public class CantManageAnger : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp096.Enraging += OnEnraging;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp096.Enraging -= OnEnraging;
    }

    public IEnumerator<float> OnEnraging(Exiled.Events.EventArgs.Scp096.EnragingEventArgs ev)
    {
        yield return Timing.WaitForOneFrame;

        ev.Scp096.EnrageCooldown = 0;
        ev.Scp096.EnragedTimeLeft = 99999;
        ev.Scp096.SprintingSpeed = 500;
    }
}
