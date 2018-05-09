
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager {

    private List<Controller> m_ListOfControllers = new List<Controller>();

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
            controller.HandleInput();
        }
    }
}
