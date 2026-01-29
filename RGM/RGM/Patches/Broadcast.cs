using Exiled.API.Features;

using RGM.API.Features;
using System.Linq;

namespace RGM.Patches
{
    public class BroadcastPatch
    {
        public static void MapBroadcastPostfix(ushort duration, string message, Broadcast.BroadcastFlags type, bool shouldClearPrevious)
        {
            foreach (var player in Player.List)
            {
                player.AddBroadcast(duration, message, tag: $"{duration} {message.Split(' ').Count()} {type} {shouldClearPrevious}");
            }
        }
    }
}
