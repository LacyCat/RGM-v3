using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommandSystem;
using Exiled.API.Extensions;
using Exiled.API.Features;
using MultiBroadcast.API;
using PlayerRoles;
using RGM.API;
using RGM.API.Features;
using RGM.Modes;
using UnityEngine;

using static RGM.Variables.ServerManagers;

namespace RGM.Commands.RemoteAdminCommands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class AddItem : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string UserId = Tools.TryGetUserId(arguments.At(0));
            string args = string.Join(" ", arguments.Skip(1)).Trim();

            if (arguments.Count < 2)
            {
                response = "아이템추가 <player> <item name>";
                return false;
            }
            else if (Products.Select(x => x.Name).Contains(args))
            {
                List<string> uc = UsersManager.UsersCache[UserId];

                if (uc[18] == "0")
                {
                    uc[18] = args;
                    UsersManager.UsersCache[UserId] = uc;
                    response = "Successfully add item.";

                    UsersManager.SaveUsers();
                    return true;
                }
                else
                {
                    uc[18] += $"/{args}";
                    UsersManager.UsersCache[UserId] = uc;
                    response = "Successfully add item.";

                    UsersManager.SaveUsers();
                    return true;
                }
            }
            else
            {
                response = "This item is not exist.";
                return false;
            }
        }

        public string Command { get; } = "item";

        public string[] Aliases { get; } = { "아이템", "아이템추가", "additem" };

        public string Description { get; } = "특정 유저에게 아이템을 지급합니다.";

        public bool SanitizeResponse { get; } = true;
    }
}
