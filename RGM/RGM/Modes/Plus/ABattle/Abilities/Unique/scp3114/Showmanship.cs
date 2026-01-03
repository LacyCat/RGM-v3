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
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp3114;

[Ability("쇼맨쉽", "춤추는 동안 지속적인 데미지를 받습니다. 대신 외부로부터 받는 데미지를 66% 줄입니다. <size=10>idea by 조용히게임함</size>", AbilityCategory.Scp3114, AbilityType.SCP3114_SHOWMANSHIP)]
public class Showmanship : Ability
{
    CoroutineHandle coroutine;

    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.Hurting += OnHurting;

        coroutine = Timing.RunCoroutine(onStarted());
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Hurting -= OnHurting;

        Timing.KillCoroutines(coroutine);
    }

    public IEnumerator<float> onStarted()
    {
        while (true)
        {
            Scp3114Role scp3114 = (Scp3114Role)Owner.Role;

            if (scp3114.Dance)
            {
                Owner.Hit(Owner, 0.2f);
            }

            yield return Timing.WaitForSeconds(0.1f);
        }
    }

    public void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        Scp3114Role scp3114 = (Scp3114Role)ev.Player.Role;

        if (scp3114.Dance)
        {
            ev.DamageHandler.Damage /= 3;
        }
    }
}
