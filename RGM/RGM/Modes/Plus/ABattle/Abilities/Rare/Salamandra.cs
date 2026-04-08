using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Rare;

[Ability("살라만드라", "뜨거운 열정의 기운! 물, 흙, 바람의 정령을 모으면..?", AbilityCategory.Rare, AbilityType.RARE_SALAMANDRA)]
public class Salamandra : Ability
{
    public override void OnEnabled()
    {
        RoomType roomType = Owner.CurrentRoom.Type;
        Color roomColor = Owner.CurrentRoom.Color;

        Room room = Room.Get(roomType);

        room.Color = new Color(1, 0, 0);

        Timing.CallDelayed(10f, () =>
        {
            room.Color = roomColor;
        });
    }

    public override void OnDisabled()
    {
    }
}
