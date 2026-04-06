using static DAONTFT.Core.Variables.Base;
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
