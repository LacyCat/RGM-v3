namespace RGM.Modes.Abilities.Normal;

[Ability("운동", "25%만큼 최대 체력을 추가합니다.", AbilityCategory.Normal, AbilityType.NORMAL_WORKOUT)]
public class Workout : Ability
{
    public override void OnEnabled()
    {
        Owner.MaxHealth += (int)(Owner.MaxHealth * 0.25f);
        Owner.Health = Owner.MaxHealth;
    }
}