namespace DAONTFT.Core.TFT.Keter.All;

[TFTAbility("운동Ⅲ", "최대 체력 + 체력 -> +100❤️ (SCP x10)", TFTAbilityLevel.Keter, TFTAbilityCategory.All, TFTAbilityPoint.Once, TFTAbilityType.AddHealth3, "❤️")]
public class AddHealth3 : TFTAbility
{
    public override void OnEnabled()
    {
        float health = Owner.IsScp ? 1000 : 100;
        Owner.MaxHealth += health;
        Owner.Health += health;
    }
    
    public override void OnDisabled()
    {
    }
}
