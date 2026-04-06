using Exiled.Events.EventArgs.Scp330;
using InventorySystem.Items.Usables.Scp330;
using MEC;
using RGM.API.Features;

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
