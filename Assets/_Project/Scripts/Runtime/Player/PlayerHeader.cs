using System;
using PanzerHero.Runtime.LevelDesign.Levels;
using PanzerHero.Runtime.Abstract;
using PanzerHero.Runtime.Systems;
using PanzerHero.Runtime.Units.Player.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PanzerHero.Runtime.Units.Player
{
    [RequireComponent(typeof(PlayerEngine))]
    public class PlayerHeader : BaseHeader<PlayerRig>
    {
        [SerializeField] private PlayerData playerData;
        
        public PlayerData GetData() => playerData;
    }
}