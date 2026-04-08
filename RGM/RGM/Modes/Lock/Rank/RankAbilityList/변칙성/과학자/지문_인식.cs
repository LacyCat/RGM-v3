using Exiled.Events.EventArgs.Player;
using RGM.Modes;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.변칙성
{
    [RankAbility("지문 인식", "테슬라를 작동시키지 않습니다.", RankAbilityType.지문_인식, RankCategory.과학자, RankAbilityCategory.변칙성, "👍")]
    public class 지문_인식 : RankAbility
    {
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.TriggeringTesla += OnTriggeringTesla;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.TriggeringTesla -= OnTriggeringTesla;
        }

        void OnTriggeringTesla(TriggeringTeslaEventArgs ev)
        {
            if (ev.Player == Owner)
                ev.IsAllowed = false;
        }
    }
}
