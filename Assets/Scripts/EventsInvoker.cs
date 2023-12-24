using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace.Utils
{
    public class EventsInvoker : MonoBehaviour
    {
        private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;

        private static EventsInvoker eventInvoker;

        public static EventsInvoker instance
        {
            get
            {
                if (!eventInvoker)
                {
                    eventInvoker = FindObjectOfType(typeof(EventsInvoker)) as EventsInvoker;

                    if (!eventInvoker)
                    {
                        Debug.LogError(
                            "There needs to be one active EventManager script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventInvoker.Init();
                        DontDestroyOnLoad(eventInvoker);
                    }
                }

                return eventInvoker;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
            }
        }

        public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            Action<Dictionary<string, object>> thisEvent;

            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent += listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
            else
            {
                thisEvent += listener;
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
        {
            if (eventInvoker == null) return;
            Action<Dictionary<string, object>> thisEvent;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent -= listener;
                instance.eventDictionary[eventName] = thisEvent;
            }
        }

        public static void TriggerEvent(string eventName, Dictionary<string, object> message)
        {
            Action<Dictionary<string, object>> thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(message);
            }
        }
    }
}