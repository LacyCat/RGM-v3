using CommandSystem;
using DAONTFT.Core.TFT;
using Exiled.API.Features;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using Hint = HintServiceMeow.Core.Models.Hints.Hint;

namespace RGM.Modes.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class StartSelect : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player player = Player.Get(sender);

        TFTBattle.StartUpgrade(new List<Player> { player });

        response = "선택창을 시작합니다.";
        return true;
    }

    public string Command { get; } = "startselect";

    public string[] Aliases { get; } = { "ss" };

    public string Description { get; } = "DAONㅣ지정한 유저를 위한 선택을 시작합니다.";
}