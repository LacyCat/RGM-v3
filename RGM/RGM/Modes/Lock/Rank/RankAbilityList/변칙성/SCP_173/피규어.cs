using RGM.Modes;
using UnityEngine;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.변칙성
{
    [RankAbility("피규어", "체력이 500 증가하고, 몸 크기가 0.8로 조정됩니다.", RankAbilityType.피규어, RankCategory.SCP_173, RankAbilityCategory.변칙성, "🀄")]
    public class 피규어 : RankAbility
    {
        public override void OnEnabled()
        {
            Owner.MaxHealth += 500;
            Owner.Health = Owner.MaxHealth;
            Owner.Scale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        public override void OnDisabled()
        {
        }
    }
}
