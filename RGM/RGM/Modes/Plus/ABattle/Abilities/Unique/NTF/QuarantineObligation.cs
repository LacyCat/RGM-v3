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

namespace RGM.Modes.Abilities.Unique.NTF;

[Ability("격리 의무자", "고폭 수류탄과 섬광탄을 지급받습니다.", AbilityCategory.NTF, AbilityType.NTF_QUARANTINEOBLIGATION)]
public class QuarantineObligation : Ability
{
    public override void OnEnabled()
    {
        List<ItemType> ContainDuty = new List<ItemType>()
        {
            ItemType.GrenadeFlash,
            ItemType.GrenadeHE
        };

        foreach (var item in ContainDuty)
            Owner.AddItem(item);
    }

    public override void OnDisabled()
    {
    }
}
