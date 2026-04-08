using Exiled.Events.EventArgs.Player;
using RGM.API.Features;

using static RGM.Variables.Variable;

namespace RGM.Modes.Abilities.Unique.Scp096;

[Ability("별자리 찢기", "25% 확률로 공격한 대상을 즉사시킵니다.", AbilityCategory.Scp096, AbilityType.SCP096_STARTEARING)]
public class StarTearing : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.Hurting += OnHurting;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Hurting -= OnHurting;
    }

    public void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Attacker == null || ev.Attacker != Owner)
            return;

        if (UnityEngine.Random.Range(1, 5) == 1)
        {
            if (GodModePlayers.Contains(ev.Player))
                GodModePlayers.Remove(ev.Player);

            ev.Player.Hit(ev.Attacker, ev.Player.MaxHealth);
        }
    }
}
