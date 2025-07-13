using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using ProjectMER.Commands.Modifying.Position;
using ProjectMER.Events.Arguments;
using ProjectMER.Events.Handlers;
using ProjectMER.Features;
using ProjectMER.Features.Objects;
using RemoteAdmin;
using RGM.API.Components;
using RGM.API.Features;
using RGM.Modes.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static RGM.Variables.ServerManagers;

namespace RGM.Modes.Sets.AddScp.Scps
{
    public static class Scp457
    {
        public static void OnEnabled()
        {
            CommandProcessor.RemoteAdminCommandHandler.RegisterCommand(new SetScp457());
        }

        public static Player Create(Player player)
        {
            player.Role.Set(RoleTypeId.Tutorial, RoleSpawnFlags.AssignInventory);
            player.EnableEffect(EffectType.SilentWalk, 10);
            player.EnableEffect(EffectType.Slowness, 10);
            player.MaxHealth = 1500;
            player.Health = player.MaxHealth;
            player.VoiceChannel = VoiceChat.VoiceChatChannel.ScpChat;
            player.IsBypassModeEnabled = true;
            Timing.CallDelayed(1, () =>
            {
                player.Scale = new Vector3(0.3f, 0.3f, 0.3f);
            });
            player.AddHint("SCP-457 설명", "<size=25>당신은 <color=red>SCP-457</color>(<color=#f4fe48>Euclid</color>/<color=#fe4848>Potential Keter</color>)입니다.</size>\n<size=20>진영은 <color=red>SCP</color> 소속입니다.</size>", 20);
            SchematicObject schematic = ObjectSpawner.SpawnSchematic("SCP_457", player.Position, player.Rotation, new Vector3(0.4f, 0.4f, 0.4f));
            schematic.transform.parent = player.Transform;

            IEnumerator<float> main()
            {
                yield break;
            }

            IEnumerator<float> makeSound()
            {
                while (true)
                {
                    var audio = Tools.PlaySound(player.Transform, "scp-457-burn");

                    yield return Timing.WaitForSeconds((float)audio.Duration.TotalSeconds);
                }
            }

            IEnumerator<float> burn2()
            {
                while (true)
                {
                    foreach (var p in Player.List.Where(x => !x.IsScp && x != player && Vector3.Distance(x.Position, player.Position) < 4))
                    {
                        p.EnableEffect(EffectType.Burned, 1, 0.1f);
                        p.Hit(player, player.Scale.x * 10);
                    }

                    yield return Timing.WaitForSeconds(0.1f);
                }
            }

            var main_c = Timing.RunCoroutine(main());
            var makeSound_c = Timing.RunCoroutine(makeSound());
            var burn2_c = Timing.RunCoroutine(burn2());

            void OnHurting(HurtingEventArgs ev)
            {
                if (ev.Player != player || ev.Attacker == null)
                    return;

                if (ev.Attacker.IsScp)
                {
                    ev.IsAllowed = false;
                }
            }

            void OnPickingUpItem(PickingUpItemEventArgs ev)
            {
                if (ev.Player != player)
                    return;

                if (ev.Pickup.Type.IsWeapon())
                {
                    ev.IsAllowed = false;

                    player.AddHint("SCP-457 제한", "<size=20>총기류는 태울 수 없습니다.</size>", 1);
                }
                else
                {
                    if (ev.Player.Scale.x < 1.15f)
                        ev.Player.Scale += new Vector3(0.002f * ev.Pickup.Weight, 0.002f * ev.Pickup.Weight, 0.002f * ev.Pickup.Weight);
                    ev.Player.MaxHealth += ev.Pickup.Weight * 1.2f;
                    ev.Player.Health += ev.Pickup.Weight * 1.2f;

                    ev.IsAllowed = false;
                    ev.Pickup.Destroy();
                }
            }

            void OnDied(DiedEventArgs ev)
            {
                if (ev.Attacker == null || ev.Attacker != player)
                    return;

                if (ev.Player.Scale.x < 1.15f)
                    ev.Attacker.Scale += new Vector3(0.03f, 0.03f, 0.03f);
                ev.Attacker.MaxHealth += 25;
                ev.Attacker.Health += 25;
            }

            void OnDying(DyingEventArgs ev)
            {
                if (ev.Player != player)
                    return;

                Vector3 pos = ev.Player.Position;

                Timing.CallDelayed(Timing.WaitForOneFrame, () =>
                {
                    if (ev.Player.IsDead)
                    {
                        if (ev.Player == player)
                        {
                            Timing.KillCoroutines(main_c);
                            Timing.KillCoroutines(makeSound_c);
                            Timing.KillCoroutines(burn2_c);

                            schematic.Destroy();

                            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
                            Exiled.Events.Handlers.Player.PickingUpItem -= OnPickingUpItem;
                            Exiled.Events.Handlers.Player.Died -= OnDied;
                            Exiled.Events.Handlers.Player.Dying -= OnDying;
                        }
                    }
                });
            }

            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.PickingUpItem += OnPickingUpItem;
            Exiled.Events.Handlers.Player.Died += OnDied;
            Exiled.Events.Handlers.Player.Dying += OnDying;
            return player;
        }
    }
}
