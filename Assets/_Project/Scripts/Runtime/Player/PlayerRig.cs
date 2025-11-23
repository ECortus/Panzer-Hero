namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerRig : UnitRig
    {
        protected override void InitializeAdditionalComponents()
        {
            InitializeComponent<PlayerEngine>();
        }
    }
}