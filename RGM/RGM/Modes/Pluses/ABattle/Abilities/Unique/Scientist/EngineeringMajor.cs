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

[Ability("공학 전공", "SCP-2176을 지급받습니다.", AbilityCategory.Scientist, AbilityType.SCIENTIST_ENGINEERINGMAJOR)]
public class EngineeringMajor : Ability
{
    public override void OnEnabled()
    {
        Owner.AddItem(ItemType.SCP2176);
    }

    public override void OnDisabled()
    {
    }
}
