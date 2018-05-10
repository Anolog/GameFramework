using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is being used so that we do not get errors for now.
public struct MusicPlaylist
{
    public List<AudioClip> Songs;
}


public class MusicManager : MonoBehaviour {

    private List<MusicPlaylist> m_MusicPlaylist;

    public bool RepeatMusic;
    public bool FadeSong;
    public float FadeDuration;
    public int CurrentPlaylistInList;
    public int CurrentSongInsidePlaylist;

    [SerializeField]
    private AudioSource m_AudioSource;

    private AudioManager m_AudioManager;

    private bool m_FadeOutStarted = false; 

    //Constructor that sets the AudioManager
    public MusicManager(AudioManager aAudioManager)
    {
        m_AudioManager = aAudioManager;
    }

	// Use this for initialization
	void Start () {
        RepeatMusic = true;
        FadeSong = true;

        FadeDuration = 1f;

        CurrentPlaylistInList = 0;
        CurrentSongInsidePlaylist = 0;
	}
	
    //Stops all coroutines and calls Play Playlist
    public void PlayTracks(MusicPlaylist aMusicPlaylist)
    {
        StopAllCoroutines();

        StartCoroutine(PlayPlaylist(aMusicPlaylist));
    }

    //Plays a playlist
    private IEnumerator PlayPlaylist(MusicPlaylist aMusicPlaylist)
    {
        while (aMusicPlaylist.Songs.Count >= CurrentSongInsidePlaylist)
        {

            if (m_AudioSource.isPlaying == false || m_AudioSource == null)
            {
                PlaySong(aMusicPlaylist.Songs[CurrentSongInsidePlaylist], FadeSong);
            }

            else if (m_AudioSource.isPlaying == true)
            {
                if (FadeSong == true)
                {
                    if (m_AudioSource.time >= m_AudioSource.clip.length - FadeDuration && m_FadeOutStarted == false)
                    {
                        m_FadeOutStarted = true;
                        StartCoroutine(FadeOut());
                    }
                }

                //Check if track is done
                if (m_AudioSource.time >= m_AudioSource.clip.length - 1)
                {
                    CurrentSongInsidePlaylist++;
                }
            }

            yield return null;
        }
    }

    // stops all coroutines and shuts off the audio
    public void Stop(bool aShouldFade)
    {
        StopAllCoroutines();

        if (aShouldFade)
        {
            StartCoroutine(FadeOut());
        }
        else
        {
            m_AudioSource.Stop();
        }
    }

    // stops the song with a fade
    public void StopWithFade()
    {
        StartCoroutine(FadeOut());
    }

    // Will start to play a song with a fade if required
    public void PlaySong(AudioClip aSong, bool aShouldFade)
    {
        m_AudioSource.clip = aSong;
        m_AudioSource.Play();

        if (aShouldFade)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        if (FadeDuration >= 0.0f)
        {
            float lerpHelper = 0.0f;

            while (lerpHelper <= 1.0f && m_AudioSource.isPlaying == true)
            {
                //Uses the audio sources time as Time.Time essentially, because we are going through it, not the game time.
                lerpHelper = Mathf.InverseLerp(0.0f, FadeDuration, m_AudioSource.time);
                m_AudioSource.volume = Mathf.Lerp(0.0f, m_AudioManager.GetMusicVolume(), lerpHelper);

                //Break out of coroutine
                yield return null;
            }

            m_AudioSource.volume = m_AudioManager.GetMusicVolume();
        }
    }

    private IEnumerator FadeOut()
    {
        if (FadeDuration >= 0.0f)
        {
            float lerpHelper = 0.0f;

            while (lerpHelper <= 1.0f && m_AudioSource.isPlaying == true)
            {
                //Uses the audio sources time as Time.Time essentially, because we are going through it, not the game time.
                lerpHelper = Mathf.InverseLerp(0.0f, FadeDuration, m_AudioSource.time);
                m_AudioSource.volume = Mathf.Lerp(m_AudioManager.GetMusicVolume(), 0.0f, lerpHelper);

                //Break out of coroutine
                yield return null;
            }

            m_AudioSource.volume = 0;
        }
    }
}
