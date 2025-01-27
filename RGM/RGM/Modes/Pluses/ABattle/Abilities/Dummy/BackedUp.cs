using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Dummy;

[Ability("백업됨", "백업이 완료되었습니다. v2 가동..", AbilityCategory.Dummy, AbilityType.DUMMY_BACKEDUP)]
public class BackedUp : Ability
{
    public override void OnEnabled()
    {
    }

    public override void OnDisabled()
    {
    }
}