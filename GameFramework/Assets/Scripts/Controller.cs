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
    private Dictionary<string, float> m_HoldTimes = new Dictionary<string, float>();

    // the timer used for seeing if a button is held down long enough
    private Dictionary<string, float> m_HoldTimer = new Dictionary<string, float>();

    public int ControllerNumber;

    // takes an index from 0-3 for which controller number it is
    public Controller(int aIndex)
    {
        m_GamePad = new x360GamePad(aIndex);
        ControllerNumber = aIndex;

        m_HoldTimes["A"] = 0.3f;
        m_HoldTimes["B"] = 0.3f;
        m_HoldTimes["X"] = 0.3f;
        m_HoldTimes["Y"] = 0.3f;
        m_HoldTimes["DPadUp"] = 0.3f;
        m_HoldTimes["DPadDown"] = 0.3f;
        m_HoldTimes["DPadLeft"] = 0.3f;
        m_HoldTimes["DPadRight"] = 0.3f;
        m_HoldTimes["Guide"] = 0.3f;
        m_HoldTimes["Start"] = 0.3f;
        m_HoldTimes["Back"] = 0.3f;
        m_HoldTimes["L3"] = 0.3f;
        m_HoldTimes["R3"] = 0.3f;
        m_HoldTimes["LB"] = 0.3f;
        m_HoldTimes["RB"] = 0.3f;
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
                if (HandleAButton != null)
                {
                    HandleAButton();
                }
                //start the hold timer for this button
                m_HoldTimer["A"] = m_HoldTimes["A"] + Time.time;
            }
            if (m_HoldTimer["A"] <= Time.time)
            {
                if (HandleAButtonHold != null)
                {
                    HandleAButtonHold();
                }
            }
        }
        if (m_GamePad.GetButton("B"))
        {
            if (m_GamePad.GetButtonDown("B")) // first check if the button was just pressed this frame
            {
                if (HandleBButton != null)
                {
                    HandleBButton(); 
                }
                //start the hold timer for this button
                m_HoldTimer["B"] = m_HoldTimes["B"] + Time.time;
            }
            if (m_HoldTimer["B"] <= Time.time)
            {
                if (HandleBButtonHold != null)
                {
                    HandleBButtonHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("X"))
        {
            if (m_GamePad.GetButtonDown("X")) // first check if the button was just pressed this frame
            {
                if (HandleXButton != null)
                {
                    HandleXButton(); 
                }
                //start the hold timer for this button
                m_HoldTimer["X"] = m_HoldTimes["X"] + Time.time;
            }
            if (m_HoldTimer["X"] <= Time.time)
            {
                if (HandleXButtonHold != null)
                {
                    HandleXButtonHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("Y"))
        {
            if (m_GamePad.GetButtonDown("Y")) // first check if the button was just pressed this frame
            {
                if (HandleYButton != null)
                {
                    HandleYButton(); 
                }
                //start the hold timer for this button
                m_HoldTimer["Y"] = m_HoldTimes["Y"] + Time.time;
            }
            if (m_HoldTimer["Y"] <= Time.time)
            {
                if (HandleYButtonHold != null)
                {
                    HandleYButtonHold(); 
                }
            }
        }
        #endregion

        #region Check DPad
        if (m_GamePad.GetButton("DPadUp"))
        {
            if (m_GamePad.GetButtonDown("DPadUp")) // first check if the button was just pressed this frame
            {
                if (HandleDPadUp != null)
                {
                    HandleDPadUp(); 
                }
                //start the hold timer for this button
                m_HoldTimer["DPadUp"] = m_HoldTimes["DPadUp"] + Time.time;
            }
            if (m_HoldTimer["DPadUp"] <= Time.time)
            {
                if (HandleDPadUpHold != null)
                {
                    HandleDPadUpHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("DPadDown"))
        {
            if (m_GamePad.GetButtonDown("DPadDown")) // first check if the button was just pressed this frame
            {
                if (HandleDPadDown != null)
                {
                    HandleDPadDown(); 
                }
                //start the hold timer for this button
                m_HoldTimer["DPadDown"] = m_HoldTimes["DPadDown"] + Time.time;
            }
            if (m_HoldTimer["DPadDown"] <= Time.time)
            {
                if (HandleDPadDownHold != null)
                {
                    HandleDPadDownHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("DPadLeft"))
        {
            if (m_GamePad.GetButtonDown("DPadLeft")) // first check if the button was just pressed this frame
            {
                if (HandleDPadLeft != null)
                {
                    HandleDPadLeft(); 
                }
                //start the hold timer for this button
                m_HoldTimer["DPadLeft"] = m_HoldTimes["DPadLeft"] + Time.time;
            }
            if (m_HoldTimer["DPadLeft"] <= Time.time)
            {
                if (HandleDPadLeftHold != null)
                {
                    HandleDPadLeftHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("DPadRight"))
        {
            if (m_GamePad.GetButtonDown("DPadRight")) // first check if the button was just pressed this frame
            {
                if (HandleDPadRight != null)
                {
                    HandleDPadRight(); 
                }
                //start the hold timer for this button
                m_HoldTimer["DPadRight"] = m_HoldTimes["DPadRight"] + Time.time;
            }
            if (m_HoldTimer["DPadRight"] <= Time.time)
            {
                if (HandleDPadRightHold != null)
                {
                    HandleDPadRightHold(); 
                }
            }
        }
        #endregion

        #region Check guide, back, start
        if (m_GamePad.GetButton("Guide"))
        {
            if (m_GamePad.GetButtonDown("Guide")) // first check if the button was just pressed this frame
            {
                if (HandleGuide != null)
                {
                    HandleGuide(); 
                }
                //start the hold timer for this button
                m_HoldTimer["Guide"] = m_HoldTimes["Guide"] + Time.time;
            }
            if (m_HoldTimer["Guide"] <= Time.time)
            {
                if (HandleGuideHold != null)
                {
                    HandleGuideHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("Back"))
        {
            if (m_GamePad.GetButtonDown("Back")) // first check if the button was just pressed this frame
            {
                if (HandleBack != null)
                {
                    HandleBack(); 
                }
                //start the hold timer for this button
                m_HoldTimer["Back"] = m_HoldTimes["Back"] + Time.time;
            }
            if (m_HoldTimer["Back"] <= Time.time)
            {
                if (HandleBackHold != null)
                {
                    HandleBackHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("Start"))
        {
            if (m_GamePad.GetButtonDown("Start")) // first check if the button was just pressed this frame
            {
                if (HandleStart != null)
                {
                    HandleStart(); 
                }
                //start the hold timer for this button
                m_HoldTimer["Start"] = m_HoldTimes["Start"] + Time.time;
            }
            if (m_HoldTimer["Start"] <= Time.time)
            {
                if (HandleStartHold != null)
                {
                    HandleStartHold(); 
                }
            }
        }
        #endregion

        #region Check joystick presses
        if (m_GamePad.GetButton("L3"))
        {
            if (m_GamePad.GetButtonDown("L3")) // first check if the button was just pressed this frame
            {
                if (HandleLeftJoystickClick != null)
                {
                    HandleLeftJoystickClick(); 
                }
                //start the hold timer for this button
                m_HoldTimer["L3"] = m_HoldTimes["L3"] + Time.time;
            }
            if (m_HoldTimer["L3"] <= Time.time)
            {
                if (HandleLeftJoystickHold != null)
                {
                    HandleLeftJoystickHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("R3"))
        {
            if (m_GamePad.GetButtonDown("R3")) // first check if the button was just pressed this frame
            {
                if (HandleRightJoystickClick != null)
                {
                    HandleRightJoystickClick(); 
                }
                //start the hold timer for this button
                m_HoldTimer["R3"] = m_HoldTimes["R3"] + Time.time;
            }
            if (m_HoldTimer["R3"] <= Time.time)
            {
                if (HandleRightJoystickHold != null)
                {
                    HandleRightJoystickHold(); 
                }
            }
        }
        #endregion

        #region Check bumpers
        if (m_GamePad.GetButton("LB"))
        {
            if (m_GamePad.GetButtonDown("LB")) // first check if the button was just pressed this frame
            {
                if (HandleLeftBumper != null)
                {
                    HandleLeftBumper(); 
                }
                //start the hold timer for this button
                m_HoldTimer["LB"] = m_HoldTimes["LB"] + Time.time;
            }
            if (m_HoldTimer["LB"] <= Time.time)
            {
                if (HandleLeftBumperHold != null)
                {
                    HandleLeftBumperHold(); 
                }
            }
        }
        if (m_GamePad.GetButton("RB"))
        {
            if (m_GamePad.GetButtonDown("RB")) // first check if the button was just pressed this frame
            {
                if (HandleRightBumper != null)
                {
                    HandleRightBumper(); 
                }
                //start the hold timer for this button
                m_HoldTimer["RB"] = m_HoldTimes["RB"] + Time.time;
            }
            if (m_HoldTimer["RB"] <= Time.time)
            {
                if (HandleRightBumperHold != null)
                {
                    HandleRightBumperHold(); 
                }
            }
        }
        #endregion

        #region Check triggers
        if (m_GamePad.GetLeftTriggerTap())
        {
            if (HandleLeftTriggerTap != null)
            {
                HandleLeftTriggerTap(); 
            }
        }
        if (m_GamePad.GetLeftTrigger() >= 0.1f)
        {
            if (HandleLeftTrigger != null)
            {
                HandleLeftTrigger(m_GamePad.GetLeftTrigger()); 
            }
        }
        if (m_GamePad.GetRightTriggerTap())
        {
            if (HandleRightTriggerTap != null)
            {
                HandleRightTriggerTap(); 
            }
        }
        if (m_GamePad.GetRightTrigger() >= 0.1f)
        {
            if (HandleRightTrigger != null)
            {
                HandleRightTrigger(m_GamePad.GetRightTrigger()); 
            }
        }
        #endregion

        #region Check joysticks
        Vector2 leftStick = new Vector2(m_GamePad.GetLeftStick().X, m_GamePad.GetLeftStick().Y);
        Vector2 rightStick = new Vector2(m_GamePad.GetRightStick().X, m_GamePad.GetRightStick().Y);
        if (HandleLeftStick != null)
        {
            if (leftStick.magnitude > Mathf.Epsilon)
            {
                HandleLeftStick(leftStick);
            }
        }
        if (HandleRightStick != null)
        {
            if (rightStick.magnitude > Mathf.Epsilon)
            {
                HandleRightStick(rightStick); 
            }
        }
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
