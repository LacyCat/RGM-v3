using MEC;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Exiled.API.Enums;

namespace RGM
{
    public enum FixedLeadingTeamTag
    {
        Undefined,
        Normal,
        Forced
    }

    public class EssentialExtension : MonoBehaviour
    {
        Player ply;

        private List<string> messages = new List<string>();
        private List<float> _visualtimes = new List<float>();

        private DateTime timer = DateTime.Now;
        public byte Speed = 0;
        public LeadingTeam FixedLeadingTeam = LeadingTeam.Draw;
        public FixedLeadingTeamTag leadingTeamTag = FixedLeadingTeamTag.Normal;

        void Awake()
        {
            this.ply = Player.Get(gameObject);
            timer = DateTime.Now;
        }

        void Update()
        {
            if ((DateTime.Now - timer).Seconds >= 1)
            {
                this.timer = DateTime.Now;
                try
                {
                    this._refresh();
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
        }
        public void AddMessage(string content, float time)
        {
            string txt = "<cspace=0.1em>" + content + "</cspace>";
            float dur = time;
            this.messages.Add(txt);
            this._visualtimes.Add(dur);
            this._refreshmsg();
            Timing.CallDelayed(time, () =>
            {
                if (messages.Contains(txt))
                {
                    this.messages.Remove(txt);
                }
                if (this._visualtimes.Contains(dur))
                {
                    this._visualtimes.Remove(dur);
                }
                this._refreshmsg();
            });
        }
        public void ClearMessage()
        {
            messages.Clear();
            _visualtimes.Clear();
            this.ply.ClearBroadcasts();
        }
        private void _refreshmsg()
        {
            if (this.messages.Count() == 0)
            {
                this.ply.ClearBroadcasts();
                return;
            }

            float a = 100;
            foreach (var b in this._visualtimes)
            {
                if (b < a)
                {
                    a = b;
                }
            }

            this.messages.Reverse();

            this.ply.ClearBroadcasts();
            this.ply.Broadcast((ushort)a, string.Join($"<size=22>\n</size>", this.messages) + $"<size=22>\n</size>", Broadcast.BroadcastFlags.Truncated, false);
            this.messages.Reverse();
        }

        private void _refresh()
        {
            if (Speed > this.ply.GetEffect(EffectType.MovementBoost).Intensity)
            {
                this.ply.GetEffect(EffectType.MovementBoost).Intensity = Math.Min(Math.Max(Speed, (byte)0), (byte)255);
            }
            if (!this.ply.IsAlive)
            {
                Speed = 0;
                FixedLeadingTeam = LeadingTeam.Draw;
                leadingTeamTag = FixedLeadingTeamTag.Undefined;
                return;
            }
            if (leadingTeamTag == FixedLeadingTeamTag.Undefined)
            {
                FixedLeadingTeam = (LeadingTeam)this.ply.Role.Team;
                leadingTeamTag = FixedLeadingTeamTag.Normal;
            }
        }
    }
}
