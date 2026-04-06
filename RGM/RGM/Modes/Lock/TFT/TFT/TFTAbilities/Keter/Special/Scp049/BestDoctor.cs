using Exiled.Events.EventArgs.Scp049;
using MEC;

namespace DAONTFT.Core.TFT.Keter.Scp049;

[TFTAbility("명의", "시체를 되살리기 시작한 후, 3초 후에도 시체가 만료되지 않았다면 즉시 되살립니다.", TFTAbilityLevel.Keter, TFTAbilityCategory.Scp049, TFTAbilityPoint.Continuous, TFTAbilityType.BestDoctor, "💉")]
public class BestDoctor : TFTAbility
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Scp049.StartingRecall += OnStartingRecall;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Scp049.StartingRecall -= OnStartingRecall;
    }

    void OnStartingRecall(StartingRecallEventArgs ev)
    {
        Timing.CallDelayed(3, () =>
        {
            if (ev.Scp049.CanResurrect(ev.Ragdoll) && ev.Scp049.IsInRecallRange(ev.Ragdoll))
            {
                ev.Scp049.Resurrect(ev.Player);
            }
        });
    }
}
