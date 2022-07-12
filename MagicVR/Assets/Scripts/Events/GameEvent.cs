using System.Collections.Generic;
using UnityEngine;

public class GameEvent<T> : ScriptableObject
{
    public List<GameEventListener<T>> listeners = new();

    public void RegisterListener(GameEventListener<T> listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener<T> listener)
    {
        listeners.Remove(listener);
    }

    public void Raise(T item)
    {
        for (int i = 1; i <= listeners.Count; i++)
        {
            // the "^i"th index means the (i-1)th index from the end of the array
            listeners[^i].onEventRaised(item);
        }
    }
}