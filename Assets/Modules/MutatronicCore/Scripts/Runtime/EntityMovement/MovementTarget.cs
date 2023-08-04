using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.EntityMovement
{
    public struct MovementTarget
    {
        public Vector3 TargetDirection { get; set; }

        public Transform TargetTransform { get; set; }


        public MovementTarget(Vector3 targetDirection, Transform targetTransform)
        {
            TargetDirection = targetDirection;
            TargetTransform = targetTransform;
        }
    }
}
