using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

using static RGM.Variables.ServerManagers;

namespace RGM.Modes.Abilities.Epic;

[Ability("성직자", "관전석에서 3명을 뽑아 아군으로 만들고, 자신의 위치로 소환합니다.", AbilityCategory.Epic, AbilityType.EPIC_PRIEST)]
public class Priest : Ability
{
    public override void OnEnabled()
    {
        for (int i = 0; i < 3; i++)
        {
            var dead = Player.List.Where(x => x.IsDead).ToList();

            if (dead.Count() != 0)
            {
                var revive = dead.GetRandomValue();

                revive.Role.Set(Owner.Role.Type);
                revive.Position = Owner.Position;
            }
        }
    }

    public override void OnDisabled()
    {

    }
}
