using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp079;
using Exiled.Events.EventArgs.Scp106;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp079;

[Ability("기동타격대", "테슬라로 소모되는 에너지가 20 감소합니다.", AbilityCategory.Scp079, AbilityType.SCP079_MOBILESTRIKEFORCE)]
public class MobileStrikeForce : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp079.InteractingTesla += OnInteractingTesla;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp079.InteractingTesla -= OnInteractingTesla;
    }

    public void OnInteractingTesla(InteractingTeslaEventArgs ev)
    {
        if (Owner != ev.Player)
            return;

        ev.AuxiliaryPowerCost -= 20;
    }
}
