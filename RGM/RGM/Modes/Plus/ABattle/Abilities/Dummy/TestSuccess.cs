using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Dummy;

[Ability("시험 성공", "축하합니다, 성공했군요!", AbilityCategory.Dummy, AbilityType.DUMMY_TESTSUCCESS)]
public class TestSuccess : Ability
{
    public override void OnEnabled()
    {
    }

    public override void OnDisabled()
    {
    }
}