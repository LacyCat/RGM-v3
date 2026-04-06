using System;
using System.Linq;
using CommandSystem;
using RGM.API.Features;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AddItem : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string userId = Tools.TryGetUserId(arguments.At(0));
            string args = string.Join(" ", arguments.Skip(1)).Trim();

            bool flag = userId.AddProduct(args, out response, arguments);
            return flag;
        }

        public string Command { get; } = "item";

        public string[] Aliases { get; } = { "아이템", "아이템추가", "additem" };

        public string Description { get; } = "특정 유저에게 아이템을 지급합니다.";

        public bool SanitizeResponse { get; } = true;
    }
}
