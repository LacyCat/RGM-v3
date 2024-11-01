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

namespace RGM.Modes.Abilities.Unique.Scp0492;

[Ability("급식", "최대 체력이 50% 증가합니다.", AbilityCategory.Scp0492, AbilityType.SCP0492_MEALS)]
public class Meals : Ability
{
    public override void OnEnabled()
    {
        Owner.MaxHealth += Owner.MaxHealth / 2;
    }

    public override void OnDisabled()
    {
    }
}
