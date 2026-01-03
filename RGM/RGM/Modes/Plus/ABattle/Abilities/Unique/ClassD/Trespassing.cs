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

namespace RGM.Modes.Abilities.Unique.ClassD;

[Ability("주거침입죄", "SCP-268을 지급받습니다.", AbilityCategory.ClassD, AbilityType.CLASSD_TRESPASSING)]
public class Trespassing : Ability
{
    public override void OnEnabled()
    {
        Owner.AddItem(ItemType.SCP268);
    }

    public override void OnDisabled()
    {
    }
}
