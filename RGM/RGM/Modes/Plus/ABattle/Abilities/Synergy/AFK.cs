namespace RGM.Modes.Abilities.Synergy;

[RequiresAbility(AbilityType.RARE_DND, AbilityType.RARE_DND, AbilityType.RARE_DND)]
[Ability("AFK", "<자리 비움 x3> 게임 하고 싶은 거 맞죠..? 용기가 가상하시니 선물을 드릴게요.", AbilityCategory.Synergy, AbilityType.SYNERGY_AFK)]
public class AFK : Ability
{
    public override void OnEnabled()
    {
        Owner.AddAhp(1205, 1205, 0);
    }

    public override void OnDisabled()
    {
    }
}
