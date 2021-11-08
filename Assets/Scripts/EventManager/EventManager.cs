using System.Collections;
using System.Collections.Generic;

public class EventManager
{
    public delegate void EventCallback(params object[] parameters);

    private static Dictionary<Parameter, EventCallback> eventDictionary;

    public enum Parameter
    {
        StartCombat
    }

    public static void Subscribe(Parameter subscribedEvent, EventCallback function)
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<Parameter, EventCallback>();

        if (!eventDictionary.ContainsKey(subscribedEvent))
            eventDictionary.Add(subscribedEvent, null);

        eventDictionary[subscribedEvent] += function;
    }

    public static void Unsubscribe(Parameter subscribedEvent, EventCallback function)
    {
        if (eventDictionary == null)
            return;

        if (!eventDictionary.ContainsKey(subscribedEvent))
            return;

        eventDictionary[subscribedEvent] -= function;
    }

    public static void CallEvent(Parameter eventName, params object[] parameters)
    {
        if (eventDictionary == null) return;

        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName](parameters);
    }

    public void CleanDictionery()
    {
        eventDictionary.Clear();
    }
}