using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using MEC;
using RGM.API.Features;
using UnityEngine;

namespace RGM.Modes.Abilities.Mythic;

[Ability("워 머신", "발사할 때마다 랜덤한 투척물을 투하하는, 탄약이 무제한인 리볼버를 얻습니다.", AbilityCategory.Mythic, AbilityType.MYTHIC_BOMBGUN)]
public class BombGun : Ability
{
    ushort itemSerial = 0;

    public override void OnEnabled()
    {
        Item item = Owner.AddItem(ItemType.GunRevolver);

        itemSerial = item.Serial;

        Exiled.Events.Handlers.Player.ChangedItem += OnChangedItem;
        Exiled.Events.Handlers.Player.Shot += OnShot;
    }

    public override void OnDisabled()
    {
    }

    public void OnChangedItem(ChangedItemEventArgs ev)
    {
        if (itemSerial == ev.Player.CurrentItem.Serial && ev.Item != null)
        {
            if (itemSerial == ev.Item.Serial)
                ev.Player.ShowHint($"<b><color={ABattle.RatingColor["신화"]}>워 머신</color></b> 능력이 있는 **리볼버**입니다!");
        }
    }

    public void OnShot(ShotEventArgs ev)
    {
        if (ev.Item.Serial == itemSerial)
        {
            ev.Player.ThrowGrenade(Tools.EnumToList<ProjectileType>().GetRandomValue());

            ev.Player.CurrentItem.As<Firearm>().MagazineAmmo = 1;
        }
    }
}