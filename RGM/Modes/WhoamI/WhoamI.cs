using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using RGM.API;
using Achievements.Handlers;
using Exiled.API.Enums;
using UnityEngine;
using CustomPlayerEffects;
using MultiBroadcast.API;

namespace RGM.Modes
{
    public class WhoamI
    {
        public static WhoamI Instance;

        public void OnEnabled()
        {
            Timing.RunCoroutine(OnModeStarted());
        }

        public IEnumerator<float> OnModeStarted()
        {
            while (true)
            {
                Player.List.ToList().ForEach(x => x.AddBroadcast(1, "1구간 통과"));

                List<RoleTypeId> BlackList = new List<RoleTypeId>()
                {
                    RoleTypeId.Filmmaker,
                    RoleTypeId.Spectator,
                    RoleTypeId.Overwatch,
                    RoleTypeId.Scp079
                };

                Dictionary<Player, List<object>> PlayersInfo = new Dictionary<Player, List<object>>();

                Player.List.ToList().ForEach(x => x.AddBroadcast(1, "2구간 통과"));

                foreach (var player in Player.List.Where(x => !BlackList.Contains(x.Role.Type)))
                {
                    PlayersInfo.Add(player, new List<object>
                    {
                        player.Role.Type,
                        player.MaxHealth,
                        player.Health,
                        player.ActiveEffects,
                        player.Items,
                        player.CurrentItem,
                        player.Position,
                        player.Rotation
                    });
                }

                Player.List.ToList().ForEach(x => x.AddBroadcast(1, "3구간 통과"));

                foreach (var player in Player.List.Where(PlayersInfo.ContainsKey))
                {
                    Player p = Tools.GetRandomValue(PlayersInfo.Keys.ToList());

                    player.Role.Set((RoleTypeId)PlayersInfo[p][0]);
                    player.MaxHealth = (int)PlayersInfo[p][1];
                    player.Health = (int)PlayersInfo[p][2];

                    foreach (var effect in (List<StatusEffectBase>)PlayersInfo[p][3])
                        player.EnableEffect(effect);

                    player.ClearInventory();

                    foreach (var item in (List<Item>)PlayersInfo[p][4])
                        player.AddItem(item.Type);

                    player.CurrentItem = player.Items.ToList().Find(x => x.Type == (ItemType)PlayersInfo[p][5]);
                    player.Position = (Vector3)PlayersInfo[p][5];
                    SLPlayerRotation.Extensions.SetHubRotation(player, (Quaternion)PlayersInfo[p][6]);

                    PlayersInfo.Remove(p);
                }

                Player.List.ToList().ForEach(x => x.AddBroadcast(1, "4구간 통과"));

                yield return Timing.WaitForSeconds(60f);
            }
        }
    }
}
