using System;
using System.Linq;
using CommandSystem;
using RGM.API.Features;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SetPlayerInfo : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string userId = Tools.TryGetUserId(arguments.At(0));
            int index = int.Parse(arguments.At(1));
            string value = string.Join(" ", arguments.Skip(2));

            string previousValue = UsersManager.UsersCache[userId][index];

            UsersManager.UsersCache[userId][index] = value;

            UsersManager.SaveUsers();

            response = $"{userId}의 {index}번째 값을 변경했습니다.\n[{previousValue}] -> [{value}]";
            return true;
        }

        public string Command { get; } = "정보변경";

        public string[] Aliases { get; } = { "setplayerinfo" };

        public string Description { get; } = "특정 유저의 특정 값을 변경합니다.";

        public bool SanitizeResponse { get; } = true;
    }
}
