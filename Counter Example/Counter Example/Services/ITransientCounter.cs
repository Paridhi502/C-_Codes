namespace ServiceLifetimeDemo
{
    public interface ITransientCounter
    {
        int Count { get; }
        void Increment();
    }
    public class TransientCounter : ITransientCounter
    {
        private int _count;
        public int Count => _count;
        public void Increment() => _count++;
    }
}
