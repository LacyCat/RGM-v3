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

namespace RGM.UserSettings
{
    public static class SSSBase
    {
        public static void Refresh(Player player)
        {
            if (!SSSBases.ContainsKey(player))
                SSSBases.Add(player, new());

            ServerSpecificSettingsSync.SendToPlayer(player.ReferenceHub, SSSBases[player].ToArray());
        }

        public static void SetUserSettings(Player player)
        {
            var userCache = UsersManager.UsersCache[player.UserId];

            var settings = new List<ServerSpecificSettingBase>
            {
                new HeaderSetting("<align=left><size=110%>ⓘ 유저 정보</align></size>", "유저와 관련된 설정 모음집입니다.").Base,
                new SSTextArea(1, $"👤 유저 ID: {player.UserId}<line-height=0>\n</line-height><align=right><mark=#5865f215><link=https://steamcommunity.com/profiles/{player.RawUserId}>ㅤ자신의 스팀 프로필 보기ㅤㅤ</link></mark></align>"),
                new SSTextArea(2, $"⭐ EXP: {userCache[0]}"),
                new SSTextArea(3, $"💫 RP: {userCache[1]}"),
                new SSTextArea(4, $"💎 Cash: {userCache[2]}<line-height=0>\n</line-height><align=right><mark=#5865f215><link=https://discord.gg/h4AKgks7VMV>ㅤ🏪 Cash 충전하기ㅤㅤ</link></mark></align>")
            };

            SSSBases[player].AddRange(settings);

            ServerSpecificSettingsSync.ServerOnSettingValueReceived += (rh, sss) =>
            {
            };

            Refresh(player);
        }

        public static void SetModeSettings(Player player)
        {
            var settings = new List<ServerSpecificSettingBase>
            {
                   new HeaderSetting("<align=left><size=110%>🎮 모드</align></size>", "모드에 관련된 설정 모음입니다.").Base,
                   new DropdownSetting(101, "✅ 활성화된 모드", EnabledModeList.Select(x => $"{x.GetModeData().Name} ({x.GetModeData().Category}, {x.GetModeData().Info})"), header: settings[0]),
                   new DropdownSetting(102, "📃 전체 모드", ModeList.Keys.Select(x => $"{x.GetModeData().Name} ({x.GetModeData().Category}, {x.GetModeData().Info})"), header: settings[0]),
                   new SSTextArea(103, "<align=left><size=105%>📝 모드 설명</align></size>\n자세한 설명을 조회할 모드를 선택해주세요."),
            };

            SSSBases[player].AddRange(settings);

            ServerSpecificSettingsSync.ServerOnSettingValueReceived += (rh, sss) =>
            {
                if (new List<int> { 101, 102 }.Contains(sss.SettingId))
                {
                    var mode = ModeList.Values.FirstOrDefault(x => x.Name == sss.DebugValue);
                    settings[3].Label = $"<align=left><size=105%>📝 모드 설명</align></size>\n{mode.Description}\n<size=80%>{mode.Detail}</size>";
                }
            };

            SSSBases[player].AddRange(settings);
            Refresh(player);
        }
    }
}
