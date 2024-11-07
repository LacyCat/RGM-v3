using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using MEC;

namespace RGM.Modes
{
    [Mode(ModeCategory.Public, ModeInfo.Plus, ModeType.Collector)]
    public class Collector : Mode
    {
        public override string Name => "수집가";
        public override string Description => "SCP 아이템 3개를 가지고 시작합니다.";
        public override string Detail =>
"""
이름에 <color=#FE2E2E>SCP</color>가 들어가는 모든 아이템 중에서 랜덤으로 3개를 지급받고 시작합니다.

<color=#FE2E2E>SCP</color>의 경우에는 하나의 <b><color=#FE2E2E>SCP</color> 아이템</b>만 지급받습니다.
""";
        public override string Color => "FFBF00";

        public static Collector Instance;

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
            List<ItemType> ScpItemList = new List<ItemType>()
            {
                ItemType.SCP018,
                ItemType.SCP268,
                ItemType.SCP207,
                ItemType.SCP500,
                ItemType.SCP1576,
                ItemType.SCP1853,
                ItemType.SCP2176,
                ItemType.SCP244a,
                ItemType.SCP244b,
                ItemType.SCP330,
                ItemType.AntiSCP207
            };


            for (int i=1; i<4; i++)
            {
                Item CurrentItem = player.AddItem(ScpItemList[UnityEngine.Random.Range(0, ScpItemList.Count())]);

                if (player.IsScp)
                {
                    player.CurrentItem = CurrentItem;
                }
            }
        }
    }
}
