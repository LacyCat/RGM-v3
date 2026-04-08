using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Rare;

[Ability("운디네", "청량한 바다의 기운! 불, 흙, 바람의 정령을 모으면..?", AbilityCategory.Rare, AbilityType.RARE_UNDINE)]
public class Undine : Ability
{
    public override void OnEnabled()
    {
        RoomType roomType = Owner.CurrentRoom.Type;
        Color roomColor = Owner.CurrentRoom.Color;

        Room room = Room.Get(roomType);

        room.Color = new Color(0, 0, 1);

        Timing.CallDelayed(10f, () =>
        {
            room.Color = roomColor;
        });
    }

    public override void OnDisabled()
    {
    }
}
