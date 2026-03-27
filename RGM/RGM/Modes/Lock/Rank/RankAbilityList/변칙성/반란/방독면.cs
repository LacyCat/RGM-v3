using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.Events.EventArgs.Player;
using RGM.Modes;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.변칙성
{
    [RankAbility("방독면", "부정적인 효과의 세기와 길이를 30% 감소시킵니다.", RankAbilityType.방독면, RankCategory.반란, RankAbilityCategory.변칙성, "😷")]
    public class 방독면 : RankAbility
    {
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
            if (ev.Player == Owner && ev.Effect.GetEffectType().IsNegative())
            {
                ev.Duration *= 0.7f;
                ev.Intensity = (byte)(ev.Intensity * 0.7f);
            }
        }
    }
}
