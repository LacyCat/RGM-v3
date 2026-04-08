using RGM.Modes;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.변칙성
{
    [RankAbility("선임 연구원", "구역 관리자 키카드를 얻습니다.", RankAbilityType.선임_연구원, RankCategory.과학자, RankAbilityCategory.변칙성, "🔬")]
    public class 선임_연구원 : RankAbility
    {
        public override void OnEnabled()
        {
            Owner.AddItem(ItemType.KeycardZoneManager);
        }

        public override void OnDisabled()
        {
        }
    }
}
