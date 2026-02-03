using System;
using System.Linq;
using UnityEngine;

namespace PanzerHero.Runtime.Units.Player.Skins
{
    [CreateAssetMenu(fileName = "PlayerSkinCollection", menuName = "Panzer Hero/Collections/PlayerSkinCollection")]
    public class PlayerSkinCollection : ScriptableObject
    {
        [Serializable]
        public class Skin
        {
            public int Id;
            public PlayerHeader Player;
        }

        [SerializeField] private Skin[] skins;

        public Skin[] GetSkins() => skins;

        public Skin GetSkin(int id)
        {
            for (int i = 0; i < skins.Length; i++)
            {
                var skin = skins[i];
                if (skin.Id == id)
                {
                    return skin;
                }
            }

            throw new NotImplementedException();
        }
    }
}