using System;
using CommandSystem;
using Exiled.API.Features;
using RGM.API.Features;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class GetPlayerInfo : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(arguments.At(0));

            response = Tools.GetPlayerInfo(player);

            return true;
        }

        public string Command { get; } = "getplayerinfo";

        public string[] Aliases { get; } = { "gpi", "정보조회", "정보" };

        public string Description { get; } = "특정 유저의 정보를 조회합니다.";

        public bool SanitizeResponse { get; } = true;
    }
}
