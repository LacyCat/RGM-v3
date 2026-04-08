using System;
using CommandSystem;
using Exiled.API.Features;
using RGM.API.Features;

namespace RGM.Commands.ClientCommands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Translator : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            string args = string.Join(" ", arguments);

            TranslationManager.Translate(args, "en",
            translated =>
            {
                player.SendConsoleMessage(translated, "white");
            },
            error =>
            {
                player.SendConsoleMessage($"{error}\n원문: {args}", "white");
            });

            response = "번역이 곧 제공됩니다..";
            return true;
        }

        public string Command { get; } = "번역";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "[RGM] 번역";

        public bool SanitizeResponse { get; } = true;
    }
}
