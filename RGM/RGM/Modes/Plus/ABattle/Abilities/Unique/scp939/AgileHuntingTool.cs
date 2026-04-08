using Exiled.API.Features.Roles;

namespace RGM.Modes.Abilities.Unique.Scp939;

[Ability("민첩한 사냥 도구", "공격 쿨타임이 25% 줄어듭니다.", AbilityCategory.Scp939, AbilityType.SCP939_AGILEHUNTINGTOOL)]
public class AgileHuntingTool : Ability
{
    public override void OnEnabled()
    {
        if (Owner.Role is Scp939Role Scp939)
            Scp939.AttackCooldown /= 4;
    }

    public override void OnDisabled()
    {
    }
}
