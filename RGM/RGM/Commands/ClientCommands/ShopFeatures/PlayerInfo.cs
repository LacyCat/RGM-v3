using System;
using CommandSystem;
using Exiled.API.Features;
using RGM.API.Features;

namespace RGM.Commands.ClientCommands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class PlayerInfo : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            response = $"성공적으로 플레이어 정보를 불러왔습니다.";

            player.SendConsoleMessage(Tools.GetPlayerInfo(player), "white");

            return true;
        }

        public string Command { get; } = "정보";

        public string[] Aliases { get; } = { "stat", "info" };

        public string Description { get; } = "[RGM] 현재 자신의 정보를 확인합니다.";

        public bool SanitizeResponse { get; } = true;
    }
}
