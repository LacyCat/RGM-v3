namespace DAONTFT.Core.TFT.Euclid.All;

[TFTAbility("운동Ⅱ", "최대 체력 + 체력 -> +50❤️ (SCP x10)", TFTAbilityLevel.Euclid, TFTAbilityCategory.All, TFTAbilityPoint.Once, TFTAbilityType.AddHealth2, "❤️")]
public class AddHealth2 : TFTAbility
{
    public override void OnEnabled()
    {
        float health = Owner.IsScp ? 500 : 50;
        Owner.MaxHealth += health;
        Owner.Health += health;
    }

    public override void OnDisabled()
    {
    }
}
