using System;
using CommandSystem;
using RGM.API.Features;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PlayAudio : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Tools.PlayGlobalAudio(string.Join(" ", arguments));

            response = "Complete!";

            return true;
        }

        public string Command { get; } = "오디오재생";

        public string[] Aliases { get; } = { "playaudio" };

        public string Description { get; } = "오디오를 재생할 수 있습니다.";

        public bool SanitizeResponse { get; } = true;
    }
}
