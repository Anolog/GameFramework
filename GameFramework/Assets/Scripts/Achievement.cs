using System.Collections;
using System.Collections.Generic;
using System;

// <T> is the variable type you want to be tracked, i.e., bool, int, float...
public class Achievement
{
    //Achievement variables
    private string m_AchievementName;
    private string m_AchievementDescription;
    private int m_AchievementPoints;
    private bool m_AchievementEarned;
    private DateTime m_TimeAchievementEarned;

    private object m_CurrentAchievementProgress;
    private object m_RequiredToEarnAchievement;

    Achievement(string aAchievementName, string aAchievementDescription, int aAchievementPoints, object aRequiredToEarnAchievement)
    {
        m_AchievementName = aAchievementName;
        m_AchievementDescription = aAchievementDescription;
        m_AchievementPoints = aAchievementPoints;
        m_AchievementEarned = false;
        m_RequiredToEarnAchievement = aRequiredToEarnAchievement;

        //Not sure if this will work, but 0 is false, 0 is an int, but 0.0 for float as well...?
        m_CurrentAchievementProgress = 0;
        
    }

    //Getters for the information for the achievements

    public string GetAchievementName()
    {
        return m_AchievementName;
    }

    public string GetAchievementDescription()
    {
        return m_AchievementDescription;
    }

    public int GetAchievementPoints()
    {
        return m_AchievementPoints;
    }

    public bool GetAchievementEarned()
    {
        return m_AchievementEarned;
    }

    public DateTime GetTimeAchievementEarned()
    {
        return m_TimeAchievementEarned;
    }

    public object GetRequiredToEarnAchievement()
    {
        return m_RequiredToEarnAchievement;
    }

    public object GetCurrentProgress()
    {
        return m_CurrentAchievementProgress;
    }
    
    //Needs a setter cause it will update semi-regularly
    public void SetCurrentProgress(object aCurrentAchievementProgress)
    {
        m_CurrentAchievementProgress = aCurrentAchievementProgress;
    }
}
