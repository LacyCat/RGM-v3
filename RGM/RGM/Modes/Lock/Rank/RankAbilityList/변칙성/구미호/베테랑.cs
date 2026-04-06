using Exiled.Events.EventArgs.Player;
using RGM.Modes;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.변칙성
{
    [RankAbility("베테랑", "최초 1회에 한하여 사망 판정을 받을 경우 한번 버팁니다.", RankAbilityType.베테랑, RankCategory.구미호, RankAbilityCategory.변칙성, "🔋")]
    public class 베테랑 : RankAbility
    {
        int count = 1;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Dying += OnDying;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Dying -= OnDying;
        }

        void OnDying(DyingEventArgs ev)
        {
            if (ev.Player == Owner) 
            {
                if (count > 0)
                {
                    ev.IsAllowed = false;
                    count--;
                }
            }
        }
    }
}
