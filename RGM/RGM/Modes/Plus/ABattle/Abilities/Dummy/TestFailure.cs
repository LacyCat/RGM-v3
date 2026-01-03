using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Dummy;

[Ability("시험 실패", "안타깝네요, 낙제했습니다.", AbilityCategory.Dummy, AbilityType.DUMMY_TESTFAILURE)]
public class TestFailure : Ability
{
    public override void OnEnabled()
    {
    }

    public override void OnDisabled()
    {
    }
}