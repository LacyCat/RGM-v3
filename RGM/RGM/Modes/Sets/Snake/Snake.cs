using Exiled.API.Features.Items;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using MEC;
using UnityEngine;
using Exiled.API.Features.Roles;
using Exiled.API.Enums;
using PlayerRoles;
using MultiBroadcast.API;
using RGM.API.Features;
using Mirror;
using Respawning;

using static RGM.Variables.ServerManagers;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Player;
using Exiled.API.Extensions;
using Exiled.API.Features.Pickups;

namespace RGM.Modes
{
    [Mode(ModeCategory.Private, ModeInfo.Set, ModeType.Snake)]
    public class Snake : Mode
    {
        public override string Name => "스네이크";
        public override string Description => "가장 높은 점수를 달성한 유저가 우승합니다.";
        public override string Detail =>
    """
    한번 죽으면 그 즉시 점수가 기록되고 기회가 주어지지 않습니다.
    
    행운을 빕니다.
    """;
        public override string Color => "3EA724";

        public static Snake Instance;

        public override void OnEnabled()
        {
            Server.FriendlyFire = true;
            Round.IsLocked = true;
            Respawn.PauseWaves();

            Timing.CallDelayed(1, () => 
            {
                Timing.RunCoroutine(OnModeStarted());
            });
        }

        public IEnumerator<float> OnModeStarted()
        {
            foreach (var player in Player.List)
            {
                player.Role.Set(RoleTypeId.Tutorial);
            }

            yield break;
        }
    }
}
