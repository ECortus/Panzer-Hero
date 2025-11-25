namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerRig : BaseRig
    {
        protected override void InitializeAdditionalComponents()
        {
            InitializeComponent<PlayerBezierSpline>();
            InitializeComponent<PlayerEngine>();
            
            InitializeComponent<PlayerMovement>();
        }
    }
}