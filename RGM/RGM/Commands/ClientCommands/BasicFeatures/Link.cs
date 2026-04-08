using System;
using CommandSystem;
using Exiled.API.Features;
using RGM.API.Features;

namespace RGM.Commands.ClientCommands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Link : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            string code = UsersManager.UsersCache[player.UserId][14];

            response = $"디스코드 연동 코드: {code}";
            return true;
        }

        public string Command { get; } = "link";

        public string[] Aliases { get; } = { "연동" };

        public string Description { get; } =  "[RGM] 디스코드 연동 링크를 받아보세요.";

        public bool SanitizeResponse { get; } = true;
    }
}
