using System.Collections.Generic;
using GameDevUtils.Runtime;
using SaveableExtension.Runtime.Prefs;

namespace PanzerHero.Runtime.SavePrefs
{
    public class PanzerHeroPrefs : GamePrefs
    {
        public int PlayerID;
        
        public int LevelID;
        
        public int Coins;
        public int Diamonds;
        
        public SkinPrefsCollection SkinPrefs;

        #region Methods

        public void SetPlayerID(int id)
        {
            PlayerID = id;
        }

        #region Skin Prefs Methods

        public SkinPref GetCurrentSkinPref() => GetSkinPref(PlayerID);
        public void SetCurrentSkinPref(SkinPref pref) => SetSkinPref(PlayerID, pref);

        public SkinPref GetSkinPref(int id)
        {
            if (!SkinPrefs.ContainsKey(id))
            {
                if (!SkinPrefs.TryAdd(new SkinPref(id)))
                {
                    DebugHelper.LogError("Error on adding on GetSkin");
                    return null;
                }
            }

            if (SkinPrefs.TryGetValue(id, out SkinPref pref))
            {
                return pref;
            }

            throw new KeyNotFoundException();
        }

        public void SetSkinPref(int id, SkinPref pref)
        {
            if (!SkinPrefs.ContainsKey(id))
            {
                if (!SkinPrefs.TryAdd(pref))
                {
                    DebugHelper.LogError("Error on adding on GetSkin");
                    return;
                }
            }

            SkinPrefs.SetValue(id, pref);
        }

        #endregion

        #endregion
    }
}