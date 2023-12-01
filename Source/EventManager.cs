using System;
using System.Collections.Generic;

namespace Hermes
{
    public class EventManager
    {
        public delegate void Callback();
        private static Dictionary<string, List<Delegate>> _listeners = new();


        public static void AddEventListener<T>(string eventName, Action<EventData<T>> listener)
        {
            if (!_listeners.ContainsKey(eventName))
            {
                _listeners.Add(eventName, new List<Delegate>());
            }

            _listeners[eventName].Add(listener);
        }


        public static void AddEventListener(string eventName, Callback listener)
        {
            if (!_listeners.ContainsKey(eventName))
            {
                _listeners.Add(eventName, new List<Delegate>());
            }

            _listeners[eventName].Add(listener);
        }


        public static void RemoveEventListener<T>(string eventName, Action<EventData<T>> listener)
        {
            if (_listeners.ContainsKey(eventName))
            {
                _listeners[eventName].Remove(listener);

                if (_listeners[eventName].Count == 0)
                    _listeners.Remove(eventName);
            }
        }


        public static void RemoveEventListener(string eventName, Callback listener)
        {
            if (_listeners.ContainsKey(eventName))
            {
                _listeners[eventName].Remove(listener);

                if (_listeners[eventName].Count == 0)
                    _listeners.Remove(eventName);
            }
        }


        public static void DispatchEvent<T>(string eventName, EventData<T> data)
        {
            if (!_listeners.ContainsKey(eventName)) return;

            foreach (var listener in _listeners[eventName])
            {
                if (listener is Action<EventData<T>> typedListener)
                {
                    typedListener.Invoke(data);
                }
            }
        }


        public static void DispatchEvent(string eventName)
        {
            if (!_listeners.ContainsKey(eventName)) return;

            foreach (var listener in _listeners[eventName])
            {
                if(listener is Callback typedListener)
                {
                    typedListener();
                }
            }
        }


    }
}
