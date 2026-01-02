using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Rare;

[Ability("도비는 자유에요", "석탄?을 지급받습니다.", AbilityCategory.Rare, AbilityType.RARE_DOBBYISFREE, AbilityHolidayType.Christmas)]
public class DobbyIsFree : Ability
{
    public override void OnEnabled()
    {
        Owner.AddItem(ItemType.SpecialCoal);
    }

    public override void OnDisabled()
    {
    }
}
