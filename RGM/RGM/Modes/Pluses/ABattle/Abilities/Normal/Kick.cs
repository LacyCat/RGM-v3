using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Normal;

[Ability("회축", "[ALT]를 눌러 발차기 공격을 가할 수 있습니다. (쿨타임 1초)", AbilityCategory.Common, AbilityType.NORMAL_KICK)]
public class Kick : Ability
{
    public static int MeleeCooldown = 0;

    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.TogglingNoClip += OnTogglingNoClip;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.TogglingNoClip -= OnTogglingNoClip;
    }

    public void OnTogglingNoClip(TogglingNoClipEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        if (Tools.TryGetLookPlayer(ev.Player, 4f, out Player player))
        {
            if (ev.Player != player && MeleeCooldown <= 0 && ev.Player.LeadingTeam != player.LeadingTeam)
            {
                Hitmarker.SendHitmarkerDirectly(ev.Player.ReferenceHub, 0.7f);
                player.Hurt(12.05f, "무지성으로 뚜드려 맞았습니다.");

                MeleeCooldown = 1;

                Timing.CallDelayed(1f, () => MeleeCooldown = 0);
            }
        }
    }
}
