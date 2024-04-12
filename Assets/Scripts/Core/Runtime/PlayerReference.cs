using Core.Runtime.Forms;
using UnityEngine;

namespace Core.Runtime
{
    public class PlayerReference
    {
        private ActorFormInstance _actorPlayer;

        public ActorFormInstance Player => _actorPlayer;


        public void SetActor(ActorFormInstance playerActor)
        {
            Debug.Assert(playerActor != null, "Player cannot be null!");
            _actorPlayer = playerActor;
        }
    }
}
