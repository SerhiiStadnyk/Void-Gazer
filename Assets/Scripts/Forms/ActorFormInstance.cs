using UnityEngine;

namespace Forms
{
    public class ActorFormInstance : BaseFormInstance<ActorForm>
    {
        private Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;


        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}
