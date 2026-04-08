using Exiled.Events.EventArgs.Player;
using RGM.Modes;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.변칙성
{
    [RankAbility("요리사", "체력 회복량이 100% 추가됩니다.", RankAbilityType.요리사, RankCategory.SCP_049_2, RankAbilityCategory.변칙성, "🍴")]
    public class 요리사 : RankAbility
    {
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Healing += OnHealing;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Healing -= OnHealing;
        }

        void OnHealing(HealingEventArgs ev)
        {
            if (ev.Player == Owner)
                ev.Amount *= 2;
        }
    }
}
