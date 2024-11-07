using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomRendering;
using Exiled.API.Features;
using MEC;
using Mirror;
using RGM.API.Features;
using RGM.API.DataBases;
using UnityEngine;
using Exiled.API.Features.Items;
using Exiled.API.Enums;

namespace RGM.Modes
{
    [Mode(ModeCategory.OnlySub, ModeInfo.Plus, ModeType.Spooky)]
    class Spooky : Mode
    {
        public override string Name => "Spooky!";
        public override string Description => "시작 시 영구적인, 랜덤한 할로윈 효과를 받으세요!";
        public override string Detail =>
"""
댜음 효과 중 하나의 효과가 영구적으로 적용됩니다.

SugarRush
SugarHigh
SugarCrave
Spicy
OrangeCandy
OrangeWitness
Metal
Marshmallow
Ghostly
Prismatic
OrangeWitness
""";
        public override string Color => "3104B4";

        public static Spooky Instance;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Spawned += OnSpawned;

            Timing.RunCoroutine(OnModeStarted());
        }

        public IEnumerator<float> OnModeStarted()
        {
            foreach (var player in Player.List)
            {
                Spawned(player);
            }

            yield break;
        }

        public void OnSpawned(Exiled.Events.EventArgs.Player.SpawnedEventArgs ev)
        {
            Spawned(ev.Player);
        }

        public void Spawned(Player player)
        {
            player.EnableEffect(Tools.GetRandomValue(Datas.FunnyEffects));
        }
    }
}
