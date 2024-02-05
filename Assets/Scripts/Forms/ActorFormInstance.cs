using UnityEngine;

namespace Forms
{
    public class ActorFormInstance : BaseFormInstance<ActorForm>
    {
        private CharacterController _charController;

        public CharacterController CharacterController => _charController;


        protected void Start()
        {
            _charController = GetComponent<CharacterController>();
        }
    }
}
