using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using UnityEngine;

namespace RGM.Modes.Abilities.Normal;

[Ability("단련", "공격력이 20% 추가됩니다.", AbilityCategory.Common, AbilityType.NORMAL_TRAINING)]
public class Training : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.Hurting += OnHurting;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Hurting -= OnHurting;
    }

    public void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        ev.DamageHandler.Damage += ev.DamageHandler.Damage * 0.2f;
    }
}
