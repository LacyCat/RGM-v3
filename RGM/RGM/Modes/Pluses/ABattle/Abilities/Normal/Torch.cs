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

namespace RGM.Modes.Abilities.Normal;

[Ability("횃불", "랜턴과 아드레날린을 받습니다.", AbilityCategory.Common, AbilityType.NORMAL_TORCH)]
public class Torch : Ability
{
    public override void OnEnabled()
    {
        Owner.AddItem(ItemType.Lantern);
        Owner.AddItem(ItemType.Adrenaline);
    }

    public override void OnDisabled()
    {
    }
}
