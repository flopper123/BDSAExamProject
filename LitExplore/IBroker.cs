using System;

namespace LitExplore
{
    public interface IBroker<T>
    {
        public void Subscribe(Action<T> action);
        public void Publish(T to);
    }
}