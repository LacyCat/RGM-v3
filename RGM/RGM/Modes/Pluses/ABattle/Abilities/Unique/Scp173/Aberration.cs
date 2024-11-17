using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp173;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp173;

[Ability("괴이", "순간이동한 방이 1초 동안 정전됩니다.", AbilityCategory.Scp173, AbilityType.SCP173_ABERRATION)]
public class Aberration : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp173.Blinking += OnBlinking;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp173.Blinking -= OnBlinking;
    }

    public void OnBlinking(BlinkingEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        if (!ev.Player.CurrentRoom.AreLightsOff)
            ev.Player.CurrentRoom.TurnOffLights(1 * ev.Player.AbilityCount(AbilityType.SCP173_ABERRATION));
    }
}
