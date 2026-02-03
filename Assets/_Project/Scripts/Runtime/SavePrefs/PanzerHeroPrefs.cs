using System;
using System.Collections.Generic;
using System.Linq;
using GameDevUtils.Runtime;
using GameSaveKit.Runtime.Prefs;

namespace PanzerHero.Runtime.SavePrefs
{
    public class PanzerHeroPrefs : GamePrefs
    {
        public int PlayerID;
        
        public int LevelID;
        
        public int Coins;
        public int Diamonds;

        #region Skins Prefs

        [Serializable]
        public class SkinPrefsCollection
        {
            public List<SkinPref> Prefs = new List<SkinPref>();

            public bool ContainsKey(int id)
            {
                for (int i = 0; i < Prefs.Count; i++)
                {
                    var pref = Prefs[i];
                    if (pref.Id == id)
                    {
                        return true;
                    }
                }

                return false;
            }

            public bool TryAdd(SkinPref pref)
            {
                if (ContainsKey(pref.Id))
                {
                    return false;
                }

                Prefs.Add(pref);
                return true;
            }

            public void SetValue(int id, SkinPref pref)
            {
                for (int i = 0; i < Prefs.Count; i++)
                {
                    var skin = Prefs[i];
                    if (skin.Id == id)
                    {
                        Prefs[i] = pref;
                        break;
                    }
                }
            }

            public bool TryGetValue(int id, out SkinPref pref)
            {
                SkinPref _pref = null;
                for (int i = 0; i < Prefs.Count; i++)
                {
                    var skin = Prefs[i];
                    if (skin.Id == id)
                    {
                        _pref = skin;
                        break;
                    }
                }

                pref = _pref;
                return _pref != null;
            }
        }
        
        [Serializable]
        public class SkinPref
        {
            public SkinPref(int id)
            {
                Id = id;
            }
            
            public int Id;
            
            public int MaxHealthGeneralLevel;
            public int MaxArmorGeneralLevel;
            public int DamageGeneralLevel;
            public int ReloadDurationGeneralLevel;
        }

        public SkinPrefsCollection SkinPrefs;

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
    }
}