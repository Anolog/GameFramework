using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller {

    //delegate declarations that our events will use
    public delegate void HandleButtonPress();
    public delegate void HandleTrigger(float aValue);
    public delegate void HandleJoystick(Vector2 aJoystick);

    //Face button press events
    public event HandleButtonPress HandleAButton,
                                   HandleXButton,
                                   HandleYButton,
                                   HandleBButton;
    //Face button held events
    public event HandleButtonPress HandleAButtonHold,
                                   HandleXButtonHold,
                                   HandleYButtonHold,
                                   HandleBButtonHold;

    //DPad button press events
    public event HandleButtonPress HandleDPadUp,
                                   HandleDPadDown,
                                   HandleDPadLeft,
                                   HandleDPadRight;
    //DPad button held events
    public event HandleButtonPress HandleDPadUpHold,
                                   HandleDPadDownHold,
                                   HandleDPadLeftHold,
                                   HandleDPadRightHold;


    //L1-3 and R1-3 button press events
    public event HandleButtonPress HandleRightBumper,
                                   HandleLeftBumper,
                                   HandleRightTriggerTap, // in this case the triggers will be used as standard buttons
                                   HandleLeftTriggerTap,
                                   HandleRightJoystickClick,
                                   HandleLeftJoystickClick;
    //left and right bumber and stick held events
    public event HandleButtonPress HandleRightBumperHold,
                                   HandleLeftBumperHold,
                                   HandleRightJoystickHold,
                                   HandleLeftJoystickHold;

    //trigger events for when the value matters
    public event HandleTrigger HandleRightTrigger,
                               HandleLeftTrigger;

    //Other button press events
    public event HandleButtonPress HandleStart,
                                   HandleBack,
                                   HandleGuide; //Xbox logo button
    //Other button held events
    public event HandleButtonPress HandleStartHold,
                                   HandleBackHold,
                                   HandleGuideHold;

    //Joystick movement events
    public event HandleJoystick HandleRightStick,
                                HandleLeftStick;




    private x360GamePad m_GamePad;

    // this is the time a button needs to be pressed for before the held event will be called
    [SerializeField]
    private Dictionary<string, float> m_HoldTimes = new Dictionary<string, float>();

    // the timer used for seeing if a button is held down long enough
    private Dictionary<string, float> m_HoldTimer = new Dictionary<string, float>();

    public int ControllerNumber;

    // takes an index from 0-3 for which controller number it is
    public Controller(int aIndex)
    {
        m_GamePad = new x360GamePad(aIndex);
        ControllerNumber = aIndex;
    }

    //checks all of the buttons and calls the appropriate events
    public void HandleInput()
    {
        //you need to update the gamepad so that internally it knows all of its buttons
        m_GamePad.Update();

        #region Check face buttons
        if (m_GamePad.GetButton("A"))
        {
            if (m_GamePad.GetButtonDown("A")) // first check if the button was just pressed this frame
            {
                HandleAButton();
                //start the hold timer for this button
                m_HoldTimer["A"] = m_HoldTimes["A"] + Time.time;
            }
            if (m_HoldTimer["A"] <= Time.time)
            {
                HandleAButtonHold();
            }
        }
        if (m_GamePad.GetButton("B"))
        {
            if (m_GamePad.GetButtonDown("B")) // first check if the button was just pressed this frame
            {
                HandleBButton();
                //start the hold timer for this button
                m_HoldTimer["B"] = m_HoldTimes["B"] + Time.time;
            }
            if (m_HoldTimer["B"] <= Time.time)
            {
                HandleBButtonHold();
            }
        }
        if (m_GamePad.GetButton("X"))
        {
            if (m_GamePad.GetButtonDown("X")) // first check if the button was just pressed this frame
            {
                HandleXButton();
                //start the hold timer for this button
                m_HoldTimer["X"] = m_HoldTimes["X"] + Time.time;
            }
            if (m_HoldTimer["X"] <= Time.time)
            {
                HandleXButtonHold();
            }
        }
        if (m_GamePad.GetButton("Y"))
        {
            if (m_GamePad.GetButtonDown("Y")) // first check if the button was just pressed this frame
            {
                HandleYButton();
                //start the hold timer for this button
                m_HoldTimer["Y"] = m_HoldTimes["Y"] + Time.time;
            }
            if (m_HoldTimer["Y"] <= Time.time)
            {
                HandleYButtonHold();
            }
        }
        #endregion

        #region Check DPad
        if (m_GamePad.GetButton("DPadUp"))
        {
            if (m_GamePad.GetButtonDown("DPadUp")) // first check if the button was just pressed this frame
            {
                HandleDPadUp();
                //start the hold timer for this button
                m_HoldTimer["DPadUp"] = m_HoldTimes["DPadUp"] + Time.time;
            }
            if (m_HoldTimer["DPadUp"] <= Time.time)
            {
                HandleDPadUpHold();
            }
        }
        if (m_GamePad.GetButton("DPadDown"))
        {
            if (m_GamePad.GetButtonDown("DPadDown")) // first check if the button was just pressed this frame
            {
                HandleDPadDown();
                //start the hold timer for this button
                m_HoldTimer["DPadDown"] = m_HoldTimes["DPadDown"] + Time.time;
            }
            if (m_HoldTimer["DPadDown"] <= Time.time)
            {
                HandleDPadDownHold();
            }
        }
        if (m_GamePad.GetButton("DPadLeft"))
        {
            if (m_GamePad.GetButtonDown("DPadLeft")) // first check if the button was just pressed this frame
            {
                HandleDPadLeft();
                //start the hold timer for this button
                m_HoldTimer["DPadLeft"] = m_HoldTimes["DPadLeft"] + Time.time;
            }
            if (m_HoldTimer["DPadLeft"] <= Time.time)
            {
                HandleDPadLeftHold();
            }
        }
        if (m_GamePad.GetButton("DPadRight"))
        {
            if (m_GamePad.GetButtonDown("DPadRight")) // first check if the button was just pressed this frame
            {
                HandleDPadRight();
                //start the hold timer for this button
                m_HoldTimer["DPadRight"] = m_HoldTimes["DPadRight"] + Time.time;
            }
            if (m_HoldTimer["DPadRight"] <= Time.time)
            {
                HandleDPadRightHold();
            }
        }
        #endregion

        #region Check guide, back, start
        if (m_GamePad.GetButton("Guide"))
        {
            if (m_GamePad.GetButtonDown("Guide")) // first check if the button was just pressed this frame
            {
                HandleGuide();
                //start the hold timer for this button
                m_HoldTimer["Guide"] = m_HoldTimes["Guide"] + Time.time;
            }
            if (m_HoldTimer["Guide"] <= Time.time)
            {
                HandleGuideHold();
            }
        }
        if (m_GamePad.GetButton("Back"))
        {
            if (m_GamePad.GetButtonDown("Back")) // first check if the button was just pressed this frame
            {
                HandleBack();
                //start the hold timer for this button
                m_HoldTimer["Back"] = m_HoldTimes["Back"] + Time.time;
            }
            if (m_HoldTimer["Back"] <= Time.time)
            {
                HandleBackHold();
            }
        }
        if (m_GamePad.GetButton("Start"))
        {
            if (m_GamePad.GetButtonDown("Start")) // first check if the button was just pressed this frame
            {
                HandleStart();
                //start the hold timer for this button
                m_HoldTimer["Start"] = m_HoldTimes["Start"] + Time.time;
            }
            if (m_HoldTimer["Start"] <= Time.time)
            {
                HandleStartHold();
            }
        }
        #endregion

        #region Check joystick presses
        if (m_GamePad.GetButton("L3"))
        {
            if (m_GamePad.GetButtonDown("L3")) // first check if the button was just pressed this frame
            {
                HandleLeftJoystickClick();
                //start the hold timer for this button
                m_HoldTimer["L3"] = m_HoldTimes["L3"] + Time.time;
            }
            if (m_HoldTimer["L3"] <= Time.time)
            {
                HandleLeftJoystickHold();
            }
        }
        if (m_GamePad.GetButton("R3"))
        {
            if (m_GamePad.GetButtonDown("R3")) // first check if the button was just pressed this frame
            {
                HandleRightJoystickClick();
                //start the hold timer for this button
                m_HoldTimer["R3"] = m_HoldTimes["R3"] + Time.time;
            }
            if (m_HoldTimer["R3"] <= Time.time)
            {
                HandleRightJoystickHold();
            }
        }
        #endregion

        #region Check bumpers
        if (m_GamePad.GetButton("LB"))
        {
            if (m_GamePad.GetButtonDown("LB")) // first check if the button was just pressed this frame
            {
                HandleLeftBumper();
                //start the hold timer for this button
                m_HoldTimer["LB"] = m_HoldTimes["LB"] + Time.time;
            }
            if (m_HoldTimer["LB"] <= Time.time)
            {
                HandleLeftBumperHold();
            }
        }
        if (m_GamePad.GetButton("RB"))
        {
            if (m_GamePad.GetButtonDown("RB")) // first check if the button was just pressed this frame
            {
                HandleRightBumper();
                //start the hold timer for this button
                m_HoldTimer["RB"] = m_HoldTimes["RB"] + Time.time;
            }
            if (m_HoldTimer["RB"] <= Time.time)
            {
                HandleRightBumperHold();
            }
        }
        #endregion

        #region Check triggers
        if (m_GamePad.GetLeftTriggerTap())
        {
            HandleLeftTriggerTap();
        }
        if (m_GamePad.GetLeftTrigger() >= 0.1f)
        {
            HandleLeftTrigger(m_GamePad.GetLeftTrigger());
        }
        if (m_GamePad.GetRightTriggerTap())
        {
            HandleRightTriggerTap();
        }
        if (m_GamePad.GetRightTrigger() >= 0.1f)
        {
            HandleRightTrigger(m_GamePad.GetRightTrigger());
        }
        #endregion

        #region Check joysticks
        Vector2 leftStick = new Vector2(m_GamePad.GetLeftStick().X, m_GamePad.GetLeftStick().Y);
        Vector2 rightStick = new Vector2(m_GamePad.GetRightStick().X, m_GamePad.GetRightStick().Y);
        HandleLeftStick(leftStick);
        HandleRightStick(rightStick);
        #endregion

        //after handling all of the input we call refresh to settup the previous values of all of the buttons
        m_GamePad.Refresh();
    }

    // Adds rumble to the controller for the specified amount of time and power
    // Fade time is how long into the rumble it should start to fade away
    public void AddRumble(float aTimer, Vector2 aPower, float aFadeTime)
    {
        m_GamePad.AddRumble(aTimer, aPower, aFadeTime);
    }
}
