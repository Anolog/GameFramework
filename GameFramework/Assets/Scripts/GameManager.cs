using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Manager variables


    //Initialize managers
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
