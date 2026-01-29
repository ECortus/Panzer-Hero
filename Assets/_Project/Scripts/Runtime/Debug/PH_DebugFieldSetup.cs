namespace PanzerHero.Runtime.Debug
{
    public class PH_DebugFieldSetup : GameDevUtils.Runtime.UI.DebugFieldSetup
    {
        protected override void Setup()
        {
            RegisterEnableLogging();
        }
    }
}