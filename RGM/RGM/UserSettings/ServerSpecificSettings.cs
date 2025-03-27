using Discord;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using RGM.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;
using static RGM.Variables.ServerManagers;
using static System.Net.Mime.MediaTypeNames;

namespace RGM.UserSettings
{
    public static class ServerSpecificSettings
    {
        public static void RegisterSettings(Player player)
        {
            var userCache = UsersManager.UsersCache[player.UserId];

            var header1 = new HeaderSetting("<align=left><size=110%>ⓘ 유저 정보</align></size>");
            var text1 = new TextInputSetting(1, $"👤 유저 ID: {player}<line-height=0>\n</line-height><align=right><mark=#5865f215><link=https://steamcommunity.com/profiles/{player.RawUserId}>ㅤ자신의 스팀 프로필 보기ㅤㅤ</link></mark></align>", header: var header1);
            var text2 = new TextInputSetting(2, $"⭐ EXP: {userCache[0]}", header: header1);
            var text3 = new TextInputSetting(3, $"💫 RP: {userCache[1]}", header: header1);
            var text4 = new TextInputSetting(4, $"💎 Cash: {userCache[2]}<line-height=0>\n</line-height><align=right><mark=#5865f215><link=https://discord.gg/h4AKgks7VMV>ㅤ🏪 Cash 충전하기ㅤㅤ</link></mark></align>", header: var header1);

            var header2 = new HeaderSetting("<align=left><size=110%>🎮 모드</align></size>");
            var text5 = new TextInputSetting(103, "<align=left><size=105%>📝 모드 설명</align></size>\n자세한 설명을 조회할 모드를 선택해주세요.", header: header1);
            var dropdown1 = new DropdownSetting(101, "✅ 활성화된 모드", EnabledModeList.Select(x => $"{x.GetModeData().Name} ({x.GetModeData().Category})"), header: header1, onChanged: (p, sb) =>
            {
                string modeName = sb.Base.DebugValue.Split('(')[1].Trim();
                var mode = ModeList.Keys.Select(x => x.GetModeData().Name).Contains(modeName) ? (ModeList.Keys.FirstOrDefault(x => x.GetModeData().Name == modeName)).GetModeData().Type : ModeType.Develop;
                text5.Base.SendTextUpdate($"<align=left><size=105%>📝 모드 설명</align></size>\n{(mode.GetModeData().Description)}\n<size=80%>{mode.GetModeData().Detail}</size>");
            });
            var dropdown2 = new DropdownSetting(102, "📃 전체 모드", ModeList.Keys.Select(x => $"{x.GetModeData().Name} ({x.GetModeData().Category}, {x.GetModeData().Info})"), header: header1, onChanged: (p, sb) =>
            {
                string modeName = sb.Base.DebugValue.Split('(')[1].Trim();
                var mode = ModeList.Keys.Select(x => x.GetModeData().Name).Contains(modeName) ? (ModeList.Keys.FirstOrDefault(x => x.GetModeData().Name == modeName)).GetModeData().Type : ModeType.Develop;
                text5.Base.SendTextUpdate($"<align=left><size=105%>📝 모드 설명</align></size>\n{(mode.GetModeData().Description)}\n<size=80%>{mode.GetModeData().Detail}</size>");
            });

            var header3 = new HeaderSetting("<align=left><size=110%>⚙️ 기타</align></size>");
            var button1 = new ButtonSetting(200, "🔄 새로고침", "모든 정보를 새로고침합니다.", 1, header: header3, onChanged: (p, sb) =>
            {
                Refresh(player);
            });

            SettingBase.Register(new[] { header1, header2, header3 }, (p) => { return p == player; });
        }

        public static void UnregisterSettings(Player player)
        {
            SettingBase.Unregister((p) => { return p == player; }, new[] { header1, header2, header3 });
        }
    }
}
