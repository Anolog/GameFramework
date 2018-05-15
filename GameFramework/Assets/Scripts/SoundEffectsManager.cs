using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour {

    [SerializeField]
    private Dictionary<string, AudioObject> m_SoundEffectsList;

    private AudioObject m_TempSoundObject;

    // plays a sound based on the name and position provided
	public void PlaySoundByName(string aSoundName, Vector3 aSoundPos)
    {
        m_TempSoundObject = m_SoundEffectsList[aSoundName];
        m_TempSoundObject.PlaySound(aSoundPos);
    }
}
