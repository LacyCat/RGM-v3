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

namespace RGM.Modes.Abilities.Epic;

[Ability("!!마쉬멜로우!!", "즉시 마쉬멜로우맨이 됩니다. 체력은 500이며, \"경공\" 능력을 2개 얻습니다.", AbilityCategory.Epic, AbilityType.EPIC_MARSHMELLOW, AbilityHolidayType.Halloween)]
public class MarshMellow : Ability
{
    public override void OnEnabled()
    {
        Owner.MaxHealth += 500;
        Owner.Health += 500;
        Owner.AddAbility(AbilityType.NORMAL_SWIFT);
        Owner.AddAbility(AbilityType.NORMAL_SWIFT);
        Owner.AddItem(ItemType.MarshmallowItem);
    }

    public override void OnDisabled()
    {
    }
}
