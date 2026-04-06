namespace DAONTFT.Core.TFT.Safe.All;

[TFTAbility("운동Ⅰ", "최대 체력 + 체력 -> +20❤️ (SCP x10)", TFTAbilityLevel.Safe, TFTAbilityCategory.All, TFTAbilityPoint.Once, TFTAbilityType.AddHealth1, "❤️")]
public class AddHealth1 : TFTAbility
{
    public override void OnEnabled()
    {
        float health = Owner.IsScp ? 200 : 20;
        Owner.MaxHealth += health;
        Owner.Health += health;
    }

    public override void OnDisabled()
    {
    }
}
