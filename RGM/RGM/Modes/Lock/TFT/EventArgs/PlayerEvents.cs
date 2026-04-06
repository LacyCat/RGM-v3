using CustomPlayerEffects;
using DAONTFT.Core.IEnumerators;
using DAONTFT.Core.TFT;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using PlayerRoles;
using PlayerStatsSystem;
using RGM.API.Features;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DAONTFT.Core.Variables.Base;

namespace DAONTFT.Core.EventArgs
{
    public static class PlayerEvents
    {
        public static void OnVerified(VerifiedEventArgs ev)
        {
            Verified(ev.Player);
        }

        public static void Verified(Player player)
        {
            if (!PlayerTFTAbilities.ContainsKey(player))
            {
                PlayerTFTAbilities.Add(player, new List<TFTAbility>());
                IsSelecting.Add(player, false);
                IsLifeUsed.Add(player, false);
            }

            if (!PlayerHints.ContainsKey(player))
            {
                PlayerHints.Add(player, new());
            }

            Timing.RunCoroutine(Enumerator.UpgradeDisplay(player));
        }

        public static void OnSpawned(SpawnedEventArgs ev)
        {
            if (ev.Player.IsAlive)
            {
                ev.Player.Scale = new Vector3(1, 1, 1);
            }
        }

        public static void OnInteractingDoor(InteractingDoorEventArgs ev)
        {
            if (!ev.IsAllowed && ev.Player.HasKeycardPermission(ev.Door.KeycardPermissions) &&
                !ev.Door.IsLocked)
                ev.IsAllowed = true;
        }

        public static void OnActivatingWarheadPanel(ActivatingWarheadPanelEventArgs ev)
        {
            if (!ev.IsAllowed && ev.Player.HasKeycardPermission(KeycardPermissions.AlphaWarhead))
                ev.IsAllowed = true;
        }

        public static void OnUnlockingGenerator(UnlockingGeneratorEventArgs ev)
        {
            if (!ev.IsAllowed && ev.Player.HasKeycardPermission(ev.Generator.KeycardPermissions))
                ev.IsAllowed = true;
        }

        public static void OnInteractingLocker(InteractingLockerEventArgs ev)
        {
            if (!ev.IsAllowed && ev.InteractingChamber != null &&
                ev.Player.HasKeycardPermission(ev.InteractingChamber.RequiredPermissions, true))
                ev.IsAllowed = true;
        }

        public static void OnHurting(HurtingEventArgs ev)
        {
            if (Round.IsLobby || Round.IsEnded)
                return;

            if (ev.DamageHandler.Type == DamageType.Falldown && ev.Player.TryGetEffect(EffectType.Lightweight, out StatusEffectBase statusEffect))
            {
                if (statusEffect.IsEnabled)
                {
                    ev.IsAllowed = false;
                }
            }

            if (GodModePlayers.Contains(ev.Player))
            {
                if (!BlockDamageTypes.Contains(ev.DamageHandler.Type))
                    ev.IsAllowed = false;
            }
        }

        public static void OnDying(DyingEventArgs ev)
        {
            if (Round.IsLobby)
            {
                ev.Player.ClearInventory();
            }
            else
            {
                if (GodModePlayers.Contains(ev.Player))
                {
                    if (!BlockDamageTypes.Contains(ev.DamageHandler.Type))
                        ev.IsAllowed = false;
                }
                else
                {
                    if (ev.DamageHandler.Type == DamageType.PocketDimension)
                    {
                        var attacker = Player.Get(RoleTypeId.Scp106).GetRandomValue();

                        if (attacker == null) return;

                        ev.IsAllowed = false;

                        ev.Player.Kill(new ScpDamageHandler(attacker.ReferenceHub, DeathTranslations.PocketDecay));
                    }
                }
            }
        }

        public static IEnumerator<float> OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.IsDead || ev.NewRole.IsDead() || ev.Player.GetAbilities().Count() == 0)
            {
                Timing.CallDelayed(Timing.WaitForOneFrame, () =>
                {
                    TFTBattle.Reset(ev.Player);
                });
            }

            yield break;
        }

        public static void OnDied(DiedEventArgs ev)
        {
            Timing.CallDelayed(Timing.WaitForOneFrame, () =>
            {
                TFTBattle.Reset(ev.Player);
            });
        }
    }
}
