using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomRendering;
using Exiled.API.Features;
using MEC;
using Mirror;
using UnityEngine;

namespace RGM.Modes
{
    class SoulMate
    {
        public static SoulMate Instance;

        private Dictionary<Player, Player> soulMates = new Dictionary<Player, Player>();

        public void OnEnabled()
        {
            Exiled.Events.Handlers.Player.ItemAdded += OnItemAdded;
            Exiled.Events.Handlers.Player.ItemRemoved += OnItemRemoved;
            Exiled.Events.Handlers.Player.Hurt += OnHurt;

            Timing.RunCoroutine(OnModeStarted());

            Exiled.Events.Handlers.Player.Died += OnDied;
        }

        public IEnumerator<float> OnModeStarted()
        {
            yield return Timing.WaitForSeconds(10f);

            Timing.RunCoroutine(SoulMateManager());

            yield return Timing.WaitForSeconds(5f);

            while (true)
            {
                foreach (var player in Player.List)
                {
                    /*
                    if (soulMates.ContainsKey(player) && soulMates[player] != null && soulMates[player].IsAlive)
                        player.ShowHint($"당신의 단짝은 <b>{soulMates[player].CurrentRoom.Name}</b>에 있습니다.", 1.2f);
                    */

                    if (Player.List.ToList().Where(x => x.IsAlive).Count() < 3 && player.Role.Type != PlayerRoles.RoleTypeId.Tutorial)
                    {
                        Player.List.ToList().Where(x => x.IsAlive).ToList().ForEach(x => Server.ExecuteCommand($"/fc {x.Id} Tutorial 1"));
                        Player.List.ToList().ForEach(x => x.Broadcast(15, $"<size=30><b>{(Player.List.ToList().Where(x => x.IsAlive).Count() == 2 ? "<color=#ffd700>소울메이트</color>" : "<color=#BFFF00>외톨이</color>")}</b>({string.Join(", ", Player.List.ToList().Where(x => x.IsAlive).Select(x => x.DisplayNickname))})의 승리입니다!</size>"));
                        yield return Timing.WaitForSeconds(100f);
                    }
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }

        public IEnumerator<float> SoulMateManager()
        {
            while (true)
            {
                List<Player> players = Player.List.ToList();

                players.ShuffleList();

                foreach (var player in players)
                {
                    if (player.IsDead)
                    {
                        if (soulMates.ContainsKey(player))
                            soulMates.Remove(player);
                    }
                    else
                    {
                        if (!soulMates.ContainsKey(player))
                        {
                            Player soulMate = players.Where(x => x.IsAlive && !soulMates.ContainsValue(x) && x != player).FirstOrDefault();

                            if (soulMate != null)
                            {
                                soulMates.Add(player, soulMate);
                                soulMates.Add(soulMate, player);
                            }
                        }
                    }

                    yield return Timing.WaitForSeconds(10f);
                }
            }
        }

        public void OnDied(Exiled.Events.EventArgs.Player.DiedEventArgs ev)
        {
            if (soulMates.ContainsKey(ev.Player))
            {
                Player soulMate = soulMates[ev.Player];

                if (soulMate != null && soulMate.IsAlive)
                {
                    soulMate.Kill($"{ev.Player.DisplayNickname}(와)과 {soulMate.DisplayNickname}(은)는 영혼의 단짝이였습니다.");
                    ev.Player.Kill($"{soulMate.DisplayNickname}(와)과 {ev.Player.DisplayNickname}(은)는 영혼의 단짝이였습니다.");
                    Server.ExecuteCommand($"/cassie_sl <color=red>{ev.Attacker.DisplayNickname}</color>(이)가 영혼의 단짝이였던 <color=#5858FA>{ev.Player.DisplayNickname}</color>와(과) <color=#FE2EF7>{soulMate.DisplayNickname}</color>을(를) 사이좋게 하늘로 보냈습니다.");
                }
            }
        }

        public void OnItemAdded(Exiled.Events.EventArgs.Player.ItemAddedEventArgs ev)
        {
            if (soulMates.ContainsKey(ev.Player))
            {
                Player soulMate = soulMates[ev.Player];

                soulMate.AddItem(ev.Item);
            }
        }

        public void OnItemRemoved(Exiled.Events.EventArgs.Player.ItemRemovedEventArgs ev)
        {
            if (soulMates.ContainsKey(ev.Player))
            {
                Player soulMate = soulMates[ev.Player];

                soulMate.RemoveItem(ev.Item);
            }
        }

        public void OnHurt(Exiled.Events.EventArgs.Player.HurtEventArgs ev)
        {
            if (soulMates.ContainsKey(ev.Player))
            {
                Player soulMate = soulMates[ev.Player];

                soulMate.MaxHealth = ev.Player.MaxHealth;
                soulMate.Health = ev.Player.Health;
            }
        }
    }
}
