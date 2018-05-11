using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [SerializeField]
    private List<Achievement> m_ListOfAchievements;

	// Use this for initialization
	void Start ()
    {
		if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AchievementEarnedInterface(Achievement aAchievement, bool aFadeInterface)
    {
        //Call interface manager functions
    }

    public void AddAchievementToList(Achievement aAchievementToAdd)
    {
        m_ListOfAchievements.Add(aAchievementToAdd);
    }

    public List<Achievement> GetListOfAchievements()
    {
        return m_ListOfAchievements;
    }

    public float CheckAchievementStatus(int aIndex)
    {
        //Will need to test if this works vvvvvvvv
        //if (m_ListOfAchievements[aIndex].GetCurrentProgress().GetType() == typeof(int))
        try
        {
            return ( (float) (m_ListOfAchievements[aIndex].GetCurrentProgress() ) ) / 
                   ( (float) (m_ListOfAchievements[aIndex].GetRequiredToEarnAchievement() ) );
        }

        catch(IndexOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException("Index " + aIndex + " is out of range\n" + e.Message);
        }
    }

    public float CheckAchievementStatus(string aAchievementName)
    {
        Achievement rangeCheck = null;
        float returnValue = -1;
        
        //Not sure if this will work...

        try
        {
            foreach (Achievement achievement in m_ListOfAchievements)
            {
                if (achievement.GetAchievementName() == aAchievementName)
                {
                    rangeCheck = achievement;

                    returnValue = ((float)(achievement.GetCurrentProgress())) /
                                  ((float)(achievement.GetRequiredToEarnAchievement()));
                    break;
                }
            }

            return returnValue;
        }

        catch (ArgumentNullException e)
        {
            throw new ArgumentNullException("Achievement Name: " + aAchievementName + " was not found in m_ListOfAchievements\n" + e.Message);
        }
    }

    public Achievement GetAchievementFromList(int aIndex)
    {
        try
        {
            return m_ListOfAchievements[aIndex];
        }

        catch (IndexOutOfRangeException e)
        {
            throw new ArgumentOutOfRangeException("Index " + aIndex + " is bigger than m_ListOfAchievements.Count " + m_ListOfAchievements.Count + "\n" + e.Message);
        }

    }

    public Achievement GetAchievementFromList(string aAchievementName)
    {
        Achievement returnValue = null;

        try
        {
            foreach (Achievement achievement in m_ListOfAchievements)
            {
                if (achievement.GetAchievementName() == aAchievementName)
                {
                    returnValue = achievement;
                    break;
                    
                }
            }

            return returnValue;

        }

        catch(ArgumentNullException e)
        {
            throw new ArgumentNullException("Achievement Name: " + aAchievementName + " was not found in m_ListOfAchievements\n" + e.Message);
        }
    }
}
