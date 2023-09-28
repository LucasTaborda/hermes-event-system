namespace Hermes
{
    public class EventData<T>
    {
        public T data;

        public EventData(T data)
        {
            this.data = data;
        }
    }
}
