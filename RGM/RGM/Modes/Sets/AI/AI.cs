using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPlayerEffects;
using CustomRendering;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using MapEditorReborn.API.Features.Objects;
using MEC;
using Mirror;
using MultiBroadcast.API;
using PlayerRoles;
using UnityEngine;
using Exiled.API.Enums;
using RGM.API.Features;

using static RGM.Variables.ServerManagers;
using RGM.API.DataBases;
using Respawning;
using PlayerRoles.FirstPersonControl;

namespace RGM.Modes
{
    [Mode(ModeCategory.Public, ModeInfo.Set, ModeType.AI)]
    class AI : Mode
    {
        public override string Name => "섬뜩한 힘";
        public override string Description => "SCP들이 전부 A.I.로 대체되고 최대 체력이 10배로 조정됩니다. (최대 4개체)";
        public override string Detail =>
"""
<b>[A.I. 리스트]</b>
<color=red>SCP-173</color>
<color=red>SCP-049</color>
<color=red>SCP-096</color>
<color=red>SCP-106</color>
""";
        public override string Color => "7401DF";

        public static AI Instance;

        public override void OnEnabled()
        {
            Timing.RunCoroutine(OnModeStarted());
            Timing.RunCoroutine(BlockFalldown());
        }

        public IEnumerator<float> OnModeStarted()
        {
            foreach (var p in Player.List.Where(x => x.IsScp))
                p.Role.Set(RoleTypeId.ClassD);

            foreach (var aiscprole in Datas.AIRoles)
            {
                Server.ExecuteCommand($"/spawnai {aiscprole.ToString()}");

                yield return Timing.WaitForSeconds(0.1f);
            }

            Timing.CallDelayed(1, () =>
            {
                foreach (var ai in Player.List.Where(x => x.IsScp))
                {
                    ai.MaxHealth *= 10;
                    ai.Health = ai.MaxHealth;
                }
            });

            yield break;
        }

        public IEnumerator<float> BlockFalldown()
        {
            while (true)
            {
                foreach (var ai in Player.List.Where(x => x.IsNPC))
                {
                    if (ai.Position.y < -2000)
                        ai.Position = Tools.GetRandomValue(Player.List.Where(x => x.IsNPC && x != ai).Select(x => x.Position).ToList());
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
