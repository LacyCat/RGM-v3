using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Pickups;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Synergy;

[RequiresAbility(AbilityType.RARE_BOMBERMAN, AbilityType.EPIC_SUICIDEBOMBER)]
[Ability("수어사이드 스쿼드", "<봄버맨, 수어사이드 봄버맨> 사망할 경우 SCP-018를 3개 떨어트립니다.", AbilityCategory.Synergy, AbilityType.SYNERGY_SUICIDESQUAD)]
public class SuicideSquad : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.Died += OnDied;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Died -= OnDied;
    }

    public void OnDied(DiedEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        for (int i = 0; i < 3; i++)
        {
            var scp018 = Pickup.Create(ItemType.SCP018);
            scp018.Spawn(Owner.Position, Quaternion.identity);
        }
    }
}
