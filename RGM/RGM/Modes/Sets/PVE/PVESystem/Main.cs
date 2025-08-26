using Exiled;
using Exiled.API.Features;
using Exiled.Events;
using System;

using ServerEvents = Exiled.Events.Handlers.Server;

namespace RGM.Modes.Sets.PVE;

public static class Main
{
    public static void OnEnabled()
    {
        RoundHandler.OnRoundStarted();
        ServerEvents.EndingRound += RoundHandler.OnEndingRound;
    }
}