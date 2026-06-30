using System.Collections.Generic;

namespace RGM.Modes.Abilities.Legend;

[Ability("존 윅", "권총류 데미지가 650% 증가합니다.", AbilityCategory.Legend, AbilityType.LEGEND_JOHNWICK)]
public class Wick : Ability
{
    public override void OnEnabled()
    {
        Exiled.Events.Handlers.Player.Hurting += OnHurting;
    }

    public override void OnDisabled()
    {
        Exiled.Events.Handlers.Player.Hurting -= OnHurting;
    }

    public void OnHurting(Exiled.Events.EventArgs.Player.HurtingEventArgs ev)
    {
        if (ev.Attacker != null)
        {
            List<ItemType> pistols = new List<ItemType>()
            {
                ItemType.GunCOM15,
                ItemType.GunCOM18,
                ItemType.GunCom45,
                ItemType.GunRevolver
            };

            if (pistols.Contains(ev.Attacker.CurrentItem.Type))
            {
                ev.DamageHandler.Damage *= 6.5f;
            }
        }
    }
}