using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using MEC;
using PlayerRoles;
using RGM.API.DataBases;

namespace RGM.Modes
{
    [Mode(ModeCategory.Public, ModeInfo.Plus, ModeType.RocketLauncher)]
    class RocketLauncher : Mode
    {
        public override string Name => "로켓 런처";
        public override string Description => "무슨 이유로든 피격당하면 일정 확률로 승천합니다.";
        public override string Detail =>
"""
공격자가 <b>인간</b>인 경우 - 5%
공격자가 <b>SCP</b>인 경우 - 50%
공격자가 <b>???</b>인 경우 - 2000%!!!!!
""";
        public override string Color => "FA8258";

        public static RocketLauncher Instance;

        public override void OnEnabled()
        {
            Exiled.Events.Handlers.Player.Hurt += OnHurt;

            Timing.RunCoroutine(OnModeStarted());
        }

        public IEnumerator<float> OnModeStarted()
        {
            yield return 0f;
        }

        public void OnHurt(Exiled.Events.EventArgs.Player.HurtEventArgs ev)
        {
            if (ev.Attacker != null && !ev.DamageHandler.IsFriendlyFire && ev.Player != ev.Attacker)
            {
                int GetPercent()
                {
                    if (ev.Attacker.IsScp)
                        return UnityEngine.Random.Range(1, 3);

                    else if (ev.Attacker.Role.Type == RoleTypeId.Tutorial)
                        return 1;

                    else
                        return UnityEngine.Random.Range(1, 21);
                }

                if (GetPercent() == 1)
                {
                    Server.ExecuteCommand($"/cassie_sl {ev.Player.Nickname}(<color={ev.Player.Role.Color.ToHex()}>{Trans.Role[ev.Player.Role.Type]}</color>)(이)가 하늘로 승천했습니다.");
                    Server.ExecuteCommand($"/rocket {ev.Player.Id} 1");
                }
            }
        }
    }
}
