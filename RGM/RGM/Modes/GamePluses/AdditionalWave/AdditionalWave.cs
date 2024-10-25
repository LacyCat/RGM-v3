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
using PlayerRoles;

namespace RGM.Modes
{
    class AdditionalWave
    {
        public static AdditionalWave Instance;

        public void OnEnabled()
        {
            Exiled.Events.Handlers.Server.RespawningTeam += OnRespawningTeam;
        }

        public void OnRespawningTeam(Exiled.Events.EventArgs.Server.RespawningTeamEventArgs ev)
        {
            Timing.CallDelayed(0.1f, () => 
            {
                switch (UnityEngine.Random.Range(1, 6))
                {
                    case 1:
                        Server.ExecuteCommand($"/fc {string.Join(".", Player.List.Where(x => x.IsDead && x.Role.Type != RoleTypeId.Overwatch).Select(x => x.Id))}. Scp0492");
                        break;

                    case 2:
                        ABattleFunctions.SpecificAbilities.CallSnakeHand(null, Player.List.Where(x => x.IsDead && x.Role.Type != RoleTypeId.Overwatch).ToList());
                        break;

                    case 3:
                        break;

                    case 4:
                        break;
                }
            });

            for (int i = 1; i < 4; i++)
            {
                foreach (var player in ev.Players)
                {
                    if (UnityEngine.Random.Range(1, 4) == 1)
                        player.AddItem(Tools.GetRandomValue(Tools.EnumToList<ItemType>()));
                }
            }
        }
    }
}
