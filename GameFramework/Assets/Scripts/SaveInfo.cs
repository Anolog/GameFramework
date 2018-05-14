using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInfo {

    private string m_SaveFile; //just using a string for now until we decide how we want this to work

    public void SetSaveInfo(string aSaveInfo) { m_SaveFile = aSaveInfo; }

    // this function will do more in the future, but for now it will just return the save info
    public string ReadSave()
    {
        return m_SaveFile;
    }
}
