using GameDevUtils.Runtime.UI;

namespace PanzerHero.UI.Debug
{
    public class PH_DebugFieldsSetup : DebugFieldsSetup
    {
        protected override void InitializeFields()
        {
            RegisterEnableLogging();
        }
    }
}