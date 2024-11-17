using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Features;
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

[Ability("간이 충전기", "즉시 <b>현재 레벨 x 20</b>만큼 경험치를 받습니다.", AbilityCategory.Scp079, AbilityType.SCP079_PORTABLECHARGER)]
public class PortableCharger : Ability
{
    public override void OnEnabled()
    {
        if (Owner.Role is Scp079Role scp079)
            scp079.AddExperience(20 * scp079.Level);
    }

    public override void OnDisabled()
    {
    }
}
