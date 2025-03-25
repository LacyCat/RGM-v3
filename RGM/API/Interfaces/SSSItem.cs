using CustomPlayerEffects;
using Exiled.API.Features.Core.UserSettings;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserSettings.ServerSpecific;

namespace RGM.API.Interfaces
{
    public class SSSItem
    {
        public List<ServerSpecificSettingBase> SSSBase { get; set; }
        public IEnumerable<SettingBase> SBase { get; set; }
    }
}
