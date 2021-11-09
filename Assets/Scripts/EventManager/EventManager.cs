using System.Collections;
using System.Collections.Generic;
using System;

public class EventManager
{
    private static Dictionary<Parameter, Action<object[]>> eventDictionary;

    public enum Parameter
    {
        StartCombat
    }

    public static void Subscribe(Parameter subscribedEvent, Action<object[]> function)
    {
        if (eventDictionary == null)
            eventDictionary = new Dictionary<Parameter, Action<object[]>>();

        if (!eventDictionary.ContainsKey(subscribedEvent))
            eventDictionary.Add(subscribedEvent, null);

        eventDictionary[subscribedEvent] += function;
    }

    public static void Unsubscribe(Parameter subscribedEvent, Action<object[]> function)
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