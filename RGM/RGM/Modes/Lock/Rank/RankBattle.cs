using DAONTFT.Core.TFT;
using Exiled.API.Features;
using System;

namespace RGM.Modes
{
    public static class RankBattle
    {
        public static void AddRankAbility(Player player, RankAbilityType type)
        {
            Log.Info("AddRankAbility called with " + player.DisplayNickname + " and " + type);

            if (!RankInfo.RankAbilities.ContainsKey(type))
            {
                Log.Error($"RankAbility {type} not found.");

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

            RankInfo.PlayerRankAbilities[player].Add(RankAbility);

            RankAbility.Data = RankAbilityData;
            RankAbility.Owner = player;
            RankAbility.OnEnabled();
        }

        public static void RemoveAllRankAbilities(this Player player)
        {
            if (!RankInfo.PlayerRankAbilities.TryGetValue(player, out var playerRankAbility))
                return;

            foreach (var rankAbility in playerRankAbility)
                rankAbility.OnDisabled();

            RankInfo.PlayerRankSettingAbilities.Remove(player);
        }

        public static void Reset(this Player player)
        {
            player.RemoveAllRankAbilities();
        }
    }
}
