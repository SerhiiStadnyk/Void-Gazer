using Forms;
using UnityEngine;

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
