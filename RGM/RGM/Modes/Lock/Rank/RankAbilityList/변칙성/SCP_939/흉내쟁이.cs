using Exiled.API.Features.Roles;
using MEC;
using RGM.Modes;
using System.Collections.Generic;

namespace RGM.RGM.Modes.Lock.Rank.RankAbilityList.변칙성
{
    [RankAbility("흉내쟁이", "흉내 쿨다운이 0.1초로 조정됩니다.", RankAbilityType.흉내쟁이, RankCategory.SCP_939, RankAbilityCategory.변칙성, "☺")]
    public class 흉내쟁이 : RankAbility
    {
        CoroutineHandle handle;

        public override void OnEnabled()
        {
            handle = Timing.RunCoroutine(enumerator());
        }

        public override void OnDisabled()
        {
            Timing.KillCoroutines(handle);
        }

        IEnumerator<float> enumerator()
        {
            while (true)
            {
                if (Owner.Role is Scp939Role scp939)
                    scp939.MimicryCooldown = 0;

                yield return Timing.WaitForSeconds(0.1f);
            }
        }
    }
}
