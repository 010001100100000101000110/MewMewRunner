using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "GameEvent", menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<IGameEventListener> eventListeners = new List<IGameEventListener>();

    public void RegisterListener(IGameEventListener listener)
    {
        if (!eventListeners.Contains(listener)) eventListeners.Add(listener);
    }

    public void UnregisterListener(IGameEventListener listener)
    {
        if (eventListeners.Contains(listener)) eventListeners.Remove(listener);
    }

    public void Raise()
    {
        for (int i = 0; i < eventListeners.Count; i++)
        {
            eventListeners[i].OnEventRaised();
        }
    }
}


