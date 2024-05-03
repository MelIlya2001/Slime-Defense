using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public AudioClip[] sounds;

    public AudioMixer mixer;
    private AudioSource audioSource => GetComponent<AudioSource>();


    public void PlaySound (AudioClip clip, float volume = 1f, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        audioSource.pitch = Random.Range(p1, p2);

        if (destroyed)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }

        else audioSource.PlayOneShot(clip, volume);
    } 
}
