using Exiled.API.Enums;
using Exiled.Events.EventArgs.Player;
using MEC;
using RGM.API.Features;
using static RGM.Variables.Variable;

namespace DAONTFT.Core.TFT.Keter.All;

[TFTAbility("방어 · 통달", "방어력을 20% 얻습니다. 추가로, 모든 받는 데미지가 최대 45까지만 적용됩니다. 능력 발동 시, 1.4초간 무적이 됩니다.", TFTAbilityLevel.Keter, TFTAbilityCategory.All, TFTAbilityPoint.Once, TFTAbilityType.AddDefense3, "⛔")]
public class AddDefense3 : TFTAbility
{
    const float MaxDamage = 45f;

    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.Hurting += OnHurting;

        Owner.AddEffect(EffectType.DamageReduction, 40);
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Hurting -= OnHurting;
    }

    public void OnHurting(HurtingEventArgs ev)
    {
        if (ev.Player != Owner || ev.DamageHandler.Type == DamageType.Crushed)
            return;

        if (ev.IsInstantKill)
        {
            ev.IsAllowed = false;
            ev.Player.Hurt(MaxDamage, ev.DamageHandler.Type);
            GodModePlayers.Add(Owner);

            Timing.CallDelayed(1.4f, () =>
            {
                if (GodModePlayers.Contains(Owner))
                    GodModePlayers.Remove(Owner);
            });
            return;
        }

        if (ev.DamageHandler.Damage > MaxDamage)
        {
            ev.DamageHandler.Damage = MaxDamage;
            GodModePlayers.Add(Owner);

            Timing.CallDelayed(1.4f, () =>
            {
                if (GodModePlayers.Contains(Owner))
                    GodModePlayers.Remove(Owner);
            });
        }
    }
}
