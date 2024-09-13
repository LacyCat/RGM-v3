using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using MEC;
using RGM.API;
using UnityEngine;

namespace RGM.Modes
{
    public class Distancing
    {
        public static Distancing Instance;

        public void OnEnabled()
        {
            Timing.RunCoroutine(OnModeStarted());
        }

        public IEnumerator<float> OnModeStarted()
        {
            while (true)
            {
                List<Player> DamagePlayers = new List<Player>();

                foreach (var p1 in Player.List)
                {
                    foreach (var p2 in Player.List)
                    {
                        if (p1 != p2 && Vector3.Distance(p1.Position, p2.Position) < 5f)
                        {
                            if (!DamagePlayers.Contains(p1))
                                DamagePlayers.Add(p1);

                            if (!DamagePlayers.Contains(p2))
                                DamagePlayers.Add(p2);
                        }
                    }
                }

                foreach (var player in DamagePlayers)
                {
                    player.Health -= player.MaxHealth / 50;

                    if (player.Health < 1)
                        player.Kill("바이러스가 당신을 끝장냈습니다.");
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}