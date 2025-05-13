using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGM.API.Features
{
    public static class HintManager
    {
        private static Dictionary<Player, List<string>> _playerHints = new Dictionary<Player, List<string>>();

        public static IEnumerator<float> OnStarted()
        {
            while (true)
            {
                foreach (var player in Player.List.Where(x => x.IsAlive && _playerHints.ContainsKey(x) && -_playerHints[x].Count > 0))
                {
                    player.ShowHint($"{string.Join("\n", _playerHints[player])}", 1.2f);
                }

                yield return Timing.WaitForSeconds(1);
            }
        }

        public static void AddHint(this Player player, string hint, float duration = 3)
        {
            if (!_playerHints.ContainsKey(player))
                _playerHints[player] = new List<string>();

            if (!_playerHints[player].Contains(hint))
            {
                _playerHints[player].Add(hint);

                Timing.CallDelayed(duration, () =>
                {
                    _playerHints[player].Remove(hint);
                });
            }
        }
    }
}
