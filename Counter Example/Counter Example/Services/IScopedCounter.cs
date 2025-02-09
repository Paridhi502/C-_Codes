namespace ServiceLifetimeDemo
{
    public interface IScopedCounter
    {
        int Count { get; }
        void Increment();
    }
    public class ScopedCounter : IScopedCounter
    {
        private int _count;
        public int Count => _count;
        public void Increment() => _count++;
    }
}
