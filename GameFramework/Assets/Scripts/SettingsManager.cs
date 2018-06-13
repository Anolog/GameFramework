using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;
    //TODO: Uncomment when that class is fixed/recreated
    //private PostProcessingManager m_PostProcessingManager;

    private Resolution m_Resolution;
    private List<string> m_ResolutionNames;
    private QualitySettings m_QualitySetting;
    private List<string> m_QualityNames;
    private bool m_WindowedMode;
    private int m_AASetting;

	// Use this for initialization
	void Start ()
    {
        //Set the manager
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        /*
        if (m_PostProcessingManager == null)
        {
            m_PostProcessingManager = new PostProcessingManager();
        }
        */
        //TODO: 
        //Load from user saved info
	}
	
	public Resolution GetResolution()
    {
        return m_Resolution;
    }

    public void SetResolution(Resolution aResolution)
    {
        m_Resolution = aResolution;
    }

    public QualitySettings GetQualitySettings()
    {
        return m_QualitySetting;
    }

    public void SetQualitySettings(QualitySettings aQuality)
    {
        m_QualitySetting = aQuality;
    }

    public void SetMotionBlur(bool aMotionBlur)
    {
        //m_PostProcessingManager.SetMotionBlur(motionBlur);
    }

    public void SetWindowedMode(bool aWindowed)
    {
        m_WindowedMode = aWindowed;
    }

    public bool GetWindowedMode()
    {
        return m_WindowedMode;
    }

    public void SetAntiAliasing(int aSetting)
    {
        m_AASetting = aSetting;
    }

    public int GetAntiAliasing()
    {
        return QualitySettings.antiAliasing;
    }

    public void SetQualityLevel(int aQualityLevel)
    {
        QualitySettings.SetQualityLevel(aQualityLevel);
    }

    public int GetQualityLevel()
    {
        return QualitySettings.GetQualityLevel();
    }
}
