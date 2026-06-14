using System;
using System.Collections.Generic;
using System.Linq;
using AdminToys;
using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using RGM.API.Features;
using RGM.API.DataBases;
using Exiled.API.Features.Items;
using PlayerRoles;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using UnityEngine;

namespace RGM.Modes
{
    [Mode(ModeCategory.Public, ModeInfo.Plus, ModeType.RandomItem)]
    public class RandomItem : Mode
    {
        private readonly List<ItemType> _l =
        [
            ItemType.GunSCP127,
            ItemType.SCP1509,
            ItemType.SCP268,
            ItemType.SCP1344,

            // 무기
            ItemType.Jailbird,
            ItemType.MicroHID,
            ItemType.ParticleDisruptor
        ];

        private readonly List<ItemType> _s =
        [
            ItemType.AntiSCP207,
            ItemType.SCP2176,
            ItemType.SCP018,
            ItemType.SCP1576,

            // 카드
            ItemType.KeycardO5,

            // 무기
            ItemType.GunLogicer,
            ItemType.GunFRMG0
        ];

        private readonly Dictionary<Player, byte> _firstCount = new();
        private readonly Dictionary<Player, byte> _secondCount = new();

        public override string Name => "랜덤박스";
        public override string Description => "60초마다 랜덤한 아이템을 얻을 수 있습니다!";

        public override string Detail =>
            """
            무작위 아이템들이 동일한 확률로 지급됩니다.

            이후, 60초마다 무작위 아이템들을 하나 더 받습니다.
            """;

        public override string Color => "BFFF00";

        public static RandomItem Instance;

        CoroutineHandle _onModeStarted;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Spawned += OnSpawned;

            _onModeStarted = Timing.RunCoroutine(OnModeStarted());
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Spawned -= OnSpawned;

            Timing.KillCoroutines(_onModeStarted);
        }

        private IEnumerator<float> OnModeStarted()
        {
            foreach (var player in PlayerManager.List)
            {
                Timing.RunCoroutine(Spawned(player));
            }

            while (true)
            {
                yield return Timing.WaitForSeconds(60f);

                foreach (var player in PlayerManager.List)
                {
                    if (!_firstCount.ContainsKey(player))
                        _firstCount.Add(player, 1);
                    if (!_secondCount.ContainsKey(player))
                        _secondCount.Add(player, 1);
                }
                
                foreach (var player in PlayerManager.List.Where(x => x.IsAlive && x.Role.Type != RoleTypeId.Scp079))
                {
                    try
                    {
                        switch (_firstCount[player])
                        {
                            case < 10 when _secondCount[player] < 10:
                            {
                                Log.Info("Case 1");
                                Item item = player.AddRandomItem();
                                
                                if (_l.Contains(item.Type))
                                    _firstCount[player] -= 5;
                                
                                if (_s.Contains(item.Type))
                                    _secondCount[player] -= 4;

                                player.AddHint("랜덤박스", $"<color=#F3F781>{Trans.Item[item.Type]}</color>(을)를 지급받았습니다.",
                                    5);
                                _firstCount[player] += 1;
                                _secondCount[player] += 1;
                                break;
                            }
                            case >= 10:
                            {
                                Log.Info("Case 2");

                                Item item = player.AddItem(_l.GetRandomValue());

                                Tools.PlaySound(player.Transform, "L 등급", 4);
                                Light(UnityEngine.Color.red);

                                player.AddHint("랜덤박스", $"<color=#F3F781>{Trans.Item[item.Type]}</color>(을)를 지급받았습니다.",
                                    5);
                                _firstCount[player] = 1;
                                break;
                            }
                            default:
                            {
                                Log.Info("Case 3");

                                if (_secondCount[player] >= 5)
                                {
                                    Item item = player.AddItem(_s.GetRandomValue());

                                    Tools.PlaySound(player.Transform, "S 등급", 2);
                                    Light(UnityEngine.Color.yellow);

                                    player.AddHint("랜덤박스",
                                        $"<color=#F3F781>{Trans.Item[item.Type]}</color>(을)를 지급받았습니다.", 5);
                                }

                                _secondCount[player] = 1;

                                break;
                            }

                                void Light(Color color)
                                {
                                    try
                                    {
                                        SchematicObject schematic = ObjectSpawner.SpawnSchematic("Light", Vector3.zero);
                                        LightSourceToy light = schematic.GetComponentsInChildren<LightSourceToy>()
                                            .First();

                                        schematic.transform.parent = player.Transform;
                                        schematic.transform.localPosition = Vector3.zero;

                                        light.NetworkLightColor = color;
                                        light.NetworkLightRange = 50;
                                        light.NetworkLightIntensity = 10;

                                        Timing.CallDelayed(3, schematic.Destroy);
                                    }
                                    catch (Exception e)
                                    {
                                        Log.Warn($"[RGM] RandomItem color fetch failure: {e.Message}");
                                    }
                                }
                        }
                    }
                    catch (KeyNotFoundException e)
                    {
                        Log.Warn($"[RGM] RandomItem card fetch failure: {e.Message}");
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"[RGM] RandomItem Mode Error: {ex}");
                    }
                }
            }
        }

        private void OnSpawned(Exiled.Events.EventArgs.Player.SpawnedEventArgs ev)
        {
            if (ev.Player.IsNonePlayer())
                return;

            Timing.RunCoroutine(Spawned(ev.Player));
        }

        private IEnumerator<float> Spawned(Player player)
        {
            if (!player.IsAlive)
                yield break;

            yield return Timing.WaitForOneFrame;

            player.ClearInventory();

            for (int i = 1; i < 9; i++)
            {
                player.AddRandomItem();

                yield return Timing.WaitForSeconds(1);
            }
        }
    }
}