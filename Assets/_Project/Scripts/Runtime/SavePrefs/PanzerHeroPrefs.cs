using GameSaveKit.Runtime.Prefs;

namespace PanzerHero.Runtime.SavePrefs
{
    public class PanzerHeroPrefs : GamePrefs
    {
        public int LevelID;
        
        public int Coins;
        public int Diamonds;

        public int MaxHealthProgressLevel;
        public int MaxArmorProgressLevel;
        public int DamageProgressLevel;
        public int ReloadDurationProgressLevel;
    }
}