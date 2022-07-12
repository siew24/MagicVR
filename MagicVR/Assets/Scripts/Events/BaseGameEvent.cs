using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/Game Event", order = 0)]
public class BaseGameEvent : ScriptableObject
{
    public List<BaseGameEventListener> listeners = new();

    public void RegisterListener(BaseGameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(BaseGameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void Raise()
    {
        for (int i = 1; i <= listeners.Count; i++)
        {
            // the "^i"th index means the (i-1)th index from the end of the array
            listeners[^i].onEventRaised();
        }
    }
}