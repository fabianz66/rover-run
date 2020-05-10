using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //Clip to play at start
    public AudioClip _engineStartClip;

    //Clip to play when car is moving
    public AudioClip _engineLoopClip;

    //Audio Source to Play Sounds
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.loop = true;
        StartCoroutine(PlayEngineSound());
    }

    IEnumerator PlayEngineSound()
    {
        _audioSource.clip = _engineStartClip;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
        _audioSource.clip = _engineLoopClip;
        _audioSource.Play();
    }
}
