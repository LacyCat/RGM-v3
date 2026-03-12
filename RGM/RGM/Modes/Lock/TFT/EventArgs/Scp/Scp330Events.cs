using DAONTFT.Core.Classes;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp330;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using PlayerRoles;
using RGM.API.Features;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAONTFT.Core.EventArgs
{
    public static class Scp330Events
    {
        public static void OnInteractingScp330(InteractingScp330EventArgs ev)
        {
            if (ev.Player.IsScp)
                return;

            Timing.CallDelayed(Timing.WaitForOneFrame, () =>
            {
                if (UnityEngine.Random.Range(1, 21) == 1)
                {
                    ev.Player.AddCandy(CandyKindID.Pink);

                    Timing.CallDelayed(Timing.WaitForOneFrame, () =>
                    {
                        ev.Player.TryRemoveCandу(ev.Candy);
                    });
                }
            });
        }
    }
}
