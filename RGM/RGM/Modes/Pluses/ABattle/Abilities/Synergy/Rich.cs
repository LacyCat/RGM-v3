using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MapEditorReborn.API.Features.Objects;
using MapEditorReborn.API.Features.Serializable;
using MapEditorReborn.API.Features;
using MEC;
using RGM.API.Features;
using UnityEngine;
using RGM.API.DataBases;
using Exiled.API.Features.DamageHandlers;

namespace RGM.Modes.Abilities.Synergy;

[RequiresAbility(AbilityType.NORMAL_ESCAPE, AbilityType.NORMAL_MILK, AbilityType.RARE_GRAPPLINGHOOK, AbilityType.RARE_STOPWATCH, AbilityType.RARE_SPACETRAVEL)]
[Ability("부자", "<위기 탈출, 우유, 갈고리, 회중시계, 공간이동> 동전 4개를 모았으므로, RP 4개를 받습니다.", AbilityCategory.Synergy, AbilityType.SYNERGY_RICH)]
public class Rich : Ability
{
    public override void OnEnabled()
    {
        /*todo*/
    }

    public override void OnDisabled()
    {
    }
}
