using System;

namespace _Project._Code.Meta.Input
{
    public interface IInputSystem
    {
        public event Action OnSelectEvent;
        public event Action OnTriggerEvent;
    }
}