using Core.Runtime.Forms;
using UnityEngine;
using Zenject;

namespace Core.Runtime
{
    public class PlayerInitiator : MonoBehaviour
    {
        [Inject]
        public void Inject(PlayerReference playerReference)
        {
            ActorFormInstance playerActor = GetComponent<ActorFormInstance>();
            playerReference.SetActor(playerActor);
        }
    }
}
