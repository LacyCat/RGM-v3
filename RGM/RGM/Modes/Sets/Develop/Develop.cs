using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using Mirror;
using MultiBroadcast;
using MultiBroadcast.API;
using PlayerRoles;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes
{
    [Mode(ModeCategory.Private, ModeInfo.Set, ModeType.Develop)]
    class Develop : Mode
    {
        public override string Name => "개발";
        public override string Description => "개발!";
        public override string Detail =>
"""
test
""";
        public override string Color => "FFFFFF";

        public static Develop Instance;

        public override void OnEnabled()
        {
            Round.IsLocked = true;
            Respawn.PauseWaves();
        }

        public IEnumerator<float> OnModeStarted()
        {
            foreach (var player in Player.List)
                player.Role.Set(RoleTypeId.Tutorial);

            yield break;
        }
    }
}
