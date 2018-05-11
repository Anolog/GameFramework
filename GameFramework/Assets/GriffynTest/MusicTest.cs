using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTest : MonoBehaviour
{

    AudioManager m_AudioManager;

    [SerializeField]
    AudioClip[] m_Clips;

    MusicPlaylist m_Playlist;
	// Use this for initialization
	void Start ()
    {
        m_AudioManager = GetComponent<AudioManager>();
        m_AudioManager.Start();

        m_Playlist = new MusicPlaylist();
        m_Playlist.Songs = new List<AudioClip>();
        m_Playlist.Songs.Add(m_Clips[0]);
        m_Playlist.Songs.Add(m_Clips[1]);
    
        m_AudioManager.SetMusicVolume(1f);
        m_AudioManager.GetMusicManager().AddPlaylist(m_Playlist);
        m_AudioManager.GetMusicManager().PlayTracks(m_AudioManager.GetMusicManager().GetMusicPlaylists()[0]);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
