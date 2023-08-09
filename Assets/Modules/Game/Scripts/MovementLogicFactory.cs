using Modules.MutatronicCore.Scripts.Runtime;
using Modules.MutatronicCore.Scripts.Runtime.FormRefMovement;
using UnityEngine;

namespace Modules.Game.Scripts
{
    [CreateAssetMenu(fileName = "FactoryPointer.asset", menuName = "Mutatronic/Runtime/Factories/Create Movement Logic Factory")]
    public class MovementLogicFactory : FactoryBase<IFormObjectRefMovementLogic<Vector3>>
    {
        [SerializeField]
        private FactoryPointer _movementLogicTopDownArcadePointer;


        public override IFormObjectRefMovementLogic<Vector3> GetObject(FactoryPointer pointer)
        {
            IFormObjectRefMovementLogic<Vector3> result = null;

            if (pointer == _movementLogicTopDownArcadePointer)
            {
                result = new FormObjectRefMovementLogicTopDownArcade();
            }

            return result;
        }
    }
}
