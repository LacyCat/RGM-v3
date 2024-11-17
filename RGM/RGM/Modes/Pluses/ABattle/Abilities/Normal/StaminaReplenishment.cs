using System.Collections.Generic;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables.Scp330;
using UnityEngine;

namespace RGM.Modes.Abilities.Normal;

[Ability("체력 보충", "파란 사탕을 받습니다.", AbilityCategory.Common, AbilityType.NORMAL_STAMINAREPLENISHMENT)]
public class StaminaReplenishment : Ability
{
    public override void OnEnabled()
    {
        Owner.TryAddCandy(CandyKindID.Blue);

        if (Owner.IsScp)
            Server.ExecuteCommand($"/forceeq {Owner.Id} 42");
    }

    public override void OnDisabled()
    {
    }
}
