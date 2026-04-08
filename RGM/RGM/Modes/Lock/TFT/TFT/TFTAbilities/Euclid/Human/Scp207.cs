using MEC;

namespace DAONTFT.Core.TFT.Euclid.Human;

[TFTAbility("스피드왜건", "1분 후, 콜라를 획득합니다.", TFTAbilityLevel.Euclid, TFTAbilityCategory.Human, TFTAbilityPoint.Once, TFTAbilityType.Scp207, "🍹")]
public class Scp207 : TFTAbility
{
    public override void OnEnabled()
    {
        Timing.CallDelayed(60, () =>
        {
            Owner.AddItem(ItemType.SCP207);
        });
    }

    public override void OnDisabled()
    {
    }
}
