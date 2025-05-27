using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using MultiBroadcast.API;
using PlayerRoles;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ShowText : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            string text = string.Join(" ", arguments);

            Tools.CreateText(player.Position, player.Rotation * Quaternion.Euler(0, 180, 0), $"{text.Split('_')[0]}", float.Parse(text.Split('_')[1]));

            response = "Successfully create text.";
            return true;
        }

        public string Command { get; } = "텍스트";

        public string[] Aliases { get; } = { "text" };

        public string Description { get; } = "[RGM] 텍스트를 추가합니다.";

        public bool SanitizeResponse { get; } = true;
    }
}