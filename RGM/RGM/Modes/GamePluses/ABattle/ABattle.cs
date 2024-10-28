using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using MultiBroadcast.API;
using PlayerRoles;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes;

public class ABattle
{
    public static ABattle Instance;

    public static bool IsFeverModeEnabled = false;

    public static Dictionary<AbilityType, Type> _abilities;
    public static Dictionary<AbilityType, List<AbilityType>> _synergyAbilities;
    public static Dictionary<Player, List<Ability>> _playerAbilities;
    public static Dictionary<Player, List<Vector3>> _playerWorkstation;
    public static Dictionary<Player, string> _playerVotes = new Dictionary<Player, string>();

    // 플러그인에 있는 모든 능력 검색
    public void OnEnabled()
    {
        Instance = this;

        _playerAbilities = new Dictionary<Player, List<Ability>>();
        _playerWorkstation = new Dictionary<Player, List<Vector3>>();
        _abilities = new Dictionary<AbilityType, Type>();
        _synergyAbilities = new Dictionary<AbilityType, List<AbilityType>>();

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            var abilityAttribute = type.GetCustomAttribute<AbilityAttribute>();

            if (abilityAttribute == null)
                continue;

            if (!typeof(Ability).IsAssignableFrom(type))
                continue;

            _abilities.Add(abilityAttribute.Type, type);

            var requiresAbilityAttribute = type.GetCustomAttribute<RequiresAbilityAttribute>();

            if (requiresAbilityAttribute != null)
                _synergyAbilities.Add(abilityAttribute.Type, requiresAbilityAttribute.Abilities.ToList());
        }

        Exiled.Events.Handlers.Player.Verified += OnVerified;
        Exiled.Events.Handlers.Player.Jumping += OnJumping;

        Timing.RunCoroutine(OnModeStarted());
    }

    public static IEnumerator<float> OnModeStarted()
    {
        foreach (var player in Player.List.Where(x => !x.IsNPC))
        {
            _playerWorkstation.Add(player, new List<Vector3>());
            _playerAbilities.Add(player, new List<Ability>());
        }

        Timing.CallDelayed(UnityEngine.Random.Range(1, 11), () =>
        {
            if (UnityEngine.Random.Range(1, 6) == 1)
                IsFeverModeEnabled = true;

            if (IsFeverModeEnabled)
                Server.ExecuteCommand($"/mp load ABattle");
        });

        while (true)
        {
            foreach (var player in Player.List.Where(x => !x.IsNPC))
            {
                try
                {
                    Hint CurrentHint = player.CurrentHint;
                    bool IsStatusHint = CurrentHint != null && (CurrentHint.Content.Contains("워크스테이션") || CurrentHint.Content.Contains("보유 업그레이드"));

                    if (CurrentHint == null || IsStatusHint)
                    {
                        if (player.IsAlive)
                            ShowStatus(player);
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            yield return Timing.WaitForSeconds(0.5f);
        }
    }

    public static Dictionary<string, string> RatingColor = new Dictionary<string, string>()
    {
        {"일반", "#A4A4A4"},
        {"희귀", "#2ECCFA"},
        {"영웅", "#FF00FF"},
        {"전설", "#ffd700"},
        {"신화", "#DF0101"},
        {"전용", "#F7819F"},
        {"시너지", "#DEEFED"}
    };

    public static string ColorFormat(string text)
    {
        return text.Replace("[시너지]", $"<color={RatingColor["시너지"]}>[시너지]</color>")
                    .Replace("[전용]", $"<color={RatingColor["전용"]}>[전용]</color>")
                    .Replace("[신화]", $"<color={RatingColor["신화"]}>[신화]</color>")
                    .Replace("[전설]", $"<color={RatingColor["전설"]}>[전설]</color>")
                    .Replace("[영웅]", $"<color={RatingColor["영웅"]}>[영웅]</color>")
                    .Replace("[희귀]", $"<color={RatingColor["희귀"]}>[희귀]</color>")
                    .Replace("[일반]", $"<color={RatingColor["일반"]}>[일반]</color>");
    }

    public static void ShowStatus(Player player)
    {
        if (_playerAbilities[player].Count() <= 0)
        {
            if (player.Role.Type == RoleTypeId.Scp079)
                player.ShowHint($"<align=left><b><size=22>레벨이 오를 때마다 능력을 획득할 수 있습니다.</size></b></align>", 1.2f);

            else
                player.ShowHint($"<align=left><b><size=22>워크스테이션 위에서 점프하면 능력을 획득할 수 있습니다.</size></b></align>", 1.2f);

        }
        else
        {
            string abilitiesText = string.Join(", ", _playerAbilities[player].Select(x => x.Data.Name));

            abilitiesText = ColorFormat(abilitiesText);

            player.ShowHint($"<align=left><b><size=25>보유 업그레이드</size></b>\n<size=20>{abilitiesText}</size></align>", 1.2f);
        }
    }

    public static string PickAbilityGrade(Player player, string force = null)
    {
        int grade = UnityEngine.Random.Range(1, 10001);
        string abilityGrade;
        if (force != null)
            abilityGrade = "[" + force.Substring(force.IndexOf('[') + 1, force.IndexOf(']') - force.IndexOf('[') - 1) + "]".Trim();

        else if (player.Role.Type == RoleTypeId.Scp079)
            abilityGrade = "[전용]";

        else
        {
            if (grade <= 5) // 0.05%
                abilityGrade = "[신화]";

            else if (grade <= 40) // 0.35%
                abilityGrade = "[전설]";

            else if (grade <= 250) // 2.1%
                abilityGrade = "[영웅]";

            else if (grade <= 1500) // 12.5%
                abilityGrade = "[희귀]";

            else if (grade <= 2000) // 5%
                abilityGrade = "[전용]";

            else // 80%
                abilityGrade = "[일반]";
        }

        return abilityGrade;
    }

    public static AbilityCategory GetAbilityCategory(Player player, string abilityGrade, bool get = true)
    {
        if (abilityGrade == "[일반]")
            return AbilityCategory.Normal;

        else if (abilityGrade == "[희귀]")
            return AbilityCategory.Rare;

        else if (abilityGrade == "[영웅]")
        {
            if (get)
            {
                Cassie.Clear();
                Server.ExecuteCommand($"/cassie_sl {player.DisplayNickname}(이)가 <color={RatingColor["영웅"]}>[영웅]</color> 업그레이드를 입수하였습니다.");
            }
            return AbilityCategory.Epic;
        }
        else if (abilityGrade == "[전설]")
        {
            if (get)
            {
                Cassie.Clear();
                Server.ExecuteCommand($"/cassie_sl {player.DisplayNickname}(이)가 <color={RatingColor["전설"]}>[전설]</color> 업그레이드를 입수하였습니다.");
            }
            return AbilityCategory.Legend;
        }
        else if (abilityGrade == "[신화]")
        {
            if (get)
            {
                Cassie.Clear();
                Server.ExecuteCommand($"/cassie_sl {player.DisplayNickname}(이)가 <color={RatingColor["신화"]}>[신화]</color> 업그레이드를 입수하였습니다.");
            }
            return AbilityCategory.Mythic;
        }
        else if (abilityGrade == "[전용]")
        {
            return AbilityCategory.Unique;
        }
        else
        {
            Cassie.Clear();
            Server.ExecuteCommand($"/cassie_sl {player.DisplayNickname}(이)가 <color={RatingColor["시너지"]}>[시너지]</color> 효과를 입수하였습니다.");
            return AbilityCategory.Synergy;
        }
    }

    public static void ApplyGiveAbility(Player player, string abilityGrade, Ability ability)
    {
        _playerAbilities[player].Add(ability);
        string styleName = ColorFormat($"[{ability.Data.Category}] {ability.Data.Name}");

        string Message = $"<size=20>{styleName}</size>\n<size=15>{ability.Data.Description}</size>";
        player.AddBroadcast(10, Message);
        player.SendConsoleMessage($"\n{Message}", "white");
    }

    public static IEnumerator<float> AddAbilityVote(Player player)
    {
        List<AbilityType> AbilitesVote = new List<AbilityType>();
        List<string> DisplayVote = new List<string>();
        int SelectedAbilityNumber = 0;

        for (int i = 1; i < 4; i++)
        {
            List<AbilityType> abilityList = AbilityCategoryExtensions.CategoryToAbilities(GetAbilityCategory(player, PickAbilityGrade(player), false));

            AbilitesVote.Add(Tools.GetRandomValue(abilityList));
        }

        for (int i = 1; i < 4; i++)
            DisplayVote.Add($"[{i}] {ColorFormat(_abilities[AbilitesVote[i - 1]].Name)}");


        for (int i = 1; i < 21; i++)
        {
            if (player.IsDead)
                yield break;

            player.ShowHint(
                $"<align=left><size=30>{string.Join("\n", DisplayVote)}</size>\n\n<size=25><b>{21 - i}초 안에 [.(번호)] 명령어로 원하는 능력을 선택하세요. (ex .1)</b></size></align>\n\n",
                1.2f);

            if (_playerVotes.ContainsKey(player))
            {
                SelectedAbilityNumber = int.Parse(_playerVotes[player]);
                break;
            }

            yield return Timing.WaitForSeconds(1f);
        }

        if (SelectedAbilityNumber == 0) SelectedAbilityNumber = UnityEngine.Random.Range(1, 4);

        ABattleExtensions.AddAbility(player, AbilitesVote[SelectedAbilityNumber - 1]);

        if (AbilitesVote.All(x => x == AbilitesVote[0]))
        {
            // Timing.RunCoroutine(AddAbility(player, "[시너지] 중복 기연"));

            for (int i = 1; i < 3; i++)
                ABattleExtensions.AddAbility(player, AbilitesVote[0]);
        }
    }

    // 플레이어에게 특정 능력을 부여
    public void AddAbility(Player player, AbilityType type)
    {
        if (!_abilities.ContainsKey(type))
            return;

        if (!_playerAbilities.ContainsKey(player))
            _playerAbilities.Add(player, []);

        if (_playerAbilities[player].Any(x => x.Data.Type == type))
            return;

        var abilityType = _abilities[type];
        var abilityAttribute = abilityType.GetCustomAttribute<AbilityAttribute>();
        var requiresAbilityAttribute = abilityType.GetCustomAttribute<RequiresAbilityAttribute>();
        Ability ability;
        try
        {
             ability = Activator.CreateInstance(_abilities[type]) as Ability;
        }
        catch (Exception e)
        {
            Log.Error($"An error occurred while trying to create an instance of {abilityType.Name}: {e}");
            return;
        }

        if (ability == null)
            return;

        ability.Data = new AbilityData
        {
            Name = abilityAttribute.Name,
            Description = abilityAttribute.Description,
            Category = requiresAbilityAttribute != null && requiresAbilityAttribute.Abilities.Length > 0
                ? AbilityCategory.Synergy
                : abilityAttribute.Category,
            Type = abilityAttribute.Type,
        };
        ability.Owner = player;
        ability.OnEnabled();

        _playerAbilities[player].Add(ability);

        EnableSynergyAbility(_playerAbilities[player]);
    }

    // 플레이어에게 시너지 능력 부여
    private void EnableSynergyAbility(List<Ability> abilities)
    {
        foreach (var synergy in _synergyAbilities.Where(synergy =>
                     synergy.Value.All(req => abilities.Any(a => a.Data.Type == req))))
        {
            if (!_abilities.TryGetValue(synergy.Key, out var synergyAbilityType))
                continue;

            Ability synergyAbility;
            try
            {
                synergyAbility = Activator.CreateInstance(synergyAbilityType) as Ability;
            }
            catch (Exception e)
            {
                Log.Error($"An error occurred while trying to create an instance of {synergyAbilityType.Name}: {e}");
                continue;
            }

            if (synergyAbility == null)
                continue;

            synergyAbility.OnEnabled();
            abilities.Add(synergyAbility);
        }
    }

    // 플레이어로부터 특정 능력 제거
    public void RemoveAbility(Player player, AbilityType type)
    {
        if (!_playerAbilities.TryGetValue(player, out var playerAbility))
            return;

        var ability = playerAbility.FirstOrDefault(x => x.Data.Type == type);

        if (ability == null)
            return;

        _playerAbilities[player].Remove(ability);

        RemoveSynergyAbility(_playerAbilities[player]);
    }

    public void RemoveAbility(Player player, Ability ability)
    {
        if (ability == null)
            return;

        if (!_playerAbilities.TryGetValue(player, out var playerAbility))
            return;

        if (!playerAbility.Contains(ability))
            return;

        _playerAbilities[player].Remove(ability);

        RemoveSynergyAbility(_playerAbilities[player]);
    }

    // 플레이어로부터 시너지 능력 확인 후 제거
    private void RemoveSynergyAbility(List<Ability> abilities)
    {
        foreach (var synergy in _synergyAbilities)
        {
            if (!synergy.Value.All(req => abilities.Any(a => a.Data.Type == req)) &&
                abilities.Any(a => a.Data.Type == synergy.Key))
            {
                var ability = abilities.First(a => a.Data.Type == synergy.Key);
                abilities.Remove(ability);
            }
        }
    }

    // 플레이어로부터 모든 능력 제거
    public void RemoveAllAbilities(Player player)
    {
        if (!_playerAbilities.TryGetValue(player, out var playerAbility))
            return;

        _playerAbilities.Remove(player);
    }

    // 플레이어의 모든 능력 가져오기
    public List<Ability> GetAbilities(Player player)
    {
        return _playerAbilities.TryGetValue(player, out var playerAbility) ? playerAbility : new List<Ability>();
    }

    // 플레이어의 특정 능력 가져오기
    public Ability GetAbility(Player player, AbilityType type)
    {
        return GetAbilities(player).FirstOrDefault(x => x.Data.Type == type);
    }

    // 플레이어가 특정 능력을 가지고 있는지 확인
    public bool HasAbility(Player player, AbilityType type)
    {
        return GetAbility(player, type) != null;
    }

    public List<AbilityType> GetRandomAbilities(AbilityCategory category, int count)
    {
        var abilities = _abilities.Where(x => x.Value.GetCustomAttribute<AbilityAttribute>().Category == category).ToList();

        abilities.ShuffleList();

        return abilities.Take(count).Select(x => x.Key).ToList();
    }

    public static void OnVerified(Exiled.Events.EventArgs.Player.VerifiedEventArgs ev)
    {
        if (!_playerAbilities.ContainsKey(ev.Player))
        {
            _playerWorkstation.Add(ev.Player, new List<Vector3>());
            _playerAbilities.Add(ev.Player, new List<Ability>());
        }
    }

    public static void OnJumping(Exiled.Events.EventArgs.Player.JumpingEventArgs ev)
    {
        if (Physics.Raycast(ev.Player.Position, Vector3.down, out RaycastHit hit, 5, (LayerMask)1))
        {
            if (hit.transform != null)
            {
                Transform WorkStation = hit.transform.parent.parent;

                if (WorkStation.name.Contains("Work Station") && !_playerWorkstation[ev.Player].Contains(WorkStation.position))
                {
                    _playerWorkstation[ev.Player].Add(WorkStation.position);

                    Timing.RunCoroutine(AddAbilityVote(ev.Player));
                }
            }
        }
    }
}

public static class ABattleExtensions
{
    public static void AddAbility(this Player player, AbilityType type)
    {
        ABattle.Instance.AddAbility(player, type);
    }

    public static void RemoveAbility(this Player player, AbilityType type)
    {
        ABattle.Instance.RemoveAbility(player, type);
    }

    public static void RemoveAbility(this Player player, Ability ability)
    {
        ABattle.Instance.RemoveAbility(player, ability);
    }

    public static void RemoveAllAbilities(this Player player)
    {
        ABattle.Instance.RemoveAllAbilities(player);
    }

    public static List<Ability> GetAbilities(this Player player)
    {
        return ABattle.Instance.GetAbilities(player);
    }

    public static Ability GetAbility(this Player player, AbilityType type)
    {
        return ABattle.Instance.GetAbility(player, type);
    }

    public static bool HasAbility(this Player player, AbilityType type)
    {
        return ABattle.Instance.HasAbility(player, type);
    }
}

public static class Utilities
{
    public static void AddEffect(this Player player, EffectType type, byte intensity, float duration = 0f, bool addDuration = false)
    {
        var effects = player.ActiveEffects.Select(x => x.GetEffectType()).ToList();

        if (effects.Contains(type))
        {
            var effect = player.ActiveEffects.First(x => x.GetEffectType() == type);

            effect.ServerChangeDuration(duration, addDuration);

            if (effect.Intensity + intensity > 255)
                effect.Intensity = 255;
            else
                effect.Intensity += intensity;
        }
        else
        {
            player.EnableEffect(type, intensity, duration);
        }
    }

    public static void RemoveEffect(this Player player, EffectType type, byte intensity)
    {
        if (player.ActiveEffects.Any(x => x.GetEffectType() == type))
        {
            var effect = player.ActiveEffects.First(x => x.GetEffectType() == type);

            if (effect.Intensity - intensity <= 0)
                player.DisableEffect(type);
            else
                effect.Intensity -= intensity;
        }
    }
}