using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;
using System;
using System.Collections.Generic;

namespace RGM.API.Interfaces
{
    public class SettingInfo
    {
        public List<SettingBase> SettingBases { get; set; }
        public HeaderSetting Header { get; set; }
        public Action<Player> Activate { get; set; }
    }
}
