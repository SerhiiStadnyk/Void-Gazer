using System.Collections.Generic;
using Modules.MutatronicCore.Submodules.GameCondition;
using UnityEngine;

namespace Modules.MutatronicCore.Scripts.Runtime.FormRefMovement
{
    public class FormObjectRefMovementComponentBase<T> : MutatronicBehaviour
    {
        [SerializeField]
        private List<GameCondition> _moveBlockerGameCondition;

        [SerializeField]
        private FactoryBase<IFormObjectRefMovementLogic<T>> _movementLogicFactory;

        [SerializeField]
        private FactoryPointer _factoryPointer;

        protected IFormObjectRefMovementLogic<T> movementLogic;

        public IFormObjectRefMovementLogic<T> MovementLogic => movementLogic;

        //TODO: Add movement modifiers


        protected void Awake()
        {
            SetMovementLogic(_factoryPointer);
            movementLogic.Init(this);
        }


        private void OnEnable()
        {
            movementLogic.Resume();
        }


        private void OnDisable()
        {
            movementLogic.Pause();
        }


        private void OnDestroy()
        {
            movementLogic.Stop();
            movementLogic.Dispose();
        }


        public void RotateTo(T target)
        {
            movementLogic.RotateTo(target);
        }


        public void MoveTo(T target)
        {
            movementLogic.MoveTo(target);
        }


        public void Stop()
        {
            movementLogic.Stop();
        }


        public void Pause()
        {
            movementLogic.Pause();
        }


        public void SetMovementLogic(FactoryPointer pointer)
        {
            if (movementLogic != null)
            {
                movementLogic.Stop();
                movementLogic.Dispose();
            }

            movementLogic = _movementLogicFactory.GetObject(pointer);
        }
    }
}