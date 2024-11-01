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

namespace RGM.Modes.Abilities.Unique.Tutorial;

[Ability("SCP 연구자", "SCP 아이템 중 하나를 지급받습니다.", AbilityCategory.Tutorial, AbilityType.TUTORIAL_RESEARCHER)]
public class Researcher : Ability
{
    public override void OnEnabled()
    {
        List<ItemType> SCPItems = new List<ItemType>()
        {
            ItemType.SCP018,
            ItemType.SCP1576,
            ItemType.SCP1853,
            ItemType.SCP207,
            ItemType.SCP2176,
            ItemType.SCP244a,
            ItemType.SCP244b,
            ItemType.SCP268,
            ItemType.SCP330,
            ItemType.SCP500,
            ItemType.AntiSCP207
        };

        Item SCPItem = Owner.AddItem(Tools.GetRandomValue(SCPItems));

        if (Owner.IsScp)
            Owner.CurrentItem = SCPItem;
    }

    public override void OnDisabled()
    {
    }
}
