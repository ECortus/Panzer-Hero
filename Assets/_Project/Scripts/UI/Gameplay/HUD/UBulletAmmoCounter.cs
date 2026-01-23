using GameDevUtils.Runtime.UI.Abstract;

namespace PanzerHero.UI.Gameplay.HUD
{
    public class UBulletAmmoCounter : UDynamicTextField
    {
        protected override void OnStart()
        {
            base.OnStart();
        }
        
        protected override string GetText()
        {
            return $"0/0";
        }
    }
}