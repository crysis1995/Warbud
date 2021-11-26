namespace Warbud.Shared.Abstraction.Interfaces
{
    public interface  IValueType<out T>
    {
        public T Value { get; }
    }
}