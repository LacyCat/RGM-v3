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

namespace RGM.Modes.Abilities.Unique.Scientist;

[Ability("05 평의회", "05등급 키카드를 지급받습니다.", AbilityCategory.Scientist, AbilityType.SCIENTIST_05)]
public class Level05 : Ability
{
    public override void OnEnabled()
    {
        Owner.AddItem(ItemType.KeycardO5);
    }

    public override void OnDisabled()
    {
    }
}
