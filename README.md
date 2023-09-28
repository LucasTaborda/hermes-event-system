# Hermes Event System

Simple sistema que permite disparar y escuchar enventos fácilmente en Unity.

# Cómo se utiliza (Ejemplo con un evento que envía texto)

1. Creas el método que quieres que se ejecute al dispararse un evento:

´´´
private void MetodoSuscripto(EventData<string> mensaje)
{
    Debug.log(mensaje.data);
}
´´´

2. Suscribes la función al evento que quieres que se dispare

´´´
Action<EventData<string>> suscriptor = MetodoSuscripto;
EventManager.Instance.AddEventListener("On Saludar", suscriptor);
´´´

3. En el momento que quieres que se dispare el evento, agregas el código:

´´´
var miSaludo = new EventData<string>("Hola");
EventManager.Instance.DispatchEvent("On Saludar", miSaludo);

´´´

4. Finalmente, si quieres dejar de escuchar ese evento, agregas este código:

´´´
EventManager.Instance.RemoveEventListener("On Saludar", suscriptor);
´´´

# Aclaraciones

1. Los delegados que se suscriben a los eventos pueden recibir un parámetro solo del tipo genérico EventData. Pero al ser genérico, puedes recibir cualquier tipo de dato especificándolo en la T: EventData<T>
2. Si necesitas recibir más de un dato como parámetro, puedes crear un ObjetoCustom que tenga todos los datos que necesitas. Posteriormente creas un delegado que reciba como parámetro un EventData<ObjetoCustom>
3. No se puede desuscribir a un evento al momento de ser despachado. Debe ser después de que termine de llamar a todos los suscriptores.