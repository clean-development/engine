namespace Components
{
    public interface EventDispatcher
    {
        void Dispatch<T>(T entity);
    }
}
