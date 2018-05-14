

public class SaveManager {

    //Variables
    private SaveInfo[] m_SaveSlots = new SaveInfo[m_AmountOfSaveSlots];
    private const int m_AmountOfSaveSlots = 10; // this number can be changed
    private int m_CurrentSave;
    private SaveInfo m_CurrentSaveInfo;
    private string m_SaveDefaultFilePath;
    private string m_SaveCustomFilePath;
    private string m_CurrentSaveFilePath;

    //Methods
    public SaveInfo[] GetSaveSlots()
    {
        return m_SaveSlots;
    }

    //Will save the current save to the desired save slot
    public void SaveCurrentSlot(int aSaveSlot)
    {

    }

    //takes index of save slot and erases the data in it
    public void EraseSaveSlot(int aSaveToDelete)
    {
        if (aSaveToDelete < m_AmountOfSaveSlots)
        {
            m_SaveSlots[aSaveToDelete] = null;
        }
        else
        {
            throw new System.Exception("Save slot you are trying to delete does not exist");
        }
    }

    public void EraseAllSaveSlots()
    {
        for (int i = 0; i < m_AmountOfSaveSlots; ++i)
        {
            EraseSaveSlot(i);
        }
    }

    public void DuplicateSaveSlot(int aSaveSlotToDuplicate, int aOtherSaveSlot)
    {
        // this function will likely have to do more once the save info is completed
        m_SaveSlots[aOtherSaveSlot] = m_SaveSlots[aSaveSlotToDuplicate];
    }

    public int GetAmountOfSaves()
    {
        int counter = 0;
        for (int i = 0; i < m_AmountOfSaveSlots; ++i)
        {
            if (m_SaveSlots[i] != null)
            {
                counter++;
            }
        }

        return counter;
    }

    // This will load the save information into the game
    // Can be overwritten for game specific information
    // Currently doesn't do anything
    public virtual void LoadSaveSlot()
    {

    }

    // this will send the save slots to the database
    // currently does nothing
    public void UploadSaveSlotsToDatabase()
    {

    }

    // will get save info from the database and makes it equal to save slots
    public void RecoverSaveSlotsFromDatabase()
    {

    }

    // Changes the current save file path to the one passed in
    public void ChangeSaveDirectory(string aNewSaveFilePath)
    {
        m_CurrentSaveFilePath = aNewSaveFilePath;
    }

    // Resets the save file path to default
    public void ResetSaveFilePath()
    {
        m_CurrentSaveFilePath = m_SaveDefaultFilePath;
    }
}
