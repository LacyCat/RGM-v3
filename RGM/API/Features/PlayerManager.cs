using Exiled.API.Features;
using PlayerStatsSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGM.API.Features
{
    public static class PlayerManager
    {
        public static void Hit(this Player player, Player attacker, float damage)
        {
            player.Hurt(new DisruptorDamageHandler(new InventorySystem.Items.Firearms.ShotEvents.DisruptorShotEvent(InventorySystem.Items.ItemIdentifier.None, attacker.Footprint, InventorySystem.Items.Firearms.Modules.DisruptorActionModule.FiringState.FiringRapid), player.Position, damage));
        }
    }
}
