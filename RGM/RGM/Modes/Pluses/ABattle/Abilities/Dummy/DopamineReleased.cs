using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Dummy;

[Ability("방출된 도파민", "도파민이 방출되었습니다.", AbilityCategory.Dummy, AbilityType.DUMMY_DOPAMINERELEASED)]
public class DopamineReleased : Ability
{
    public override void OnEnabled()
    {
    }

    public override void OnDisabled()
    {
    }
}