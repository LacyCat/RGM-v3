using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace RGM.Modes.Abilities.Rare;

[Ability("노움", "웅장한 대지의 기운! 불, 물, 바람의 정령을 모으면..?", AbilityCategory.Rare, AbilityType.RARE_GNOME)]
public class Gnome : Ability
{
    public override void OnEnabled()
    {
        RoomType roomType = Owner.CurrentRoom.Type;
        Color roomColor = Owner.CurrentRoom.Color;

        Room room = Room.Get(roomType);

        room.Color = new Color(0.5f, 0.25f, 0);

        Timing.CallDelayed(10f, () =>
        {
            room.Color = roomColor;
        });
    }

    public override void OnDisabled()
    {
    }
}
