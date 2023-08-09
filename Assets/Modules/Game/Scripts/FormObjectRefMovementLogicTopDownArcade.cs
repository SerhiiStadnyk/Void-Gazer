using System;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.FormRefMovement
{
    public class FormObjectRefMovementLogicTopDownArcade: IFormObjectRefMovementLogic<Vector3>
    {
        private FormObjectRefMovementComponentBase<Vector3> _formObjectRefMovementComponentBase;
        private Transform _formRefTransform;


        void IFormObjectRefMovementLogic<Vector3>.Init(FormObjectRefMovementComponentBase<Vector3> formObjectRefMovementComponentBase)
        {
            _formObjectRefMovementComponentBase = formObjectRefMovementComponentBase;
            _formRefTransform = _formObjectRefMovementComponentBase.transform;
        }


        void IFormObjectRefMovementLogic<Vector3>.MoveTo(Vector3 target)
        {
            Vector3 foo = _formRefTransform.forward.normalized * target.z;
            _formRefTransform.position += foo * 10 * Time.deltaTime;
        }


        void IFormObjectRefMovementLogic<Vector3>.RotateTo(Vector3 target)
        {
            Vector3 foo = new Vector3(0, target.x, 0);
            _formRefTransform.Rotate(foo, 100 * Time.deltaTime);
        }


        void IFormObjectRefMovementLogic<Vector3>.Stop()
        {
        }


        void IFormObjectRefMovementLogic<Vector3>.Pause()
        {
        }


        void IFormObjectRefMovementLogic<Vector3>.Resume()
        {
        }


        void IDisposable.Dispose()
        {
        }
    }
}