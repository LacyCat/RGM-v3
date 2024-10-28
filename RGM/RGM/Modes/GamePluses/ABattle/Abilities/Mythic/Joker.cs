using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Myth;

[Ability("조커", "사망할 경우 3초간 무적이 되고, 최대 체력이 1~3배로 조정되고, 상대방의 능력 1개를 삭제시키고, 전설 능력 2개를 얻습니다.", AbilityCategory.Mythic, AbilityType.MYTHIC_JOKER)]
public class Joker : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.Dying += OnDying;
    }

    private void OnDying(DyingEventArgs ev)
    {
        if (ev.Player != Owner)
            return;

        ev.IsAllowed = false;

        foreach (var player in Player.List)
            player.ShowHint("<b><i><color=#FF0000>조</color><color=#E70717>커</color><color=#D00F2E>를</color> <color=#A21E5C>건</color><color=#8B2673>들</color><color=#732E8B>인</color> <color=#453DB9>죄</color><color=#2E45D0>다</color><color=#174DE7>!</color></i></b>", 3);

        ev.Player.IsGodModeEnabled = true;

        Timing.CallDelayed(3f, () =>
        {
            ev.Player.IsGodModeEnabled = false;
        });

        ev.Player.MaxHealth = Random.Range(1, 4) * ev.Player.MaxHealth;

        ev.Attacker.RemoveAbility(ev.Attacker.GetAbilities().GetRandomValue());

        foreach (var ability in ABattle.Instance.GetRandomAbilities(AbilityCategory.Legend, 2))
        {
            ev.Player.AddAbility(ability);
        }
    }
}