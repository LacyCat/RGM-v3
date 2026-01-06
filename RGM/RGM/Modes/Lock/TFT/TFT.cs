using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using MEC;
using RemoteAdmin;
using RGM.API.Features;
using System.Collections.Generic;
using System.Linq;

namespace RGM.Modes;

[Mode(ModeCategory.Public, ModeInfo.Lock, ModeType.TFT)]
public class TFT : Mode
{
    public override string Name => "전략적 팀 전투";
    public override string Description => "전략적인 빌드를 구성하여 팀원을 승리로 이끄십시오.";
    public override string Detail =>
"""
증강은 한 사람당 총 3개를 확보할 수 있으며,

처음 라운드 시작시 20초 후에,

그 다음 300초마다 지급됩니다.
""";
    public override string Color => "ffd700";

    public static ABattle Instance;

    CoroutineHandle _onModeStarted;

    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Server.RoundEnded += OnRoundEnded;

        Exiled.Events.Handlers.Player.Verified += OnVerified;

        _onModeStarted = Timing.RunCoroutine(OnModeStarted());
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Server.RoundEnded -= OnRoundEnded;

        Exiled.Events.Handlers.Player.Verified -= OnVerified;

        Timing.KillCoroutines(_onModeStarted);
    }

    IEnumerator<float> OnModeStarted()
    {
        DAONTFT.Main.Init();

        QueryProcessor.DotCommandHandler.RegisterCommand(new DAONTFT.Core.Commands.ClientCommands.basicfeatures.SelectFirst());
        QueryProcessor.DotCommandHandler.RegisterCommand(new DAONTFT.Core.Commands.ClientCommands.basicfeatures.SelectSecond());
        QueryProcessor.DotCommandHandler.RegisterCommand(new DAONTFT.Core.Commands.ClientCommands.basicfeatures.SelectThird());
        QueryProcessor.DotCommandHandler.RegisterCommand(new DAONTFT.Core.Commands.ClientCommands.basicfeatures.RerollFirst());
        QueryProcessor.DotCommandHandler.RegisterCommand(new DAONTFT.Core.Commands.ClientCommands.basicfeatures.RerollSecond());
        QueryProcessor.DotCommandHandler.RegisterCommand(new DAONTFT.Core.Commands.ClientCommands.basicfeatures.RerollThird());

        foreach (var player in PlayerManager.List)
            DAONTFT.Core.EventArgs.PlayerEvents.Verified(player);

        Timing.CallDelayed(20f, () =>
        {
            DAONTFT.Core.TFT.ABattle.StartUpgrade();
        });

        while (true)
        {
            yield return Timing.WaitForSeconds(300f);

            DAONTFT.Core.TFT.ABattle.StartUpgrade();
        }
    }

    void OnVerified(VerifiedEventArgs ev)
    {
        DAONTFT.Core.EventArgs.PlayerEvents.Verified(ev.Player);
    }

    void OnRoundEnded(RoundEndedEventArgs ev)
    {
        IEnumerable<Player> players = PlayerManager.List.Where(x => x.IsAlive && !x.IsNPC);

        if (players.Count() == 1)
            Timing.RunCoroutine(Tools.SetWinner(players.ToList(), 5));

        else if (players.Count() > 1)
            Timing.RunCoroutine(Tools.SetWinner(players.ToList(), 1));
    }
}
