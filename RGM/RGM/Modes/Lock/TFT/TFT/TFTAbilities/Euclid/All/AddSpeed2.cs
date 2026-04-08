using Exiled.API.Enums;

namespace DAONTFT.Core.TFT.Euclid.All;

[TFTAbility("경공Ⅱ", "더 빠르게 이동합니다. (+12%)", TFTAbilityLevel.Euclid, TFTAbilityCategory.All, TFTAbilityPoint.Once, TFTAbilityType.AddSpeed2, "🏃")]
public class AddSpeed2 : TFTAbility
{
    public override void OnEnabled()
    {
        Owner.AddEffect(EffectType.MovementBoost, 12);
    }

    public override void OnDisabled()
    {
    }
}
