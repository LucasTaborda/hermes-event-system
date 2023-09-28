using UnityEngine;
using System;
using Hermes;

public class TestEventSystem : MonoBehaviour
{
    Action<EventData<string>> listener; //El delegado que se llamará cuando se dispare el evento.

    void Start()
    {
        listener = EventListener; //Le asigno el nombre de la función

        EventManager.Instance.AddEventListener("On Hi", listener); //Comienzo a escuchar el evento "On Hi" y le digo al manager qué función quiero que se ejecute cuando se dispare.

        Invoke("DispatchEventString", 3); //Hago que se dispare un evento en 3 segundos.
    }


    private void DispatchEventString()
    {
        var hi = new EventData<string>("ola k ase"); //El evento tiene el string "ola k ase" como dato

        EventManager.Instance.DispatchEvent("On Hi", hi); //Disparo el evento y envió el dato a todas las funciones suscriptas.
    }


    private void EventListener(EventData<string> message) //La función que se ejecuta al dispararse el evento
    {
        Debug.Log(message.data); //Imprimo el dato que me trajo el evento

        Invoke("RemoveEventListener", 1); //Borro la suscripción al evento en 1 segundo. No lo puedo borrar inmediatamente porque está recorriendo la lista de funciones y se rompería el proceso.
    }

    private void RemoveEventListener()
    {
        EventManager.Instance.RemoveEventListener("On Hi", listener); //Se borra el listener.
    }
}
