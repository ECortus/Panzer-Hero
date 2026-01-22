using System.Collections.Generic;
using GameDevUtils.Runtime;
using PanzerHero.Runtime.LevelDesign;
using PanzerHero.Runtime.Units.Components;

namespace PanzerHero.Runtime.Units.Systems
{
    public class UnitTargetCalculatorSystem : AbstractComponentSystem<UnitTargetCalculator>
    {
        GameStatement statement;
        UnitTargetCalculatorManager manager;
        
        protected override void OnStart()
        {
            manager = UnitTargetCalculatorManager.GetInstance;
            statement = GameStatement.GetInstance;
            
            base.OnStart();
        }
        
        protected override List<UnitTargetCalculator> GetUnitList()
        {
            return manager.AllComponents;
        }

        protected override bool UpdateFunction(UnitTargetCalculator unit, float delta)
        {
            if (statement.IsOnStartedState())
            {
                unit.UpdateTarget();
            }
            else
            {
                unit.ResetTarget();
            }
            
            return true;
        }
    }
}