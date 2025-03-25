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
    public static class SSSBase
    {
        public static void Refresh(string userId)
        {

            if (!Player.TryGet(userId, out Player player))
                return;

            if (!SSSBases.ContainsKey(userId))
            {
                SSSBases.Add(userId, new()
                {
                    SSSBase = new(),
                    SBase = null
                });
            }

            SSSBases[userId].SSSBase.Clear();
            if (SSSBases[userId].SBase != null)
            {
                SettingBase.Unregister(settings: SSSBases[userId].SBase);

                SSSBases[userId].SBase = null;
            }

            var userCache = UsersManager.UsersCache[userId];

            var header1 = new HeaderSetting("<align=left><size=110%>ⓘ 유저 정보</align></size>", "유저와 관련된 설정 모음입니다.");
            var text1 = new SSTextArea(1, $"👤 유저 ID: {userId}<line-height=0>\n</line-height><align=right><mark=#5865f215><link=https://steamcommunity.com/profiles/{player.RawUserId}>ㅤ자신의 스팀 프로필 보기ㅤㅤ</link></mark></align>");
            var text2 = new SSTextArea(2, $"⭐ EXP: {userCache[0]}");
            var text3 = new SSTextArea(3, $"💫 RP: {userCache[1]}");
            var text4 = new SSTextArea(4, $"💎 Cash: {userCache[2]}<line-height=0>\n</line-height><align=right><mark=#5865f215><link=https://discord.gg/h4AKgks7VMV>ㅤ🏪 Cash 충전하기ㅤㅤ</link></mark></align>");

            var header2 = new HeaderSetting("<align=left><size=110%>🎮 모드</align></size>", "모드에 관련된 설정 모음입니다.");
            var text5 = new SSTextArea(103, "<align=left><size=105%>📝 모드 설명</align></size>\n자세한 설명을 조회할 모드를 선택해주세요.");
            var dropdown1 = new DropdownSetting(101, "✅ 활성화된 모드", EnabledModeList.Select(x => $"{x.GetModeData().Name} ({x.GetModeData().Category})"), header: header1, onChanged: (p, sb) =>
            {
                string modeName = sb.Base.DebugValue.Split('(')[1].Trim();
                var mode = ModeList.Keys.Select(x => x.GetModeData().Name).Contains(modeName) ? (ModeList.Keys.FirstOrDefault(x => x.GetModeData().Name == modeName)).GetModeData().Type : ModeType.Develop;
                SSTextArea text = (SSTextArea)SSSBases[userId].SSSBase.FirstOrDefault(x => x.SettingId == 103);
                text.SendTextUpdate($"<align=left><size=105%>📝 모드 설명</align></size>\n{(mode.GetModeData().Description)}\n<size=80%>{mode.GetModeData().Detail}</size>");
            });
            var dropdown2 = new DropdownSetting(102, "📃 전체 모드", ModeList.Keys.Select(x => $"{x.GetModeData().Name} ({x.GetModeData().Category}, {x.GetModeData().Info})"), header: header1, onChanged: (p, sb) =>
            {
                string modeName = sb.Base.DebugValue.Split('(')[1].Trim();
                var mode = ModeList.Keys.Select(x => x.GetModeData().Name).Contains(modeName) ? (ModeList.Keys.FirstOrDefault(x => x.GetModeData().Name == modeName)).GetModeData().Type : ModeType.Develop;
                SSTextArea text = (SSTextArea)SSSBases[userId].SSSBase.FirstOrDefault(x => x.SettingId == 103);
                text.SendTextUpdate($"<align=left><size=105%>📝 모드 설명</align></size>\n{(mode.GetModeData().Description)}\n<size=80%>{mode.GetModeData().Detail}</size>");
            });

            var header3 = new HeaderSetting("<align=left><size=110%>⚙️ 기타</align></size>", "그 외 설정 모음입니다.");
            var button1 = new ButtonSetting(200, "🔄 새로고침", "모든 정보를 새로고침합니다.", 1, header: header3, onChanged: (p, sb) =>
            {
                Refresh(userId);
            });

            var settings = new List<ServerSpecificSettingBase>
            {
                header1.Base, text1, text2, text3, text4, header2.Base, dropdown1.Base, dropdown2.Base, text5, header3.Base, button1.Base
            };
            IEnumerable<SettingBase> settingBases = new SettingBase[]
            {
            dropdown1, dropdown2, button1
            };

            SSSBases[userId].SBase = settingBases;
            SSSBases[userId].SSSBase.AddRange(settings);

            SettingBase.Register(settingBases);
            ServerSpecificSettingsSync.SendToPlayer(player.ReferenceHub, settings.ToArray());
        } 
    }
}
