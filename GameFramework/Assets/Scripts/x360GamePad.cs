using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


// tutorial used for this script found here: https://lcmccauley.wordpress.com/2015/04/20/x360-input-tutorial-unity-p1/

//stores the state of a single gamePad button
public struct xButton
{
    public ButtonState PrevState;
    public ButtonState State;
}

//stores the state of a single gamePad trigger
public struct TriggerState
{
    public float PrevValue;
    public float Value;
}

public class xRumble
{
    public float Timer; //rumble timer
    public float FadeTime; //the time the rumble will start to fade out
    public Vector2 Power; //intensity

    // decrease the timer
    public void Update()
    {
        Timer -= Time.deltaTime;
    }
}

public class x360GamePad
{
    private GamePadState m_PrevState; //previous gamepad state
    private GamePadState m_State; //current gamepad state

    private int m_GamePadIndex; //numeris index (1,2,3,4)
    private PlayerIndex m_PlayerIndex; // XInput 'Player' index
    private List<xRumble> m_RumbleEvents; //stores rumble events

    //button input map
    private Dictionary<string, xButton> m_InputMap;

    // states for all buttons/inputs supported
    private xButton m_A, m_B, m_X, m_Y; //face buttons
    private xButton m_DPadUp, m_DPadDown, m_DPadLeft, m_DPadRight;

    private xButton m_Guide; //Xbox logo button
    private xButton m_Back, m_Start;
    private xButton m_L3, m_R3; //joystick buttons
    private xButton m_LB, m_RB; //bumper buttons
    private TriggerState m_LT, m_RT; //triggers

    // constructor
    public x360GamePad(int aIndex)
    {
        // Set gamepad index
        m_GamePadIndex = aIndex - 1;
        m_PlayerIndex = (PlayerIndex)m_GamePadIndex;

        // Create rumble container and inputmap
        m_RumbleEvents = new List<xRumble>();
        m_InputMap = new Dictionary<string, xButton>();
    }

    //Update gamePad state
    public void Update()
    {
        // Get the current state
        m_State = GamePad.GetState(m_PlayerIndex);

        //check gamePad is connected
        if (m_State.IsConnected)
        {
            m_A.State = m_State.Buttons.A;
            m_B.State = m_State.Buttons.B;
            m_X.State = m_State.Buttons.X;
            m_Y.State = m_State.Buttons.Y;

            m_DPadUp.State = m_State.DPad.Up;
            m_DPadDown.State = m_State.DPad.Down;
            m_DPadLeft.State = m_State.DPad.Left;
            m_DPadRight.State = m_State.DPad.Right;

            m_Guide.State = m_State.Buttons.Guide;
            m_Back.State = m_State.Buttons.Back;
            m_Start.State = m_State.Buttons.Start;
            m_L3.State = m_State.Buttons.LeftStick;
            m_R3.State = m_State.Buttons.RightStick;
            m_LB.State = m_State.Buttons.LeftShoulder;
            m_RB.State = m_State.Buttons.RightShoulder;

            // Read trigger values into trigger states
            m_LT.Value = m_State.Triggers.Left;
            m_RT.Value = m_State.Triggers.Right;

            UpdateInputMap(); // Update inputMap dictionary
            HandleRumble(); // update rumble event(s)
        }
    }

    //refresh previous gamePad state
    public void Refresh()
    {
        // This 'saves' the current state for next update
        m_PrevState = m_State;

        //check gamePad is connected
        if (m_State.IsConnected)
        {
            m_A.PrevState = m_PrevState.Buttons.A;
            m_B.PrevState = m_PrevState.Buttons.B;
            m_X.PrevState = m_PrevState.Buttons.X;
            m_Y.PrevState = m_PrevState.Buttons.Y;

            m_DPadUp.PrevState = m_PrevState.DPad.Up;
            m_DPadDown.PrevState = m_PrevState.DPad.Down;
            m_DPadLeft.PrevState = m_PrevState.DPad.Left;
            m_DPadRight.PrevState = m_PrevState.DPad.Right;

            m_Guide.PrevState = m_PrevState.Buttons.Guide;
            m_Back.PrevState = m_PrevState.Buttons.Back;
            m_Start.PrevState = m_PrevState.Buttons.Start;
            m_L3.PrevState = m_PrevState.Buttons.LeftStick;
            m_R3.PrevState = m_PrevState.Buttons.RightStick;
            m_LB.PrevState = m_PrevState.Buttons.LeftShoulder;
            m_RB.PrevState = m_PrevState.Buttons.RightShoulder;

            // Read previous trigger values into trigger states
            m_LT.PrevValue = m_PrevState.Triggers.Left;
            m_RT.PrevValue = m_PrevState.Triggers.Right;

            UpdateInputMap(); //update inputmap dictionary
        }
    }


    //return button state
    public bool GetButton(string aButton)
    {
        // for reference, the way the ? works is if the left side evaluates to true it uses the left side of the colon
        // if the left side evaulates to false it uses the right side of the colon
        return m_InputMap[aButton].State == ButtonState.Pressed ? true : false;
    }

    //returns the button state on the current frame. unlike the above function it will only return true once
    public bool GetButtonDown(string aButton)
    {
        return (m_InputMap[aButton].PrevState == ButtonState.Released &&
                m_InputMap[aButton].State == ButtonState.Pressed) ? true : false;
    }

    //return numeric gamePad index
    public int Index { get { return m_GamePadIndex; } }

    //return gamepad connection state
    public bool IsConnected { get { return m_State.IsConnected; } }

    // Update Input map
    void UpdateInputMap()
    {
        m_InputMap["A"] = m_A;
        m_InputMap["B"] = m_B;
        m_InputMap["X"] = m_X;
        m_InputMap["Y"] = m_Y;

        m_InputMap["DPadUp"] = m_DPadUp;
        m_InputMap["DPadDown"] = m_DPadDown;
        m_InputMap["DPadLeft"] = m_DPadLeft;
        m_InputMap["DPadRight"] = m_DPadRight;

        m_InputMap["Guide"] = m_Guide;
        m_InputMap["Back"] = m_Back;
        m_InputMap["Start"] = m_Start;

        //joystick buttons
        m_InputMap["L3"] = m_L3;
        m_InputMap["R3"] = m_R3;

        // bumper buttons
        m_InputMap["LB"] = m_LB;
        m_InputMap["RB"] = m_RB;
    }

    // Update and apply rumble events
    void HandleRumble()
    {
        //ignore if there are no events
        if (m_RumbleEvents.Count > 0)
        {
            Vector2 currentPower = Vector2.zero;

            for (int i = 0; i < m_RumbleEvents.Count; i++)
            {
                //update current event
                m_RumbleEvents[i].Update();

                if (m_RumbleEvents[i].Timer > 0)
                {
                    //calculate current power
                    float timeLeft = Mathf.Clamp01(m_RumbleEvents[i].Timer / m_RumbleEvents[i].FadeTime);
                    currentPower = new Vector2(Mathf.Max(m_RumbleEvents[i].Power.x * timeLeft, currentPower.x),
                                               Mathf.Max(m_RumbleEvents[i].Power.y * timeLeft, currentPower.y));

                    //apply vibration to gamepad motors
                    GamePad.SetVibration(m_PlayerIndex, currentPower.x, currentPower.y);
                }
                else
                {
                    //Remove expired events
                    m_RumbleEvents.Remove(m_RumbleEvents[i]);
                }
            }
        }
    }

    //add rumble event to the gamePad
    public void AddRumble(float aTimer, Vector2 aPower, float aFadeTime)
    {
        xRumble rumble = new xRumble();

        rumble.Timer = aTimer;
        rumble.Power = aPower;
        rumble.FadeTime = aFadeTime;

        m_RumbleEvents.Add(rumble);
    }

    //return axes of left joystick
    public GamePadThumbSticks.StickValue GetLeftStick()
    {
        return m_State.ThumbSticks.Left;
    }

    //return axes of right joystick
    public GamePadThumbSticks.StickValue GetRightStick()
    {
        return m_State.ThumbSticks.Right;
    }

    // return axes of left trigger
    public float GetLeftTrigger() { return m_State.Triggers.Left; }

    // return axes of right trigger
    public float GetRightTrigger() { return m_State.Triggers.Right; }

    // Check if the left trigger was tapped on the current frame
    public bool GetLeftTriggerTap()
    {
        return (m_LT.PrevValue == 0f && m_LT.Value >= 0.1f) ? true : false;
    }

    public bool GetRightTriggerTap()
    {
        return (m_RT.PrevValue == 0f && m_RT.Value >= 0.1f) ? true : false;
    }

}

