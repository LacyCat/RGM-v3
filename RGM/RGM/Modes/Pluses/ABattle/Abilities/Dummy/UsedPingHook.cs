using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Dummy;

[Ability("핑 갈?고리", "핑 갈고리 능력을 사용했습니다.", AbilityCategory.Dummy, AbilityType.DUMMY_USEDPINGHOOK)]
public class UsedPingHook : Ability
{
    public override void OnEnabled()
    {
    }

    public override void OnDisabled()
    {
    }
}