using UnityEngine;
using System.Collections;
using InControl;

public class KeyboardProfile : UnityInputDeviceProfile
{
    public KeyboardProfile()
    {
        Name = "Keyboard";
        Meta = "A keyboard.";

        // This profile only works on desktops.
        SupportedPlatforms = new[]
        {
            "Windows",
            "Mac",
            "Linux"
        };

        Sensitivity = 1.0f;
        LowerDeadZone = 0.0f;
        UpperDeadZone = 1.0f;

        ButtonMappings = new[]
        {
            new InputControlMapping
            {
                Handle = "Jump",
                Target = InputControlType.Action1,
                Source = KeyCodeButton(KeyCode.Z, KeyCode.Space)
            },
            new InputControlMapping
            {
                Handle = "Attack",
                Target = InputControlType.Action2,
                Source = KeyCodeButton(KeyCode.X, KeyCode.J)
            },
            new InputControlMapping
            {
                Handle = "Transform?",
                Target = InputControlType.Action3,
                Source = KeyCodeButton(KeyCode.V)
            }
        };

        AnalogMappings = new[]
        {
            new InputControlMapping
            {
                Handle = "Move X",
			    Target = InputControlType.LeftStickX,
                // KeyCodeAxis splits the two KeyCodes over an axis. The first is negative, the second positive.
                Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
            },
            new InputControlMapping
            {
				Handle = "Move Y",
				Target = InputControlType.LeftStickY,
				// Notes that up is positive in Unity, therefore the order of KeyCodes is down, up.
				Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
			},
			new InputControlMapping {
				Handle = "Move X Alternate",
				Target = InputControlType.LeftStickX,
				Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
			},
			new InputControlMapping {
                Handle = "Move Y Alternate",
			    Target = InputControlType.LeftStickY,
				Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
			}
        };
    }
}
