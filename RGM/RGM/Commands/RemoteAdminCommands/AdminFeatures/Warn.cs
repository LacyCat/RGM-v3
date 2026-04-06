using System;
using System.Linq;
using CommandSystem;
using RGM.API.Features;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AddWarn : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string userId = Tools.TryGetUserId(arguments.At(0));
            string args = string.Join(" ", arguments.Skip(1)).Trim();

            bool flag = userId.AddWarn(args, out response, arguments);
            return flag;
        }

        public string Command { get; } = "addwarn";

        public string[] Aliases { get; } = { "경고추가", "경고" };

        public string Description { get; } = "특정 유저에게 경고를 부여합니다.";

        public bool SanitizeResponse { get; } = true;
    }
}
