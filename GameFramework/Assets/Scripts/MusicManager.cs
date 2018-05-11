using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// This is being used so that we do not get errors for now.
public struct MusicPlaylist
{
    public List<AudioClip> Songs;
}


public class MusicManager : MonoBehaviour
{

    private List<MusicPlaylist> m_MusicPlaylist;

    private bool m_RepeatMusic;
    private bool m_FadeSong;
    private float m_FadeDuration;
    private int m_CurrentPlaylistInList;
    private int m_CurrentSongInsidePlaylist;

    [SerializeField]
    private AudioSource m_AudioSource;

    private AudioManager m_AudioManager;

    private bool m_FadeOutStarted = false;

    //Getters and Setters
    public bool GetRepeatMusic() { return m_RepeatMusic; }
    public void SetRepeatMusic(bool aRepeatMusic) { m_RepeatMusic = aRepeatMusic; }
    public bool GetFadeSong() { return m_FadeSong; }
    public void SetFadeSong(bool aFadeSong) { m_FadeSong = aFadeSong; }
    public float GetFadeDuration() { return m_FadeDuration; }
    public void SetFadeDuration(float aFadeDuration) { m_FadeDuration = aFadeDuration; }
    public int GetCurrentPlaylistIndex() { return m_CurrentPlaylistInList; }
    public void SetCurrentPlaylistIndex(int aIndex) { m_CurrentPlaylistInList = aIndex; }
    public int GetCurrentSongIndex() { return m_CurrentSongInsidePlaylist; }
    public void SetCurrentSongIndex(int aIndex) { m_CurrentSongInsidePlaylist = aIndex; }
    public AudioManager GetAudioManager() { return m_AudioManager; }
    public void SetAudioManager(AudioManager aAudioManager) { m_AudioManager = aAudioManager; }


    // Use this for initialization
    public void Start()
    {
        m_MusicPlaylist = new List<MusicPlaylist>();

        m_RepeatMusic = true;
        m_FadeSong = true;

        m_FadeDuration = 5f;

        m_CurrentPlaylistInList = 0;
        m_CurrentSongInsidePlaylist = 0;

    }

    public void AddPlaylist(MusicPlaylist aPlaylist)
    {
            m_MusicPlaylist.Add(aPlaylist);
    }

    public List<MusicPlaylist> GetMusicPlaylists() { return m_MusicPlaylist; }

    //Stops all coroutines and calls Play Playlist
    public void PlayTracks(MusicPlaylist aMusicPlaylist)
    {
        StopAllCoroutines();

        m_CurrentPlaylistInList = 0;
        StartCoroutine(PlayPlaylist(aMusicPlaylist));
    }

    //TODO: Make sure this works
    //Plays a playlist
    private IEnumerator PlayPlaylist(MusicPlaylist aMusicPlaylist)
    {
        int counter = m_CurrentSongInsidePlaylist;

        while (aMusicPlaylist.Songs.Count >= counter)
        {
            if (m_AudioSource.isPlaying == false || m_AudioSource == null)
            {
                //Perhaps do this? vvvv
                //yield return StartCoroutine(PlaySong(aMusicPlaylist.Songs[CurrentSongInsidePlaylist], FadeSong);
                //Then once it is done on the coroutine, it will increment CurrentSongInsidePlaylist++
                PlaySong(aMusicPlaylist.Songs[counter], m_FadeSong);
                counter++;
            }

            else if (m_AudioSource.isPlaying == true)
            {
                if (m_FadeSong == true)
                {
                    if (m_AudioSource.time >= m_AudioSource.clip.length - m_FadeDuration && m_FadeOutStarted == false)
                    {
                        m_FadeOutStarted = true;
                        StartCoroutine(FadeOut());
                    }
                }

                //Check if track is done
                if (m_AudioSource.time >= m_AudioSource.clip.length - 1)
                {
                    m_CurrentSongInsidePlaylist = counter;
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

    //Make this coroutine, have another PlaySong override that is just a function that calls the coroutine
    //Takes in a song. That way, it does it's own thing and doesn't need to go into this function
    // Will start to play a song with a fade if required
    public void PlaySong(AudioClip aSong, bool aShouldFade)
    {
        StartCoroutine(PlaySongCoroutine(aSong, aShouldFade));
    }

    private IEnumerator PlaySongCoroutine(AudioClip aSong, bool aShouldFade)
    {
        m_AudioSource.clip = aSong;
        m_AudioSource.Play();

        //m_CurrentSongInsidePlaylist++;

        if (aShouldFade)
        {
            StartCoroutine(FadeIn());
        }

        yield return null;
    }

    private IEnumerator FadeIn()
    {
        if (m_FadeDuration >= 0.0f)
        {
            float lerpHelper = 0.0f;

            while (lerpHelper <= 1.0f && m_AudioSource.isPlaying == true)
            {
                //Uses the audio sources time as Time.Time essentially, because we are going through it, not the game time.
                lerpHelper = Mathf.InverseLerp(0.0f, m_FadeDuration, m_AudioSource.time);
                m_AudioSource.volume = Mathf.Lerp(0.0f, m_AudioManager.GetMusicVolume(), lerpHelper);

                //Break out of coroutine
                yield return null;
            }

            m_AudioSource.volume = m_AudioManager.GetMusicVolume();
        }
    }

    //TODO: This appears to not be taking into account the proper fade duration
    private IEnumerator FadeOut()
    {
        if (m_FadeDuration >= 0.0f)
        {
            float lerpHelper = m_FadeDuration;

            while (lerpHelper <= m_FadeDuration && m_AudioSource.isPlaying == true)
            {
                lerpHelper -= Time.deltaTime * m_FadeDuration;
                float temp = Mathf.Lerp(0.0f, m_AudioManager.GetMusicVolume(), lerpHelper);

                m_AudioSource.volume = temp;

                if (m_AudioSource.volume <= 0.1f)
                {
                    m_AudioSource.Stop();
                    m_FadeOutStarted = false;
                    yield break;
                }

                yield return null;
            }


        }
    }
}
