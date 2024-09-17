using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomRendering;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using Mirror;
using UnityEngine;

namespace RGM.Modes
{
    class SoulMate
    {
        public static SoulMate Instance;

        private Dictionary<Player, Player> soulMates;
        private List<Player> waitingPlayers;

        public void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Died += OnDied;
            Exiled.Events.Handlers.Player.Hurt += OnHurt;
            Exiled.Events.Handlers.Player.Healed += OnHealed;
            Exiled.Events.Handlers.Player.Escaping += OnEscaping;
            Exiled.Events.Handlers.Player.ItemAdded += OnItemAdded;
            Exiled.Events.Handlers.Player.ItemRemoved += OnItemRemoved;

            Timing.RunCoroutine(OnModeStarted());
            Timing.RunCoroutine(CurrentItemAsync());
            Timing.RunCoroutine(SoulMateMatching());
        }

        public IEnumerator<float> OnModeStarted()
        {
            while (true)
            {
                foreach (var player in Player.List)
                {
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

        public IEnumerator<float> SoulMateMatching()
        {
            soulMates = new Dictionary<Player, Player>();
            waitingPlayers = new List<Player>();

            List<Player> players = Player.List.ToList();

            players.ShuffleList();

            for (int i = 0; i < players.Count; i += 2)
            {
                if (i + 1 < players.Count)
                {
                    soulMates.Add(players[i], players[i + 1]);
                    soulMates.Add(players[i + 1], players[i]);
                }
            }

            yield return Timing.WaitForSeconds(10f);

            while (true)
            {
                foreach (var player in soulMates.Keys.ToList())
                {
                    if (player.IsDead)
                    {
                        Player soulMate = soulMates[player];
                        soulMates.Remove(player);
                    }
                    else if (player.IsAlive && !soulMates.ContainsKey(player))
                    {
                        waitingPlayers.Add(player);
                    }
                }

                if (waitingPlayers.Count >= 2)
                {
                    Player player1 = waitingPlayers[0];
                    Player player2 = waitingPlayers[1];

                    soulMates.Add(player1, player2);
                    soulMates.Add(player2, player1);

                    waitingPlayers.Remove(player1);
                    waitingPlayers.Remove(player2);
                }

                yield return Timing.WaitForSeconds(10f);
            }
        }

        public IEnumerator<float> CurrentItemAsync()
        {
            Dictionary<Player, Item> CurrentItem = new Dictionary<Player, Item>();

            while (true)
            {
                foreach (var player in Player.List)
                {
                    if (soulMates.ContainsKey(player))
                    {
                        Player soulmate = soulMates[player];

                        if (CurrentItem.ContainsKey(player))
                        {
                            if (CurrentItem[player] != player.CurrentItem)
                            {
                                foreach (var Item in soulmate.Items)
                                {
                                    if (Item.Type == player.CurrentItem.Type)
                                        soulmate.CurrentItem = Item;
                                }
                            }
                        }
                        else
                        {
                            CurrentItem.Add(player, player.CurrentItem);
                        }

                        soulmate.CurrentItem = player.CurrentItem;
                    }
                }

                yield return Timing.WaitForSeconds(0.1f);
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

        public void OnHurt(Exiled.Events.EventArgs.Player.HurtEventArgs ev)
        {
            if (soulMates.ContainsKey(ev.Player))
            {
                Player soulMate = soulMates[ev.Player];

                if (soulMate != null && soulMate.IsAlive)
                {
                    soulMate.Health = ev.Player.Health;
                }
            }
        }

        public void OnHealed(Exiled.Events.EventArgs.Player.HealedEventArgs ev)
        {
            if (soulMates.ContainsKey(ev.Player))
            {
                Player soulMate = soulMates[ev.Player];

                if (soulMate != null && soulMate.IsAlive)
                {
                    soulMate.Health = ev.Player.Health;
                }
            }
        }

        public async void OnEscaping(Exiled.Events.EventArgs.Player.EscapingEventArgs ev)
        {
            float MaxHealth = ev.Player.MaxHealth;
            float Health = ev.Player.Health;

            await Task.Delay(500);

            ev.Player.MaxHealth = MaxHealth;
            ev.Player.Health = Health;
        }

        public void OnItemAdded(Exiled.Events.EventArgs.Player.ItemAddedEventArgs ev)
        {
            if (!ev.Item.IsAmmo && soulMates.ContainsKey(ev.Player))
            {
                Player soulMate = soulMates[ev.Player];

                if (soulMate != null && soulMate.IsAlive)
                {
                    soulMate.AddItem(ev.Item.Type);
                }
            }
        }

        public void OnItemRemoved(Exiled.Events.EventArgs.Player.ItemRemovedEventArgs ev)
        {
            if (!ev.Item.IsAmmo && soulMates.ContainsKey(ev.Player))
            {
                Player soulMate = soulMates[ev.Player];

                if (soulMate != null && soulMate.IsAlive)
                {
                    foreach (var Item in soulMate.Items)
                    {
                        if (Item.Type == ev.Item.Type)
                            soulMate.RemoveItem(Item);
                    }
                }
            }
        }
    }
}
