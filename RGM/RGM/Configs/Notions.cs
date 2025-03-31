using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGM
{
    public class Notions
    {
        public static string StartModeDescription { get; set; } =
"""
<size=30><b><color=#{ModeColor}>{CurrentMode}</color></b></size>
<size=25>{ModeDescription}</size> <size=20>({ModeInfo})</size>
{CurrentSubMode}<size=15>[ESC] -> [Settings] -> [Server-specific]로 이동하여 자세한 설명을 확인해보세요.</size>
""";
        public static string LateJoinModeDescription { get; set; } =
"""
<size=20>현재 진행중인 모드</size>
<size=30><b><color=#{ModeColor}>{CurrentMode}</color></b></size>
<size=25>{ModeDescription}</size> <size=20>({ModeInfo})</size>
{CurrentSubMode}<size=15>[ESC] -> [Settings] -> [Server-specific]로 이동하여 자세한 설명을 확인해보세요.</size>
""";
        public static string WelcomeMessage { get; set; } = "<size=25><b>!랜덤게임모드 (RGM)</b>에 오신 것을 환영안합니다.</size>";
        public static string LobbyMessage { get; set; } =
"""
<size=120><b><color=#B45F04>💀</color> ({Logo}) <color=#FA8258>💀</color></b></size>
<size=80><b>[!RGM] !랜덤게임모드</b></size> <size=25>(ver. {Version})</size>
<align=right><size=20><b><i><color=#3F0808>서</color><color=#430F13>버</color><color=#48161F>는</color> <color=#522536>오</color><color=#572D42>늘</color><color=#5B344E>을</color> <color=#654365>마</color><color=#6A4A71>지</color><color=#6F527D>막</color><color=#735988>으</color><color=#786194>로</color> <color=#8270AC>폐</color><color=#8777B7>업</color><color=#8B7EC3>합</color><color=#9086CF>니</color><color=#958DDA>다</color><color=#9A95E6>.</color><color=#9F9CF2>.</color></i></b></size></align>
<align=left>
Exp: {Exp}
RP: {RP}
<b>Cash: ₩{Cash}</b>

{FirstMark} [1] {First} | {FirstVote}
{SecondMark} [2] {Second} | {SecondVote}
{ThirdMark} [3] {Third} | {ThirdVote}
{FourthMark} [4] {Fourth} | {FourthVote}

<color=#{ModeColor}><b>{ModeName}</b></color>
{ModeDescription}{Lines}
<size=25>tip. <i>{Tip}</i></size>
</align>








""";
    }
}
