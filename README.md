# Hermes Event System

**Un simple sistema que permite disparar y escuchar eventos fácilmente en Unity.**

&nbsp;

## Instalación

1. Clona el proyecto en la carpeta Assets de Unity.
2. Agrega la librería en los scripts donde quieras utilizarla: `using Hermes;`

&nbsp;

## Cómo se utiliza (Ejemplo con un evento que envía un string saludando)

1. En el momento y lugar que quieras que se dispare el evento, agregas este código. (El evento puede llamarse como quieras. Yo a este lo llamé "On Saludar").

```cs
var miSaludo = new EventData<string>("Hola"); //Dato que envía el evento. Yo necesito enviar un string que saluda. Así que declaro un EventData de tipo string.
EventManager.Instance.DispatchEvent("On Saludar", miSaludo);
```

&nbsp;

2. En el objeto que quieras que haga algo cuando se dispare el evento, creas el método que se usará como delegado. Debe recibir como parámetro un EventData.

```cs
private void MetodoSuscripto(EventData<string> datoRecibidoDelEvento)
{
    Debug.log(datoRecibidoDelEvento.data);
}
```

&nbsp; 
 
3. Suscribes el método al evento. El delegado debe ser declarado como un `Action<EventData<T>>`, siendo T el tipo de dato que necesitamos enviar desde el evento (En este caso un string).

```cs
Action<EventData<string>> suscriptor = MetodoSuscripto;
EventManager.Instance.AddEventListener("On Saludar", suscriptor);
``` 

&nbsp;
 
4. Finalmente, si quieres dejar de escuchar ese evento, agregas este código:

```cs
EventManager.Instance.RemoveEventListener("On Saludar", suscriptor);
```

&nbsp;

**_Para ver un ejemplo completo del sistema funcionando, en la carpeta_ Example _hay una escena con la implementación del mismo. (Si al abrir la escena el game object Test perdió la referencia al script, agrégale el componente TestEventSystem que está dentro de la carpeta Example)._**

&nbsp;

## Aclaraciones

1. Los delegados que se suscriben a los eventos solo pueden recibir un parámetro del tipo EventData. Pero al ser genérico, puedes recibir cualquier tipo de dato especificándolo en la instanciación del `EventData<T>`
2. Si necesitas recibir más de un dato como parámetro, puedes crear un ObjetoCustom que tenga todos los datos que necesitas. Posteriormente creas un delegado que reciba como parámetro un `EventData<ObjetoCustom>`
3. No se puede desuscribir a un evento al momento de ser despachado. Debe ser después de que termine de llamar a todos los suscriptores.
