using CustomRendering;
using DAONTFT.Core.TFT;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using MEC;
using Mirror;
using MultiBroadcast;
using PlayerRoles;
using ProjectMER.Features;
using Respawning;
using RGM.API.Features;
using RGM.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UserSettings.ServerSpecific;
using static RGM.Variables.Variable;

namespace RGM.Modes
{
    [Mode(ModeCategory.Public, ModeInfo.Lock, ModeType.Rank)]
    public class Rank : Mode
    {
        public override string Name => "경쟁전";
        public override string Description => "직업별로 변칙성, 가젯, 기어를 설정하세요.";
        public override string Detail =>
"""
모든 능력들은 스폰 후, 30초 뒤에 적용됩니다.

[ESC] -> [Settings] -> [Server-specific]
""";
        public override string Color => "ea524c";

        CoroutineHandle _onModeStarted;

        public override void OnEnabled()
        {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                var abilityAttribute = type.GetCustomAttribute<RankAbilityAttribute>();

                if (abilityAttribute == null)
                    continue;

                if (!typeof(RankAbility).IsAssignableFrom(type))
                    continue;

                RankInfo.RankAbilities.Add(abilityAttribute.Type, new RankAbilityData
                {
                    Type = type,
                    Name = abilityAttribute.Name,
                    Description = abilityAttribute.Description,
                    Emoji = abilityAttribute.Emoji,
                    RankAbilityType = abilityAttribute.Type,
                    RankCategory = abilityAttribute.RankCategory,
                    RankAbilityCategory = abilityAttribute.RankAbilityCategory
                });
            }

            RankSetting.Init();

            ServerSpecificSettingsSync.ServerOnSettingValueReceived += ServerSpecificSettings.OnSSInput;

            _onModeStarted = Timing.RunCoroutine(OnModeStarted());
        }

        public override void OnDisabled()
        {
            ServerSpecificSettingsSync.ServerOnSettingValueReceived -= ServerSpecificSettings.OnSSInput;

            Timing.KillCoroutines(_onModeStarted);
        }

        IEnumerator<float> OnModeStarted()
        {
            yield return Timing.WaitForSeconds(30);

            foreach (var player in RankInfo.PlayerRankAbilities.Keys.ToList())
            {
                RankCategory rankCategory = player.GetRankCategory();

                List<RankAbilityType> list = RankInfo.PlayerRankAbilities[player][rankCategory];

                foreach (var ability in list)
                    AddRankAbility(player, ability);
            }
        }

        // 플레이어에게 특정 능력을 부여
        void AddRankAbility(Player player, RankAbilityType type)
        {
            Log.Info("AddTFTAbility called with " + player.DisplayNickname + " and " + type);

            if (!RankInfo.RankAbilities.ContainsKey(type))
            {
                Log.Error($"TFTAbility {type} not found.");

                return;
            }

            var RankAbilityData = RankInfo.RankAbilities[type];
            RankAbility RankAbility;

            try
            {
                RankAbility = Activator.CreateInstance(RankInfo.RankAbilities[type].Type) as RankAbility;
            }
            catch (Exception e)
            {
                Log.Error($"An error occurred while trying to create an instance of {RankAbilityData.Name}: {e}");
                return;
            }

            if (RankAbility == null)
            {
                Log.Error($"An error occurred while trying to create an instance of {RankAbilityData.Name}. The instance is null.");
                return;
            }

            RankAbility.Data = RankAbilityData;
            RankAbility.Owner = player;
            RankAbility.OnEnabled();
        }
    }
}
