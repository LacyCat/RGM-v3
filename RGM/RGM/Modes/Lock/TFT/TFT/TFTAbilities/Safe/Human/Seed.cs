using System.Collections.Generic;
using Exiled.API.Extensions;
using Exiled.API.Features;
using PlayerRoles;

namespace DAONTFT.Core.TFT.Safe.Human;

[TFTAbility("꿈나무", "자신의 진영의 지원 영향력을 2 추가합니다. (죄수나 과학자의 경우 +1)", TFTAbilityLevel.Safe, TFTAbilityCategory.Human, TFTAbilityPoint.Once, TFTAbilityType.Seed1, "🌳")]
public class Seed1 : TFTAbility
{
    public override void OnEnabled()
    {
        if (new List<RoleTypeId>
        {
            RoleTypeId.ClassD,
            RoleTypeId.Scientist
        }.Contains(Owner.Role.Type))
        {
            Respawn.GrantInfluence(Owner.Role.Team.GetFaction(), 1);
        }

        Respawn.GrantInfluence(Owner.Role.Team.GetFaction(), 2);
    }

    public override void OnDisabled()
    {
    }
}
