using Exiled.API.Features.Items;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using MultiBroadcast.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGM.EventArgs
{
    public static class Scp330Events
    {
        public static void OnInteractingScp330(Exiled.Events.EventArgs.Scp330.InteractingScp330EventArgs ev)
        {
            if (ev.Player.IsScp)
                return;

            Timing.CallDelayed(Timing.WaitForOneFrame, () =>
            {
                if (UnityEngine.Random.Range(1, 21) == 1)
                {
                    ev.Scp330.RemoveCandy(ev.Candy);

                    ev.Player.TryAddCandy(CandyKindID.Pink);
                }
            });
        }
    }
}
