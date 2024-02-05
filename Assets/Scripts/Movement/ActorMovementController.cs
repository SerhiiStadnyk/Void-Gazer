using Forms;
using UnityEngine;

namespace Movement
{
    public class ActorMovementController : MonoBehaviour
    {
        private ActorFormInstance _actor;

        protected void Start()
        {
            _actor = GetComponent<ActorFormInstance>();
        }


        public void MoveForward()
        {
            Transform actorTransform = _actor.transform;
            Vector3 targetPos = actorTransform.position + actorTransform.forward * (Time.deltaTime * 10);
            _actor.Rigidbody.MovePosition(targetPos);
        }


        private void Update()
        {
            MoveForward();
        }
    }
}
