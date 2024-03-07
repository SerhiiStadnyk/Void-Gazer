using System.Collections.Generic;
using System.Collections.ObjectModel;

public class SceneLifetimeHandlersContainer
{
    private List<SceneLifetimeHandler> _lifetimeHandlers = new List<SceneLifetimeHandler>();

    public ReadOnlyCollection<SceneLifetimeHandler> LifetimeHandlers => _lifetimeHandlers.AsReadOnly();


    public void RegisterLifetimeHandler(SceneLifetimeHandler lifetimeHandler)
    {
        if (!_lifetimeHandlers.Contains(lifetimeHandler))
        {
            _lifetimeHandlers.Add(lifetimeHandler);
        }
    }


    public void UnregisterLifetimeHandler(SceneLifetimeHandler lifetimeHandler)
    {
        if (_lifetimeHandlers.Contains(lifetimeHandler))
        {
            _lifetimeHandlers.Remove(lifetimeHandler);
        }
    }
}
