using Exiled.API.Features;

namespace RGM
{

    public static class Bc
    {
        public static void Message(this Player player, float duration, string content, bool isStack = true)
        {
            if (player == null) return;
            if (!isStack)
            {
                player.ClearBroadcasts();
                player.Broadcast((ushort)duration, $"<cspace=0.1em>" + content + "</cspace>", Broadcast.BroadcastFlags.Truncated);
                return;
            }

            if (player.GameObject.gameObject.TryGetComponent(out EssentialExtension ex))
            {
                ex.AddMessage(content, duration);
            }
        }
        public static void MessageClear(this Player player, bool isStack = true)
        {
            if (player == null) return;
            if (!isStack)
            {
                player.ClearBroadcasts();
                return;
            }

            if (player.GameObject.gameObject.TryGetComponent(out EssentialExtension ex))
            {
                ex.ClearMessage();
            }
        }
        public static void MessageAll(float duration, string content, bool isStack = true)
        {
            foreach (Player p in Player.List)
            {
                p.Message(duration, content, isStack);
            }
        }
        public static void MessageClearAll(bool isStack = true)
        {
            foreach (Player p in Player.List)
            {
                p.MessageClear(isStack);
            }
        }
    }
}
