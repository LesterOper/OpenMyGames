using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Utils;
using Elements;
using Elements.ElementsConfig;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;

    private void OnEnable()
    {
        EventsInvoker.StartListening(EventsKeys.SWIPE, CheckSwitchBetweenElements);
    }

    private void OnDisable()
    {
        EventsInvoker.StopListening(EventsKeys.SWIPE, CheckSwitchBetweenElements);
    }

    void Start()
    {
        
    }

    private void CheckSwitchBetweenElements(Dictionary<string, object> parameters)
    {
        SwipeEventsArgs swipeEventsArgs = (SwipeEventsArgs) parameters[EventsKeys.SWIPE];
        Debug.Log("Event " + swipeEventsArgs.SwipeDirection);
        //bool isCan = _levelGenerator.CanSwitch(swipeEventsArgs.ElementPosition, swipeEventsArgs.SwipeDirection);
    }
    
}
