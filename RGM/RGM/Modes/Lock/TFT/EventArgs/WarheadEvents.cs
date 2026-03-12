using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static DAONTFT.Core.Variables.Base;
using System.Diagnostics.Eventing.Reader;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Warhead;

namespace DAONTFT.Core.EventArgs
{
    public static class WarheadEvents
    {
        public static void OnStopping(StoppingEventArgs ev)
        {
            if (AutoNuke)
            {
                ev.IsAllowed = false;
            }
        }
    }
}
