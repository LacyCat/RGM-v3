using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using RGM.API.Features;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Exiled.API.Enums;
using Exiled.API.Features.Roles;

namespace RGM.Modes.Abilities.Mythic;

[Ability("철퇴 자크", "점프하는 순간 일대가 초토화됩니다.", AbilityCategory.Mythic, AbilityType.MYTHIC_HAMMER, AbilityHolidayType.Halloween)]
public class Hammer : Ability
{
    public override void OnEnabled()
    {
        Owner.AddEffect(EffectType.Metal, 255);
        Owner.AddEffect(EffectType.Lightweight, 255);
    }

    public override void OnDisabled()
    {
    }
}