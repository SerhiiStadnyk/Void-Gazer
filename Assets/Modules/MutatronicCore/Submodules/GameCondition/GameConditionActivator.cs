using System;

namespace Modules.MutatronicCore.Submodules.GameCondition
{
    public class GameConditionActivator: IDisposable
    {
        private GameCondition _condition;

        public GameConditionActivator(GameCondition condition)
        {
            _condition = condition;
        }


        void IDisposable.Dispose()
        {
            _condition.DeactivateCondition(this);
        }
    }
}
