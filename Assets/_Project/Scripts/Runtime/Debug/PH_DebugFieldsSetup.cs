using GameDevUtils.Runtime.UI;

namespace PanzerHero.Runtime.Debug
{
    public class PH_DebugFieldsSetup : DebugFieldsSetup
    {
        protected override void InitializeFields()
        {
            RegisterEnableLogging();
        }
    }
}