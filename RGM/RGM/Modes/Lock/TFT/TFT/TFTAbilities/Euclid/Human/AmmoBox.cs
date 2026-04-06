using System.Linq;
using DAONTFT.Core.Functions;
using Exiled.API.Extensions;

namespace DAONTFT.Core.TFT.Euclid.Human;

[TFTAbility("탄약고", "모든 종류의 탄약을 각각 5세트씩 받습니다.", TFTAbilityLevel.Euclid, TFTAbilityCategory.Human, TFTAbilityPoint.Once, TFTAbilityType.AmmoBox, "🔫")]
public class CandyAddict : TFTAbility
{
    public override void OnEnabled()
    {
        foreach (var ammo in Function.EnumToList<ItemType>().Where(x => x.IsAmmo()))
        {
            for (int i = 0; i < 5; i++)
            {
                Owner.AddItem(ammo);
            }
        }
    }

    public override void OnDisabled()
    {
    }
}
