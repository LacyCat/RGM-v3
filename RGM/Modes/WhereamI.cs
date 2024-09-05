using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using MEC;
using UnityEngine;

namespace RGM.Modes
{
    public class WhereamI
    {
        public static WhereamI Instance;

        public void OnEnabled()
        {
            Timing.RunCoroutine(OnModeStarted());
        }

        public IEnumerator<float> OnModeStarted()
        {
            foreach (var player in Player.List)
            {
                Door SelectedDoor = Door.List.ToList()[UnityEngine.Random.Range(0, Door.List.Count())];
                player.Position = new Vector3(SelectedDoor.Position.x, SelectedDoor.Position.y + 2, SelectedDoor.Position.z);
            }

            yield break;
        }
    }
}
