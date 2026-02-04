using System;
using System.Collections.Generic;
using GameDevUtils.Runtime;

namespace PanzerHero.Runtime.SavePrefs
{
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

        public bool IsBuyed;
        
        public int MaxHealthProgressLevel;
        public int MaxArmorProgressLevel;
        public int DamageProgressLevel;
        public int ReloadDurationProgressLevel;

        public int MaxHealthGeneralLevel;
        public int MaxArmorGeneralLevel;
        public int DamageGeneralLevel;
        public int ReloadDurationGeneralLevel;
    }
}