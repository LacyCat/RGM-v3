using System;

namespace RGM.API.Interfaces
{
    public class PlayerReport
    {
        public int Kill { get; set; }
        public int Death { get; set; }
        public int Revive { get; set; }
        public int KillScp { get; set; }
        public int KillHuman { get; set; }
        public int Damage { get; set; }
        public DateTime LastDeath { get; set; }
    }
}
