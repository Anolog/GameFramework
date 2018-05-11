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

    public bool RepeatMusic;
    public bool FadeSong;
    public float FadeDuration;
    public int CurrentPlaylistInList;
    public int CurrentSongInsidePlaylist;

    [SerializeField]
    private AudioSource m_AudioSource;

    public AudioManager AudioManager;

    private bool m_FadeOutStarted = false;


    // Use this for initialization
    void Start()
    {
        m_MusicPlaylist = new List<MusicPlaylist>();

        RepeatMusic = true;
        FadeSong = true;

        FadeDuration = 5f;

        CurrentPlaylistInList = 0;
        CurrentSongInsidePlaylist = 0;

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

        CurrentPlaylistInList = 0;
        StartCoroutine(PlayPlaylist(aMusicPlaylist));
    }

    //TODO: Make sure this works
    //Plays a playlist
    private IEnumerator PlayPlaylist(MusicPlaylist aMusicPlaylist)
    {
        while (aMusicPlaylist.Songs.Count >= CurrentSongInsidePlaylist)
        {
            if (m_AudioSource.isPlaying == false || m_AudioSource == null)
            {
                //Perhaps do this? vvvv
                //yield return StartCoroutine(PlaySong(aMusicPlaylist.Songs[CurrentSongInsidePlaylist], FadeSong);
                //Then once it is done on the coroutine, it will increment CurrentSongInsidePlaylist++
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
                    m_FadeOutStarted = false;
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
                m_AudioSource.volume = Mathf.Lerp(0.0f, AudioManager.GetMusicVolume(), lerpHelper);

                //Break out of coroutine
                yield return null;
            }

            m_AudioSource.volume = AudioManager.GetMusicVolume();
        }
    }

    //TODO: Make sure this works
    private IEnumerator FadeOut()
    {
        if (FadeDuration >= 0.0f)
        {
            float lerpHelper = FadeDuration;

            while (lerpHelper <= FadeDuration && m_AudioSource.isPlaying == true)
            {
                lerpHelper -= Time.deltaTime * FadeDuration;
                float temp = Mathf.Lerp(0.0f, AudioManager.GetMusicVolume(), lerpHelper);

                m_AudioSource.volume = temp;

                if (m_AudioSource.volume <= 0.1f)
                {
                    m_AudioSource.Stop();
                    yield break;
                }

                yield return null;
            }


        }
    }
}
