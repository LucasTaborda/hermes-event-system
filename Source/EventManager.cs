using System;
using System.Collections.Generic;

namespace Hermes
{
    public class EventManager
    {
        private Dictionary<string, List<Delegate>> _listeners = new();

        private static EventManager _instance;

        public static EventManager Instance
        {
            get
            {
                if (_instance == null) _instance = new EventManager();
                return _instance;
            }
        }


        private EventManager() { }


        public void AddEventListener<T>(string eventName, Action<EventData<T>> listener)
        {
            if (!_listeners.ContainsKey(eventName))
            {
                _listeners.Add(eventName, new List<Delegate>());
            }

            _listeners[eventName].Add(listener);
        }


        public void RemoveEventListener<T>(string eventName, Action<EventData<T>> listener)
        {
            if (_listeners.ContainsKey(eventName))
            {
                _listeners[eventName].Remove(listener);

                if (_listeners[eventName].Count == 0)
                    _listeners.Remove(eventName);
            }
        }


        public void DispatchEvent<T>(string eventName, EventData<T> data)
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
    }
}
