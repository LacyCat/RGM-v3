using System.Collections.Generic;
using Exiled.API.Enums;
using MEC;

namespace DAONTFT.Core.TFT.Keter.All;

[TFTAbility("잠수부", "시야가 개선되고 스태미나가 무제한이 됩니다.", TFTAbilityLevel.Keter, TFTAbilityCategory.All, TFTAbilityPoint.Continuous, TFTAbilityType.Diver, "🫁")]
public class Diver : TFTAbility
{
    CoroutineHandle _diverRotation;

    public override void OnEnabled()
    {
        _diverRotation = Timing.RunCoroutine(DiverRotation());
    }

    public override void OnDisabled()
    {
        Timing.KillCoroutines(_diverRotation);

        Owner.DisableEffect(EffectType.Invigorated);
    }

    public IEnumerator<float> DiverRotation()
    {
        while (true)
        {
            Owner.EnableEffect(EffectType.Invigorated);

            yield return Timing.WaitForSeconds(1f);
        }
    }
}
