using Forms;
using UnityEngine;

namespace Movement
{
    public class ActorMovementController : MonoBehaviour
    {
        [SerializeField]
        private float _forwardSpeed = 2f;

        [SerializeField]
        private float _backwardSpeed = 1f;

        [SerializeField]
        private float _rotationSpeed = 0.25f;

        private ActorFormInstance _actor;
        private Transform _actorTransform;

        protected void Start()
        {
            _actor = GetComponent<ActorFormInstance>();
            _actorTransform = _actor.transform;
        }


        public void MoveForward()
        {
            Vector3 targetPos = _actorTransform.forward * ((Time.deltaTime) * _forwardSpeed);
            _actor.CharacterController.Move(targetPos);
        }


        public void MoveBackward()
        {
            Vector3 targetPos = -_actorTransform.forward * ((Time.deltaTime) * _backwardSpeed);
            _actor.CharacterController.Move(targetPos);
        }


        public void Rotate(float inputValue)
        {
            _actorTransform.Rotate(0, inputValue * _rotationSpeed, 0);
        }
    }
}
