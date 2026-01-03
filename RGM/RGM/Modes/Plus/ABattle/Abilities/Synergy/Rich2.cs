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

namespace RGM.Modes.Abilities.Synergy;

[RequiresAbility(AbilityType.RARE_GRAPPLINGHOOK, AbilityType.RARE_SPACETRAVEL, AbilityType.RARE_STOPWATCH, AbilityType.RARE_CONTRACT)]
[Ability("부자Ⅱ", "<갈고리, 공간이동, 회중시계, 계약> 랜덤코인 8개를 받으세요.", AbilityCategory.Synergy, AbilityType.SYNERGY_RICH2)]
public class Rich2 : Ability
{
    public override void OnEnabled()
    {
        List<string> uc = UsersManager.UsersCache[Owner.UserId];

        Owner.UserId.SetRC(8 + int.Parse(uc[1]), out string response);
    }

    public override void OnDisabled()
    {
    }
}
