using PanzerHero.Runtime.Abstract;
using PanzerHero.Runtime.Player.Components;
using PanzerHero.Runtime.Units.Player;

namespace PanzerHero.Runtime.Player
{
    public class PlayerRig : BaseRig
    {
        public PlayerMovement Movement { get; private set; }
        
        protected override void InitializeComponents()
        {
            InitializeComponent<PlayerHealth, PlayerRig>();
            
            InitializeComponent<PlayerBezierSpline, PlayerRig>();
            Movement = InitializeComponent<PlayerMovement, PlayerRig>();
            
            InitializeComponent<PlayerAttacker, PlayerRig>();
        }
    }
}