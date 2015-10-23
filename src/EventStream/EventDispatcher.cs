namespace EventStream
{
    public interface EventDispatcher
    {
        void Dispatch<T>(T entity);
    }
}
