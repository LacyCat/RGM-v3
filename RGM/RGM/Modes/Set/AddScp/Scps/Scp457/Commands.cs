using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;

namespace RGM.Modes.Sets.AddScp.Scps;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class SetScp457 : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        Player player = Player.Get(arguments.At(0));

        Scp457.Create(player);

        response = $"<b><i>{player.DisplayNickname}</i></b>을(를) SCP-457로 만드는데 성공했습니다.";
        return true;
    }

    public string Command { get; } = "setscp457";

    public string[] Aliases { get; } = { "scp457" };

    public string Description { get; } = "추가 SCPㅣ특정 유저를 SCP-457로 만듭니다.";
}