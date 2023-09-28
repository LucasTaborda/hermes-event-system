# Hermes Event System

**Un simple sistema que permite disparar y escuchar eventos fácilmente en Unity.**

&nbsp;

## Instalación

1. Clona el proyecto en la carpeta Assets de Unity.
2. Agrega la librería en los scripts donde quieras utilizarla: `using Hermes;`

&nbsp;

## Cómo se utiliza (Ejemplo con un evento que envía texto)

1. Creas el método que quieres que se ejecute al dispararse un evento:

```cs
private void MetodoSuscripto(EventData<string> mensaje)
{
    Debug.log(mensaje.data);
}
```

&nbsp; 
 
2. Suscribes la función al evento que quieres que se dispare

```cs
Action<EventData<string>> suscriptor = MetodoSuscripto;
EventManager.Instance.AddEventListener("On Saludar", suscriptor);
```

&nbsp;
 
3. En el momento y lugar que quieras que se dispare el evento, agregas este código:

```cs
var miSaludo = new EventData<string>("Hola");
EventManager.Instance.DispatchEvent("On Saludar", miSaludo);
```

&nbsp;
 
4. Finalmente, si quieres dejar de escuchar ese evento, agregas este código:

```cs
EventManager.Instance.RemoveEventListener("On Saludar", suscriptor);
```

&nbsp;

**_Para ver un ejemplo completo del sistema funcionando, en la carpeta_ Example _hay una implementación del mismo._**

&nbsp;

## Aclaraciones

1. Los delegados que se suscriben a los eventos pueden recibir un parámetro solo del tipo genérico EventData. Pero al ser genérico, puedes recibir cualquier tipo de dato especificándolo en la T: EventData<T>
2. Si necesitas recibir más de un dato como parámetro, puedes crear un ObjetoCustom que tenga todos los datos que necesitas. Posteriormente creas un delegado que reciba como parámetro un EventData<ObjetoCustom>
3. No se puede desuscribir a un evento al momento de ser despachado. Debe ser después de que termine de llamar a todos los suscriptores.
