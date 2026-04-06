using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using RGM.API.Features;

namespace RGM.Modes.Abilities.Unique.Tutorial;

[Ability("제 3세력", "뱀의 손 지원을 2명 더 부릅니다.", AbilityCategory.Tutorial, AbilityType.TUTORIAL_THIRDFORCE)]
public class ThirdForce : Ability
{
    public override void OnEnabled()
    {
        List<Player> DeadPlayers = PlayerManager.List.Where(x => x.IsDead).ToList();
        DeadPlayers.ShuffleList();

        Tools.CallSnakeHand(Owner, DeadPlayers.Take(2).ToList());
    }

    public override void OnDisabled()
    {
    }
}
