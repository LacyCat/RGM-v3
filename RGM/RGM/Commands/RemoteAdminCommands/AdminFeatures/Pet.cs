using CommandSystem;
using Exiled.API.Features;
using RGM.Modes.SubClass;
using System;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Pet : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(arguments.At(0));

            if (CapybaraPet.Players.Contains(player))
            {
                CapybaraPet.Players.Remove(player);

                response = "Pet has been disabled.";
            }
            else
            {
                CapybaraPet.Create(player);

                response = "Pet has been enabled.";
            }

            return true;
        }

        public string Command { get; } = "pet";

        public string[] Aliases { get; } = { "펫" };

        public string Description { get; } = "펫을 켜거나 끕니다.";

        public bool SanitizeResponse { get; } = true;
    }
}
