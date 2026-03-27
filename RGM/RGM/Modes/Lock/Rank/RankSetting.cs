using Discord;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using HarmonyLib;
using MEC;
using MonoMod.Utils;

using PlayerRoles;
using RGM.API.DataBases;
using RGM.API.Features;
using RGM.API.Interfaces;
using RGM.Commands.ClientCommands;
using RGM.Modes.SubClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnityEngine;
using UserSettings.ServerSpecific;
using static RGM.Variables.Variable;

namespace RGM.Modes
{
    public static class RankSetting
    {
        public static HeaderSetting RankHeader { get; private set; } = new HeaderSetting(190023, "경쟁전");

        public static DropdownSetting ClassDSetting { get; private set; }

        public static void Init()
        {
            List<DropdownSetting> dropdowns = new();

            foreach (var settingDict in RankInfo.변칙성)
            {
                dropdowns.Add(
                new DropdownSetting(
                id: 2044,
                label: $"<color={RankAbilityCategoryExtensions.GetColor(RankAbilityCategory.변칙성)}>변칙성</color> {(settingDict.Key.ToString().Contains("SCP") ? settingDict.Key.ToString().Replace("_", "-") : settingDict.Key.ToString().Replace("_", " "))}",
                options: RankInfo.변칙성[settingDict.Key].Keys,
                header: RankHeader
                ));
            }

            foreach (var settingDict in RankInfo.가젯)
            {
                dropdowns.Add(
                new DropdownSetting(
                id: 2045,
                label: $"<color={RankAbilityCategoryExtensions.GetColor(RankAbilityCategory.가젯)}>가젯</color> {(settingDict.Key.ToString().Contains("SCP") ? settingDict.Key.ToString().Replace("_", "-") : settingDict.Key.ToString().Replace("_", " "))}",
                options: RankInfo.가젯[settingDict.Key].Keys,
                header: RankHeader
                ));
            }

            foreach (var settingDict in RankInfo.기어)
            {
                dropdowns.Add(
                new DropdownSetting(
                id: 2046,
                label: $"<color={RankAbilityCategoryExtensions.GetColor(RankAbilityCategory.기어)}>기어</color>",
                options: RankInfo.기어.Keys,
                header: RankHeader
                ));
            }

            SettingBase.Register(dropdowns);
        }

        public static void OnSSInput(ReferenceHub sender, ServerSpecificSettingBase setting)
        {
            Player player = Player.Get(sender);

            if (setting is SSDropdownSetting dropdown)
            {
                RankAbilityCategory abilityCategory;

                if (setting.Label.Contains("변칙성"))
                    abilityCategory = RankAbilityCategory.변칙성;
                else if (setting.Label.Contains("가젯"))
                    abilityCategory = RankAbilityCategory.가젯;
                else
                    abilityCategory = RankAbilityCategory.기어;

                string rawLabel = setting.Label;

                string rankText = rawLabel.Split('>').Last().Trim();

                RankCategory rankCategory = Enum.GetValues(typeof(RankCategory))
                    .Cast<RankCategory>()
                    .FirstOrDefault(x =>
                    {
                        string name = x.ToString();
                        name = name.Contains("SCP") ? name.Replace("_", "-") : name.Replace("_", " ");
                        return rankText.Contains(name);
                    });

                string selectedOption = dropdown.Options.ElementAt(dropdown.SyncSelectionIndexValidated);

                RankAbilityType abilityType;

                if (abilityCategory == RankAbilityCategory.변칙성)
                    abilityType = RankInfo.변칙성[rankCategory][selectedOption].Item2;
                else if (abilityCategory == RankAbilityCategory.가젯)
                    abilityType = RankInfo.가젯[rankCategory][selectedOption].Item2;
                else
                    abilityType = RankInfo.기어[selectedOption].Item2;

                if (!RankInfo.PlayerRankSettingAbilities.ContainsKey(player))
                    RankInfo.PlayerRankSettingAbilities[player] = new();

                if (!RankInfo.PlayerRankSettingAbilities[player].ContainsKey(rankCategory))
                    RankInfo.PlayerRankSettingAbilities[player][rankCategory] = new();

                var list = RankInfo.PlayerRankSettingAbilities[player][rankCategory];

                if (!list.Contains(abilityType))
                    list.Add(abilityType);
            }
        }
    }
}
