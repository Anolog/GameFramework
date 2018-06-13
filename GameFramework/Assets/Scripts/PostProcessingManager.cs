using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This entire class will be reworked to specifically handle the post processing stack
//Things like AA are in the settings.
public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance;
   
	// Use this for initialization
	void Start ()
    {
		if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
	}
	
    

}
