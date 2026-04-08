using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using RGM.Modes;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.변칙성
{
    [RankAbility("강렬한 의지", "최초 1회에 한하여 부정적인 효과를 제거합니다.", RankAbilityType.강렬한_의지, RankCategory.D계급, RankAbilityCategory.변칙성, "🔥")]
    public class 강렬한_의지 : RankAbility
    {
        int count = 1;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.ReceivingEffect += OnReceivingEffect;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.ReceivingEffect -= OnReceivingEffect;
        }

        void OnReceivingEffect(ReceivingEffectEventArgs ev)
        {
            if (ev.Player == Owner)
            {
                if (ev.Effect.GetEffectType().IsNegative())
                {
                    if (count > 0)
                    {
                        count--;
                        ev.IsAllowed = false;
                    }
                }
            }
        }
    }
}
