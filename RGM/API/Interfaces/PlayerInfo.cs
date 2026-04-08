using CustomPlayerEffects;
using Exiled.API.Features.Items;
using PlayerRoles;
using System.Collections.Generic;
using UnityEngine;

namespace RGM.API.Interfaces
{
    public class PlayerInfo
    {
        public RoleTypeId RoleType { get; set; }
        public float MaxHealth { get; set; }
        public float Health { get; set; }
        public IEnumerable<StatusEffectBase> ActiveEffects { get; set; }
        public IReadOnlyCollection<Item> Items { get; set; }
        public Item CurrentItem { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
    }
}
