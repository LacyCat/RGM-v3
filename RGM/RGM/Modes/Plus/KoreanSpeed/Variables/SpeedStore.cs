using System.Collections.Generic;
using Exiled.API.Features;
using MEC;

namespace RGM.Modes;

public class SpeedStore
{
    /// <summary>
    /// 모드 "한국인이 좋아하는 속도"에서 속도 관련 계산을 처리할 때 사용합니다.
    /// </summary>
    public static byte Count { get; set; }
    
    /// <summary>
    /// SCP 관련 코루틴을 저장합니다.
    /// </summary>
    private static readonly Dictionary<Player, Dictionary<int, CoroutineHandle>> ScpCoroutines = new();
    
    public static bool IsEnabled { get; private set; }
    
    public static void Clear() => Count = 0;

    public static void Ignition()
    {
        Count = 0;
        IsEnabled = true;
    }

    public static void Disable()
    {
        Count = 0;
        IsEnabled = false;
    }

    public static bool TryRemove(byte value,
        out string message)
    {
            if (Count - value < 0)
            {
                message = $"값이 너무 큽니다. 현재 횟수 {Count}보다 작거나 같게 하세요.";
                return false;
            }

            Count -= value;
            message = "해당 삭제 명령이 성공적으로 처리되었습니다.";
            KoreanSpeed.AddEffects();
            return true;
    }

    internal static void AddCoroutine(
        Player player,
        CoroutineHandle handle)
    {
        if (player == null) return;
        if (ScpCoroutines[player!].ContainsKey(handle.Key)) return;

        ScpCoroutines.Add(player!, new()
        {
            { handle.Key, handle }
        });
        
    }

    internal static void AddCoroutine(
        Player player,
        IEnumerable<CoroutineHandle> handle)
    {
        if (player == null) return;
        foreach (var items in handle)
        {
            if (ScpCoroutines[player!].ContainsKey(items.Key)) continue;
            ScpCoroutines.Add(player, new()
            {
                { items.Key, items }
            });
        }
    }

    internal static void RemoveCoroutine(
        Player player, 
        CoroutineHandle handle)
    {
        if (!ScpCoroutines.TryGetValue(player, out var coroutine)) return;
        
        Timing.KillCoroutines(coroutine[handle.Key]);
        ScpCoroutines[player].Remove(handle.Key);
    }
    
    internal static void RemoveCoroutine(
        Player player, 
        IEnumerable<CoroutineHandle> handle)
    {
        foreach (var items in handle) {
            if (!ScpCoroutines[player].ContainsKey(items.Key)) continue;

            Timing.KillCoroutines(ScpCoroutines[player][items.Key]);
            ScpCoroutines[player].Remove(items.Key);
        }
    }
}