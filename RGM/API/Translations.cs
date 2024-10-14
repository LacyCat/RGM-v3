using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGM
{
    public class Translations
    {
        public static Dictionary<RoleTypeId, string> RoleTranslation = new Dictionary<RoleTypeId, string>
        {
            {RoleTypeId.Scp049, "SCP-049"},
            {RoleTypeId.Scp0492, "SCP-049-2"},
            {RoleTypeId.Scp079, "SCP-079"},
            {RoleTypeId.Scp096, "SCP-096"},
            {RoleTypeId.Scp106, "SCP-106"},
            {RoleTypeId.Scp173,  "SCP-173"},
            {RoleTypeId.Scp939, "SCP-939"},
            {RoleTypeId.Scp3114, "SCP-3114"},
            {RoleTypeId.ClassD, "D계급"},
            {RoleTypeId.Scientist, "과학자"},
            {RoleTypeId.FacilityGuard, "시설 가드"},
            {RoleTypeId.ChaosConscript, "혼돈의 반란 병사"},
            {RoleTypeId.ChaosMarauder, "혼돈의 반란 약탈자"},
            {RoleTypeId.ChaosRepressor, "혼돈의 반란 압제자"},
            {RoleTypeId.ChaosRifleman, "혼돈의 반란 소총수"},
            {RoleTypeId.NtfCaptain, "구미호 대위"},
            {RoleTypeId.NtfPrivate, "구미호 이등병"},
            {RoleTypeId.NtfSergeant, "구미호 병장"},
            {RoleTypeId.NtfSpecialist, "구미호 전문가"},
            {RoleTypeId.Spectator, "관전자"},
            {RoleTypeId.None, "알 수 없음"},
            {RoleTypeId.Tutorial, "튜토리얼"},
            {RoleTypeId.Overwatch, "오버워치"},
            {RoleTypeId.Filmmaker, "필름메이커"},
        };
    }
}
