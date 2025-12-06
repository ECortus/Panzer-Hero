namespace PanzerHero.Runtime.Units.Player
{
    public class PlayerRig : BaseRig
    {
        public PlayerMovement Movement { get; private set; }
        
        protected override void InitializeComponents()
        {
            InitializeComponent<PlayerBezierSpline, PlayerRig>();
            Movement = InitializeComponent<PlayerMovement, PlayerRig>();
        }
    }
}