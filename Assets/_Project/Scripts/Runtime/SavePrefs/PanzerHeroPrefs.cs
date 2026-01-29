using GameSaveKit.Runtime.Prefs;

namespace PanzerHero.Runtime.SavePrefs
{
    public class PanzerHeroPrefs : GamePrefs
    {
        public int LevelID;
        
        public int Coins;
        public int Diamonds;

        public int MaxHealthGeneralLevel;
        public int MaxArmorGeneralLevel;
        public int DamageGeneralLevel;
        public int ReloadDurationGeneralLevel;
    }
}