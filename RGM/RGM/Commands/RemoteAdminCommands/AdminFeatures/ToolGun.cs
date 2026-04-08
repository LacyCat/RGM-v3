using CommandSystem;
using Exiled.API.Features;
using System;
using ProjectMER.Features.ToolGun;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ToolGun : ICommand, IUsageProvider
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(arguments.At(0));

            ToolGunItem.TryAdd(player);

            response = "툴건을 지급했습니다.";
            return true;
        }

        public string Command { get; } = "툴건";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "툴건을 얻으세요.";

        public bool SanitizeResponse { get; } = true;
        public string[] Usage { get; } = { "%player%" };
    }
}
