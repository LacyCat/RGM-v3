namespace RGM.Modes.Abilities.Synergy;

[RequiresAbility(AbilityType.NORMAL_INSURANCE, AbilityType.NORMAL_DOPAMINE, AbilityType.EPIC_SURVIVOR)]
[Ability("생존 전문가", "<보험, 도파민, 구사일생> 즉시 500HP를 얻습니다. (최대 체력 반영)", AbilityCategory.Synergy, AbilityType.SYNERGY_SURVIVALEXPERT)]
public class SurvivalExpert : Ability
{
    public override void OnEnabled()
    {
        Owner.MaxHealth += 500;
        Owner.Health += 500;
    }

    public override void OnDisabled()
    {
    }
}
