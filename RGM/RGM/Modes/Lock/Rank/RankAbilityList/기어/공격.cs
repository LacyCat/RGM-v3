using Exiled.Events.EventArgs.Player;
using RGM.Modes;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.기어
{
    [RankAbility("공격", "체력이 50% 이하가 되면 입히는 데미지가 10% 증가합니다.", RankAbilityType.공격, RankCategory.공통, RankAbilityCategory.기어, "🔪")]
    public class 공격 : RankAbility
    {
        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Player.Hurting -= OnHurting;
        }

        void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Attacker != null && ev.Attacker == Owner)
            {
                if (Owner.Health <= Owner.MaxHealth / 2)
                    ev.DamageHandler.Damage *= 1.1f;
            }
        }
    }
}
