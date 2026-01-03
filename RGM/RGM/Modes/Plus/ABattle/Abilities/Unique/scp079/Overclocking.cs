using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp079;
using Exiled.Events.EventArgs.Scp106;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using PlayerRoles;
using RGM.API.DataBases;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Unique.Scp079;

[Ability("오버클럭", "1초마다 전력을 0.2 얻습니다.", AbilityCategory.Scp079, AbilityType.SCP079_OVERCLOCKING)]
public class Overclocking : Ability
{
    public override void OnEnabled()
    {
        IEnumerator<float> enumerator()
        {
            while (Owner.Role.Type == RoleTypeId.Scp079)
            {
                if (Owner.Role is Scp079Role scp079)
                    scp079.Energy += 0.2f;

                yield return Timing.WaitForSeconds(1f);
            }
        }

        Timing.RunCoroutine(enumerator());
    }

    public override void OnDisabled()
    {

    }
}
