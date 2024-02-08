using Forms;
using UnityEngine;
using Zenject;

public class PlayerInitiator : MonoBehaviour
{
    [Inject]
    public void Inject(PlayerReference playerReference)
    {
        ActorFormInstance playerActor = GetComponent<ActorFormInstance>();
        playerReference.SetActor(playerActor);
    }
}
