using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Item;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp1509;
using MEC;
using Mirror;
using PlayerRoles;
using RGM.API.Features;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RGM.Modes
{
    [Mode(ModeCategory.Public, ModeInfo.Set, ModeType.Chess)]
    class Chess : Mode
    {
        public override string Name => "체스";
        public override string Description => "자신의 왕을 지키세요.";
        public override string Detail =>
"""
킹(1): 폰과 동일한 스펙을 지닙니다. 죽으면 팀 전체가 패배합니다.
퀸(1): 룩과 비숍의 능력을 동시에 지닙니다.
나이트(2): 점프력이 향상됩니다.
비숍(2): 이동 속도가 빠릅니다.
룩(2): 제일버드 대신 칼을 받습니다.
폰: 제일버드를 들고 스폰합니다.

(숫자) -> 각 팀당 스폰되는 수
""";
        public override string Color => "637c66";
        public override string Map => "Chess";

        CoroutineHandle _onModeStarted;

        List<Player> special = new();
        List<Player> teamA = new();
        List<Player> teamB = new();
        Player kingA = null;
        Player kingB = null;
        Player queenA = null;
        Player queenB = null;
        List<Player> knightA = new();
        List<Player> knightB = new();
        List<Player> bishopA = new();
        List<Player> bishopB = new();
        List<Player> rookA = new();
        List<Player> rookB = new();

        public override void OnEnabled()
        {
            Round.IsLocked = true;

            Exiled.Events.Handlers.Player.Died += OnDied;
            Exiled.Events.Handlers.Scp1509.Resurrecting += OnResurrecting;
            Exiled.Events.Handlers.Item.ChargingJailbird += OnChargingJailbird;

            _onModeStarted = Timing.RunCoroutine(OnModeStarted());
        }

        public override void OnDisabled()
        {
            Round.IsLocked = false;

            Exiled.Events.Handlers.Player.Died -= OnDied;
            Exiled.Events.Handlers.Scp1509.Resurrecting -= OnResurrecting;
            Exiled.Events.Handlers.Item.ChargingJailbird -= OnChargingJailbird;

            Timing.KillCoroutines(_onModeStarted);
        }

        public IEnumerator<float> OnModeStarted()
        {
            var players = PlayerManager.List.Where(x => !x.IsTutorial).ToList();
            if (players.Count < 2)
                yield break;

            players.ShuffleList();

            int halfCount = players.Count / 2;
            special = new();
            teamA = players.Take(halfCount).ToList();
            teamB = players.Skip(halfCount).ToList();
            teamA.GetRandomValue();
            special.Add(kingA);
            teamB.GetRandomValue();
            special.Add(kingB);
            queenA = null;
            queenB = null;
            knightA = new();
            knightB = new();
            bishopA = new();
            bishopB = new();
            rookA = new();
            rookB = new();

            bool canSpawnFullPieces = players.Count >= 16;

            Tools.PlayGlobalAudio("RankCountdown", 1.5f);

            void setupTeam(List<Player> team, RoleTypeId roleType, Vector3 pos, Player king, Player queen, List<Player> knights, List<Player> rooks, List<Player> bishops)
            {
                foreach (var ply in team)
                {
                    ply.Role.Set(roleType, RoleSpawnFlags.None);
                    ply.Position = pos;
                    ply.AddEffect(EffectType.Ensnared, 1, 20);
                    ply.AddEffect(EffectType.HeavyFooted, 100, 20);

                    ply.ClearInventory();
                    ply.AddItem(ItemType.Jailbird);

                    if (ply == king) // 킹
                    {
                        ply.AddBroadcast(20, "당신은 <b>킹</b>입니다. 죽으면 팀 전체가 패배합니다.");
                    }
                    else if (ply == queen) // 퀸
                    {
                        ply.AddBroadcast(20, "당신은 <b>퀸</b>입니다. 룩과 비숍의 능력을 동시에 지닙니다.");
                        ply.AddItem(ItemType.SCP1344);
                        ply.AddEffect(EffectType.MovementBoost, 20);
                    }
                    else if (knights.Contains(ply)) // 나이트
                    {
                        ply.AddBroadcast(20, "당신은 <b>나이트</b>입니다. 점프력이 향상됩니다.");
                        ply.AddEffect(EffectType.Lightweight, 100);
                    }
                    else if (rooks.Contains(ply)) // 룩
                    {
                        ply.AddBroadcast(20, "당신은 <b>룩</b>입니다. 칼을 소지합니다.");
                        ply.AddItem(ItemType.SCP1344);
                    }
                    else if (bishops.Contains(ply)) // 비숍
                    {
                        ply.AddBroadcast(20, "당신은 <b>비숍</b>입니다. 이동 속도가 빠릅니다.");
                        ply.AddEffect(EffectType.MovementBoost, 20);
                    }
                    else // 폰
                    {
                        ply.AddBroadcast(20, "당신은 <b>폰</b>입니다. 왕을 지키세요.");
                    }
                }
            }

            if (canSpawnFullPieces)
            {
                queenA = teamA.GetRandomValue(x => !special.Contains(x));
                special.Add(queenA);
                queenB = teamB.GetRandomValue(x => !special.Contains(x));
                special.Add(queenB);
                knightA = teamA.Where(x => !special.Contains(x)).Take(2).ToList();
                special.AddRange(knightA);
                knightB = teamB.Where(x => !special.Contains(x)).Take(2).ToList();
                special.AddRange(knightB);
                bishopA = teamA.Where(x => !special.Contains(x)).Take(2).ToList();
                special.AddRange(bishopA);
                bishopB = teamB.Where(x => !special.Contains(x)).Take(2).ToList();
                special.AddRange(bishopB);
                rookA = teamA.Where(x => !special.Contains(x)).Take(2).ToList();
                special.AddRange(rookA);
                rookB = teamB.Where(x => !special.Contains(x)).Take(2).ToList();
                special.AddRange(rookB);
            }

            setupTeam(teamA, RoleTypeId.Scientist, new Vector3(34.60717f, 381.5809f, -30.17525f), kingA, queenA, knightA, rookA, bishopA); // A팀 (과학자)
            setupTeam(teamB, RoleTypeId.ClassD, new Vector3(34.64063f, 381.5783f, -1.050781f), kingB, queenB, knightB, rookB, bishopB);    // B팀 (죄수)
        }

        void OnDied(DiedEventArgs ev)
        {
            List<Player> winTeam = new();

            if (ev.Player == kingA)
                winTeam = teamB;

            if (ev.Player == kingB)
                winTeam = teamA;

            Timing.RunCoroutine(Tools.SetWinner(winTeam, 1));

            foreach (var player in Player.List)
            {
                player.AddBroadcast(20, $"{(winTeam == teamA ? "A" : "B")}팀 승리!");
            }
        }

        void OnResurrecting(ResurrectingEventArgs ev)
        {
            ev.IsAllowed = false;
        }

        void OnChargingJailbird(ChargingJailbirdEventArgs ev)
        {
            ev.IsAllowed = false;
        }
    }
}
