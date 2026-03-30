using Interfaces;

namespace InputContent
{
    public class InputFactory
    {
        public IPlayerInput Create(Joystick moveJoystick, LookAreaInput lookAreaInput)
        {
#if UNITY_EDITOR
            return new PCInput();

#else
 return new MobileInput(moveJoystick,lookAreaInput);
#endif
        }
    }
}