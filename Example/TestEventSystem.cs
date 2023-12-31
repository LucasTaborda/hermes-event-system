using UnityEngine;
using System;
using Hermes;

public class TestEventSystem : MonoBehaviour
{
    Action<EventData<string>> listener; //El delegado que se llamará cuando se dispare el evento.


    void Start()
    {
        listener = MyEventListener; //Le asigno al delegado el método en cuestión

        EventManager.AddEventListener("On 3 Seconds", listener); //Comienzo a escuchar el evento "On 3 Seconds" y le digo al manager qué método quiero que se ejecute cuando se dispare.

        Debug.Log($"{gameObject.name}: Me suscribí al evento 'On 3 Seconds'");

        Invoke("DispatchEventString", 3); //Hago que se dispare un evento en 3 segundos.
    }


    private void DispatchEventString()
    {
        var message = new EventData<string>("El evento fue disparado"); //El evento enviará este string como parámetro

        EventManager.DispatchEvent("On 3 Seconds", message); //Disparo el evento y envio el dato a todas los métodos suscriptos.
    }


    private void MyEventListener(EventData<string> message) //La función que se ejecuta al dispararse el evento
    {
        Debug.Log($"{gameObject.name}: Evento 'On 3 Seconds' escuchado. Envió este mensaje: '{message.data}'"); //Imprimo el dato que me trajo el evento

        Invoke("RemoveMyEventListener", 1); //Borro la suscripción al evento en 1 segundo. No lo puedo borrar inmediatamente porque está recorriendo la lista de métodos y se rompería el proceso.
    }


    private void RemoveMyEventListener()
    {
        EventManager.RemoveEventListener("On 3 Seconds", listener); //Se borra el listener.

        Debug.Log($"{gameObject.name}: Me desuscribí al evento 'On 3 Seconds'");
    }
}
