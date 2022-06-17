using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// README: http://bernardopacheco.net/using-an-event-manager-to-decouple-your-game-in-unity
/// This has examples of how to use this event system. The clunkies part is that it uses dictionaries to pass in parameters, so you have to get the param var name correct.
/// It also uses Actions, and Events should be a little bit faster, but let's see how it performs.
/// Every event handler method will take in [Dictionary<string, object> eventParams] and then finds the values as shown in Consumer.cs of the example
/// </summary>

public class EventManager : MonoBehaviour
{
    private Dictionary<EventType, Action<Dictionary<string, object>>> eventDictionary;
    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene");
                }
                else
                {
                    eventManager.Init();
                    DontDestroyOnLoad(eventManager); //Sets this to not be destroyed when reloading scene
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventType, Action<Dictionary<string, object>>>();
        }
    }

    public static void StartListening(EventType eventType, Action<Dictionary<string, object>> listener)
    {
		if (instance.eventDictionary.TryGetValue(eventType, out Action<Dictionary<string, object>> thisEvent))
		{
			thisEvent += listener;
			instance.eventDictionary[eventType] = thisEvent;
		}
		else
		{
			thisEvent += listener;
			instance.eventDictionary.Add(eventType, thisEvent);
		}
	}

    public static void StopListening(EventType eventType, Action<Dictionary<string, object>> listener)
    {
        if (eventManager == null) return;
		if (instance.eventDictionary.TryGetValue(eventType, out Action<Dictionary<string, object>> thisEvent))
		{
			thisEvent -= listener;
			instance.eventDictionary[eventType] = thisEvent;
		}
	}

    public static void TriggerEvent(EventType eventType, Dictionary<string, object> message)
    {
        Action<Dictionary<string, object>> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventType, out thisEvent))
		{
			thisEvent.Invoke(message);
		}
	}

    public enum EventType
	{
        experienceChangedEvent,
        levelChangedEvent
	}

}
