using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using PlayerRoles;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Legend;

[Ability("섬뜩한 힘", "모든 SCP-330의 효과가 일괄적으로 적용됩니다. 체력을 200 얻습니다.", AbilityCategory.Legend, AbilityType.LEGEND_CANDYPOWER, AbilityHolidayType.Halloween)]
public class CandyPower : Ability
{
    public override void OnEnabled()
    {
        var effects = new List<EffectType>
        {
            EffectType.SugarRush,
            EffectType.Prismatic,
            EffectType.OrangeCandy,
            EffectType.WhiteCandy,
            EffectType.Metal,
            EffectType.MovementBoost,
            EffectType.Spicy,
        };

        foreach (var effect in effects)
        {
            Owner.AddEffect(effect, 50);
        }

        Owner.MaxHealth += 200;
        Owner.Health += 200;
    }

    public override void OnDisabled() 
    {
    }
}
