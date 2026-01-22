using PanzerHero.Runtime.Units.Abstract.Base;
using PanzerHero.Runtime.Units.Player.Data;
using PanzerHero.Runtime.Units.Simultaneous;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerHeader : BaseHeader<PlayerRig>
    {
        [SerializeField] private PlayerData playerData;
        
        public PlayerData GetData() => playerData;
    }
}