using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundPlayer : MonoBehaviour
{
    AudioSource myAudioSource;
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        myAudioSource.Play();
    }
}
