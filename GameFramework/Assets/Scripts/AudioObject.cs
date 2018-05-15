using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject  {

    private AudioSource m_AudioSource;
    private GameObject m_AudioGameObject;
    private Transform m_AudioTransform;
    private AudioClip m_AudioClip;
    private string m_AudioName;

    // initialize the audio source, clip and volume
    public AudioObject(AudioSource aAudioSource, AudioClip aAudioClip, string aClipName, float aVolume)
    {
        m_AudioSource = aAudioSource;
        m_AudioClip = aAudioClip;
        m_AudioName = aClipName;
        m_AudioSource.volume = aVolume;
    }

    public void PlaySound(Vector3 aPos)
    {
        m_AudioSource.transform.position = aPos;
        m_AudioSource.PlayOneShot(m_AudioClip);
    }
}
