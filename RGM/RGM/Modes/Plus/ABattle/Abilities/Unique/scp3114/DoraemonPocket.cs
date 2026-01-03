using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp106;
using Exiled.Events.EventArgs.Scp3114;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp3114;

[Ability("도라에몽 주머니", "변신을 해제할 때마다 아이템을 하나 지급받습니다.", AbilityCategory.Scp3114, AbilityType.SCP3114_DORAEMONPOCKET)]
public class DoraemonPocket : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp3114.Revealed += OnRevealed;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp3114.Revealed -= OnRevealed;
    }

    public void OnRevealed(RevealedEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        List<ItemType> ItemTypes = Tools.EnumToList<ItemType>();

        Item Item = ev.Player.AddItem(Tools.GetRandomValue(ItemTypes));

        ev.Player.CurrentItem = Item;
    }
}
