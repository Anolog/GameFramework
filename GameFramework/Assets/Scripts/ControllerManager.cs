using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager {

    private List<Controller> m_ListOfControllers = new List<Controller>();

    //delegate declarations that our events will use
    public delegate void HandleButtonPress(Controller aGamePad);
    public delegate void HandleJoystick(Controller aGamePad, Vector2 aJoystick);

    //Face button press events
    public event HandleButtonPress HandleAButton,
                                   HandleXButton,
                                   HandleYButton,
                                   HandleBButton;

    //DPad button press events
    public event HandleButtonPress HandleDPadUp,
                                   HandleDPadDown,
                                   HandleDPadLeft,
                                   HandleDPadRight;

    //L1-3 and R1-3 button press events
    public event HandleButtonPress HandleRightBumper,
                                   HandleLeftBumper,
                                   HandleRightTrigger,
                                   HandleLeftTrigger,
                                   HandleRightJoystickClick,
                                   HandleLeftJoystickClick;

    //Other button press events
    public event HandleButtonPress HandleStart,
                                   HandleBack,
                                   HandleGuide; //Xbox logo button

    //Joystick movement events
    public event HandleJoystick HandleRightStick,
                                HandleLeftStick;

    //retruns the controller at the specified index
    public Controller GetController(int aControllerIndex)
    {
        return m_ListOfControllers[aControllerIndex];
    }

    //adds a controller to the ListOfControllers
    public void AddController(Controller aControllerToAdd)
    {
        m_ListOfControllers.Add(aControllerToAdd);
    }

    //removes the controller at the specified index
    public void RemoveController(int aControllerIndexToRemove)
    {
        m_ListOfControllers.RemoveAt(aControllerIndexToRemove);
    }

    //goes through all of the controllers in the list and calls their individual handle input
    public void HandleInput()
    {
        foreach (Controller controller in m_ListOfControllers)
        {
            controller.HandleInput(this);
        }
    }
}
