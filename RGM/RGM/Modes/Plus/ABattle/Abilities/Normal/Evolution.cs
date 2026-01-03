using System.Collections.Generic;
using Exiled.API.Enums;
using UnityEngine;

namespace RGM.Modes.Abilities.Normal;

[Ability("진화", "몸의 크기가 9% 작아집니다.", AbilityCategory.Common, AbilityType.NORMAL_EVOLUTION)]
public class Evolution : Ability
{
    public override void OnEnabled()
    {
        Owner.Scale = new Vector3(Owner.Scale.x - 0.09f, Owner.Scale.y - 0.09f, Owner.Scale.z - 0.09f);
    }

    public override void OnDisabled()
    {
        Owner.Scale = new Vector3(Owner.Scale.x + 0.09f, Owner.Scale.y + 0.09f, Owner.Scale.z + 0.09f);
    }
}