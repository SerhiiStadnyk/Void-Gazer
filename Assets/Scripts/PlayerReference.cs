using Forms;
using UnityEngine;

public class PlayerReference
{
    private ActorFormInstance _actorFormInstance;

    public ActorFormInstance FormInstance => _actorFormInstance;


    public void SetActor(ActorFormInstance playerActor)
    {
        Debug.Assert(playerActor != null, "Player cannot be null!");
        _actorFormInstance = playerActor;
    }
}
