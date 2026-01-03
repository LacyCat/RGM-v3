using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
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

[Ability("응당한 대가", "즉시 55의 경험치를 얻습니다. 대신 20초 동안 전력을 사용할 수 없습니다.", AbilityCategory.Scp079, AbilityType.SCP079_JUSTPRICE)]
public class JustPrice : Ability
{
    public override void OnEnabled()
    {
        IEnumerator<float> enumerator()
        {
            if (Owner.Role is Scp079Role scp079)
                scp079.AddExperience(55);

            for (int i = 0; i < 20; i++)
            {
                if (Owner.Role is Scp079Role scp079_)
                    scp079_.Energy = 0;

                yield return Timing.WaitForSeconds(1f);
            }
        }

        Timing.RunCoroutine(enumerator());
    }

    public override void OnDisabled()
    {
    }
}
