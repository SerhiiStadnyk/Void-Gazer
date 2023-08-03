using Modules.MutatronicCore.Scripts.Runtime;
using Modules.MutatronicCore.Scripts.Runtime.EntityMovement;
using UnityEngine;

namespace Modules.Game.Scripts
{
    [CreateAssetMenu(fileName = "FactoryPointer.asset", menuName = "Mutatronic/Runtime/Factories/Create Movement Logic Factory")]
    public class MovementLogicFactory : FactoryBase<IMovementLogic>
    {
        [SerializeField]
        private FactoryPointer _movementLogicTopDownArcadePointer;


        public override IMovementLogic GetObject(FactoryPointer pointer)
        {
            IMovementLogic result = null;

            if (pointer == _movementLogicTopDownArcadePointer)
            {
                result = new EntityMovementLogicTopDownArcade();
            }

            return result;
        }
    }
}
