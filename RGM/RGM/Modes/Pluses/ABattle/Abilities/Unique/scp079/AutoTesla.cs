using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp079;
using Exiled.Events.EventArgs.Scp106;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp079;

[Ability("자동 방어 시스템", "테슬라가 가끔씩 자동으로 작동됩니다.", AbilityCategory.Scp079, AbilityType.SCP079_AUTOTESLA)]
public class AutoTesla : Ability
{
    CoroutineHandle _autoTesla;

    public override void OnEnabled()
    {
        _autoTesla = Timing.RunCoroutine(autoTesla());
    }

    public override void OnDisabled()
    {
        Timing.KillCoroutines(_autoTesla);
    }

    IEnumerator<float> autoTesla()
    {
        while (true)
        {
            LabApi.Features.Wrappers.Tesla tesla = LabApi.Features.Wrappers.Tesla.List.GetRandomValue();
            tesla.Trigger();
            tesla.InstantTrigger();

            yield return Timing.WaitForSeconds(UnityEngine.Random.Range(1, 60));
        }
    }
}
