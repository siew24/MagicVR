using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameEventListener<T> : MonoBehaviour
{
    public GameEvent<T> gameEvent;
    public UnityEvent<T> Response;

    void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void onEventRaised(T item)
    {
        Response.Invoke(item);
    }
}