using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using PlayerRoles;
using RGM.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerStatsSystem;

namespace RGM.Donator
{
    public class Main
    {
        public void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Dying += OnDying;
        }

        public void OnDying(Exiled.Events.EventArgs.Player.DyingEventArgs ev)
        {
            if (ev.Attacker != null)
            {
                List<string> Attacker = DonatorsManager.UsersCache[ev.Attacker.UserId];

                if (Attacker[0] != "0")
                {
                    if (Attacker[0] == "영혼 가출")
                    {
                        DamageHandlerBase DisruptorDamage = new DisruptorDamageHandler(ev.Attacker.Footprint, -1);

                        Ragdoll.CreateAndSpawn(ev.Player.Role.Type, Attacker[0], DisruptorDamage, ev.Player.Position, ev.Player.Rotation);
                    }
                }
            }
        }
    }
}
