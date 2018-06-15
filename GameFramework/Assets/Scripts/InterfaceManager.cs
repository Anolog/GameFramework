using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour {

    private List<UserInterfaceCanvas> m_UserInterfaceList = new List<UserInterfaceCanvas>();
    private UserInterfaceCanvas m_ActiveUserInterface = new UserInterfaceCanvas();
    private bool m_IsChanging = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeUserInterfaceAndFade(UserInterfaceCanvas aUIToChangeTo)
    {
        StartCoroutine(ChangeInterface(aUIToChangeTo, true));
    }

    public void ChangeUserInterface(UserInterfaceCanvas aUIToChangeTo)
    {
        StartCoroutine(ChangeInterface(aUIToChangeTo, false));
    }


    // this will change the user interface
    private IEnumerator ChangeInterface(UserInterfaceCanvas aUIToChangeTo, bool aFade)
    {
        m_ActiveUserInterface = aUIToChangeTo;


        yield return null;
    }
}
