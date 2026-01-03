using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using ProjectMER.Features.Objects;
using ProjectMER.Features.Serializable;
using ProjectMER.Features;
using MEC;
using RGM.API.Features;
using UnityEngine;
using RGM.API.DataBases;
using Exiled.API.Features.DamageHandlers;

namespace RGM.Modes.Abilities.Synergy;

[RequiresAbility(AbilityType.RARE_DND, AbilityType.RARE_DND, AbilityType.RARE_DND)]
[Ability("AFK", "<자리 비움 x3> 게임 하고 싶은 거 맞죠..? 용기가 가상하시니 선물을 드릴게요.", AbilityCategory.Synergy, AbilityType.SYNERGY_AFK)]
public class AFK : Ability
{
    public override void OnEnabled()
    {
        Owner.AddAhp(1205, 1205, 0);
    }

    public override void OnDisabled()
    {
    }
}
