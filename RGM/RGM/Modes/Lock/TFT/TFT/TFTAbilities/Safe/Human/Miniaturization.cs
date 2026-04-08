using UnityEngine;

namespace DAONTFT.Core.TFT.Safe.Human;

[TFTAbility("소인화", "몸의 크기가 10% 줄어듭니다.", TFTAbilityLevel.Safe, TFTAbilityCategory.Human, TFTAbilityPoint.Once, TFTAbilityType.Miniaturization, "👤")]
public class Miniaturization : TFTAbility
{
    public override void OnEnabled()
    {
        Owner.Scale = new Vector3(0.9f, 0.9f, 0.9f);
    }

    public override void OnDisabled()
    {
    }
}
