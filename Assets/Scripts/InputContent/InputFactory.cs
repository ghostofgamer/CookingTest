using Interfaces;
using UnityEngine;

namespace InputContent
{
    public class InputFactory
    {
        public IPlayerInput Create(Joystick moveJoystick, RectTransform lookArea)
        {
            return new PCInput();
            
#if UNITY_ANDROID || UNITY_IOS
            Debug.Log("InputFactory::Create");
            return new MobileInput(moveJoystick, lookArea);
#else
        return new PCInput();
#endif
        }
    }
}