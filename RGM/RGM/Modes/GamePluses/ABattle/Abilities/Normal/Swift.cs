using System.Collections.Generic;
using Exiled.API.Enums;

namespace RGM.Modes.Abilities.Normal;

[Ability("경공", "이동 속도가 25% 증가합니다.", AbilityCategory.Normal, AbilityType.NORMAL_SWIFT)]
public class Swift : EffectAbility
{
    public override void OnEnabled()
    {
        Owner.GetEffect(EffectType.MovementBoost).Intensity += 10;
    }
}