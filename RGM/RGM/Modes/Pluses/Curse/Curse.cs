using Exiled.API.Features.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using MEC;
using UnityEngine;
using Exiled.API.Features.Roles;
using Exiled.API.Enums;
using RGM.API.Features;
using MultiBroadcast.API;
using PlayerRoles;
using Exiled.API.Extensions;

namespace RGM.Modes
{
    [Mode(ModeCategory.Public, ModeInfo.Plus, ModeType.Curse)]
    public class Curse : Mode
    {
        public override string Name => "저주";
        public override string Description => "관전자의 수에 비례해 능력치가 하락합니다.";
        public override string Detail =>
"""
플레이어는 자신을 바라보는 <b>관전자</b>의 수를 확인할 수 있습니다.
<b>관전자</b>의 수에 비례해 다음 능력치가 하락합니다.
• 이동 속도
• 점프력 하락
• 초당 체력 하락
• (SCP-079) 전력 하락

<b>관전자</b>의 수가 5명씩 넘어갈 때마다 특수한 효과가 적용됩니다.
5명 이상 - SCP-939 안개 효과
10명 이상 - 블라인드 효과
15명 이상 - 아이템이 자꾸 버려짐
20명 이상 - <b><color=#0BBDF9>S</color><color=#12B2F9>C</color><color=#19A7FA>P</color><color=#209CFA>-</color><color=#2792FB>2</color><color=#2E87FB>4</color><color=#357CFC>4</color> <color=#4367FD>효</color><color=#4A5CFE>과</color></b>
25명 이상 - <i><color=#CCF2FF>섬</color><color=#D6DBF7>광</color> <color=#EAADE7>효</color><color=#F496DF>과</color></i>
30명 이상 - <b><i><color=#FF1F1F>손</color> <color=#EB335F>절</color><color=#E13D7F>단</color> <color=#CD51BF>효</color><color=#C35BDF>과</color></i></b>
""";
        public override string Color => "000000";
        public override string Suggester => "광대 (@yun_0n0)";

        public static Curse Instance;

        public override void OnEnabled()
        {
            Timing.RunCoroutine(OnModeStarted());
        }

        public IEnumerator<float> OnModeStarted()
        {
            while (true)
            {
                foreach (var player in Player.List)
                {
                    if (player.IsAlive)
                    {
                        int s = player.CurrentSpectatingPlayers.Count();
                        player.AddHint("저주", $"현재 {s}명이 당신을 관전하고 있습니다.", 1.2f);

                        player.GetEffect(EffectType.Slowness).Intensity = (byte)(1.5 * s);
                        player.GetEffect(EffectType.HeavyFooted).Intensity = (byte)(1.5 * s);
                        player.Hurt(0.15f * s);

                        if (player.Role is Scp079Role scp079)
                            scp079.Energy -= 0.15f * s;

                        if (s >= 5)
                            player.EnableEffect(EffectType.AmnesiaVision);

                        else
                            player.DisableEffect(EffectType.AmnesiaVision);

                        if (s >= 10)
                            player.EnableEffect(EffectType.Blinded);

                        else
                            player.DisableEffect(EffectType.Blinded);

                        if (s >= 15)
                        {
                            if (UnityEngine.Random.Range(1, 11) == 1)
                            {
                                Item item = player.Items.GetRandomValue(x => !x.IsAmmo);
                                player.CurrentItem = item;

                                Timing.CallDelayed(1, () =>
                                {
                                    if (player.CurrentItem == item)
                                        player.DropItem(item);
                                });
                            }
                        }

                        if (s >= 20)
                            player.EnableEffect(EffectType.Hypothermia, 50);

                        else
                            player.DisableEffect(EffectType.Blinded);

                        if (s >= 25)
                            player.EnableEffect(EffectType.Flashed, 1, 0.3f);

                        else
                            player.DisableEffect(EffectType.Flashed);

                        if (s >= 30) 
                            player.EnableEffect(EffectType.SeveredHands);

                        else
                            player.DisableEffect(EffectType.SeveredHands);
                    }
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
