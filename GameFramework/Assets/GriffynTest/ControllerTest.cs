using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

    ControllerManager m_ControllerManager;
    Controller m_Controller;

	// Use this for initialization
	void Start () {
        m_ControllerManager = new ControllerManager();

        m_Controller = new Controller(0);

        m_ControllerManager.AddController(m_Controller);

        // Face button tests
        m_ControllerManager.GetController(0).HandleAButton += TestAButton;
        m_ControllerManager.GetController(0).HandleAButtonHold += TestAButtonHold;
        m_ControllerManager.GetController(0).HandleBButton += TestBButton;
        m_ControllerManager.GetController(0).HandleXButton += TestXButton;
        m_ControllerManager.GetController(0).HandleYButton += TestYButton;

        // D-Pad tests
        m_ControllerManager.GetController(0).HandleDPadUp += TestDPadUp;
        m_ControllerManager.GetController(0).HandleDPadDown += TestDPadDown;
        m_ControllerManager.GetController(0).HandleDPadLeft += TestDPadLeft;
        m_ControllerManager.GetController(0).HandleDPadRight += TestDPadRight;

        // Guide, back, and start tests
        m_ControllerManager.GetController(0).HandleGuide += TestGuide;
        m_ControllerManager.GetController(0).HandleBack += TestBack;
        m_ControllerManager.GetController(0).HandleStart += TestStart;

        // JoyStick click tests
        m_ControllerManager.GetController(0).HandleLeftJoystickClick += TestLeftJoyStickClick;
        m_ControllerManager.GetController(0).HandleLeftStick += TestLeftStick;
        m_ControllerManager.GetController(0).HandleRightJoystickClick += TestRightJoyStickClick;

        // Bumper tests
        m_ControllerManager.GetController(0).HandleLeftBumper += TestLeftBumper;
        m_ControllerManager.GetController(0).HandleRightBumper += TestRightBumper;

        // Trigger tests
        m_ControllerManager.GetController(0).HandleLeftTriggerTap += TestLeftTriggerTap;
        m_ControllerManager.GetController(0).HandleLeftTrigger += TestLeftTrigger;
        m_ControllerManager.GetController(0).HandleRightTriggerTap += TestRightTriggerTap;
    }
	
	// Update is called once per frame
	void Update () {
        m_ControllerManager.HandleInput();
	}

    void TestAButton()
    {
        Debug.Log("You have pressed the A button");
    }
    void TestAButtonHold()
    {
        Debug.Log("You are holding the A button");
    }
    void TestBButton()
    {
        Debug.Log("You have pressed the B button");
    }
    void TestXButton()
    {
        Debug.Log("You have pressed the X button");
    }
    void TestYButton()
    {
        Debug.Log("You have pressed the Y button");
    }
    void TestDPadUp()
    {
        Debug.Log("You have pressed D-Pad Up");
    }
    void TestDPadDown()
    {
        Debug.Log("You have pressed D-Pad Down");
    }
    void TestDPadLeft()
    {
        Debug.Log("You have pressed D-Pad Left");
    }
    void TestDPadRight()
    {
        Debug.Log("You have pressed D-Pad Right");
    }
    void TestLeftBumper()
    {
        Debug.Log("You have pressed the left bumper");
    }
    void TestRightBumper()
    {
        Debug.Log("You have pressed the right bumper");
    }
    void TestLeftTriggerTap()
    {
        Debug.Log("You have tapped the left trigger");
    }
    void TestRightTriggerTap()
    {
        Debug.Log("You have tapped the right trigger");
    }
    void TestLeftJoyStickClick()
    {
        Debug.Log("You have clicked the left stick");
    }
    void TestRightJoyStickClick()
    {
        Debug.Log("You have clicked the right stick");
    }
    void TestStart()
    {
        Debug.Log("You have pressed the start button");
    }
    void TestBack()
    {
        Debug.Log("You have pressed the back button");
    }
    void TestGuide()
    {
        Debug.Log("You have pressed the guide button");
    }
    void TestLeftTrigger(float value)
    {
        Debug.Log("You have pressed the left trigger down for " + value.ToString());
    }
    void TestLeftStick(Vector2 value)
    {
        Debug.Log("You have moved the left stick to " + value.ToString());
    }
}
