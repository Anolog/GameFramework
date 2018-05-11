using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static MusicManager Instance;
    private SoundEffectsManager m_SoundEffectsManager;

    // These will be obtained from the player prefs
    private float m_MusicVolume;
    private float m_SoundEffectsVolume;

	public void Start ()
    {
        // Set the Audio stuff
        Instance = GetComponent<MusicManager>();
        Instance.AudioManager = this;
	}

    public MusicManager GetMusicManager() { return Instance; }
    public SoundEffectsManager GetSoundEffectsManager() { return m_SoundEffectsManager; }

    public void SetMusicVolume(float aVolume) { m_MusicVolume = aVolume; }
    public float GetMusicVolume() { return m_MusicVolume; }
    public void SetSoundEffectsVolume(float aVolume) { m_SoundEffectsVolume = aVolume; }
}
