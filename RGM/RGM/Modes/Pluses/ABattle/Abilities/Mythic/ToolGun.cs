using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using ProjectMER.Features.ToolGun;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Mythic;

[Ability("툴건", "그저 권총을 얻습니다. (좌클릭 - 생성, 우클릭(홀드) - 선택, F(토글) - 삭제)", AbilityCategory.Mythic, AbilityType.MYTHIC_TOOLGUN)]
public class ToolGun : Ability
{
    public override void OnEnabled()
    {
        if (ToolGunItem.TryAdd(Owner))
        {
        }
    }

    public override void OnDisabled()
    {
    }
}