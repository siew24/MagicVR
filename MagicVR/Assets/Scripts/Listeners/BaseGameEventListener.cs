using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BaseGameEventListener : MonoBehaviour
{
    public BaseGameEvent gameEvent;
    public UnityEvent Response;

    void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    public void onEventRaised()
    {
        Response.Invoke();
    }
}