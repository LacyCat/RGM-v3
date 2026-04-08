using System.Collections.Generic;
using MEC;

namespace RGM.Modes.Abilities.Unique.Scp939;

[Ability("흉내쟁이", "흉내 쿨타임이 사라집니다.", AbilityCategory.Scp939, AbilityType.SCP939_MINIC)]
public class Minic : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp939.PlayingSound += OnPlayingSound;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp939.PlayingSound -= OnPlayingSound;
    }

    public IEnumerator<float> OnPlayingSound(Exiled.Events.EventArgs.Scp939.PlayingSoundEventArgs ev)
    {
        if (Owner != ev.Player)
            yield break;

        yield return Timing.WaitForOneFrame;

        ev.Scp939.MimicryCooldown = 0;
    }
}
