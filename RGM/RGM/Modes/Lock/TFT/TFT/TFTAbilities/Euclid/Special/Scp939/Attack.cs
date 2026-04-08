using Exiled.API.Features.Roles;

namespace DAONTFT.Core.TFT.Euclid.Scp939;

[TFTAbility("안아줘요", "갈고리 발톱ㅣ능력의 쿨타임이 25% 감소합니다.", TFTAbilityLevel.Euclid, TFTAbilityCategory.Scp939, TFTAbilityPoint.Once, TFTAbilityType.Attack, "⚔️")]
public class Attack : TFTAbility
{
    public override void OnEnabled()
    {
        if (Owner.Role is Scp939Role scp939)
        {
            scp939.AttackCooldown *= 0.75f;
        }
    }

    public override void OnDisabled()
    {
    }
}
