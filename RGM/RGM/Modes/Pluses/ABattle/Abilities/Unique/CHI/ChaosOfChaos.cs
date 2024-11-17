using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.CHI;

[Ability("혼돈의 카오스", "SCP-018을 지급받습니다.", AbilityCategory.CHI, AbilityType.CHI_CHAOSOFCHAOS)]
public class ChaosOfChaos : Ability
{
    public override void OnEnabled()
    {
        Item c = Owner.AddItem(ItemType.SCP018);
    }

    public override void OnDisabled()
    {
    }
}
