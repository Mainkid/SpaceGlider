using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputService : IInputService
    {
        public bool isMoveForwardButtonUp() =>
            UnityEngine.Input.GetKey(KeyCode.Space);
    }
}