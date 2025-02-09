namespace ServiceLifetimeDemo
{
    public interface ISingletonCounter
    {
        int Count { get; }
        void Increment();
    }
    public class SingletonCounter : ISingletonCounter
    {
        private int _count;
        public int Count => _count;
        public void Increment() => _count++;
    }
}
