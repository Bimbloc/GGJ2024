using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{
    [SerializeField] private AudioSource normalRadio, helloKittyRadio;
    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();
    private List<AudioClip> audiosToPlay = new List<AudioClip>();
    private AudioSource currentRadio;
    private void Start()
    {
        GameManager.GetInstance().setRadioManager(this);
        currentRadio = normalRadio;
    }
    public enum AudioTracks
    {
        IntroDialogue,
        Puzzle1Music,
        Puzzle1Dialogue,
        Puzzle2Music,
        Puzzle2Dialogue,
        Puzzle3Music,
        Puzzle3Dialogue,
        FinalDialogue,
        CreditsMusic
    }
    private void Update()
    {
        if (!currentRadio.isPlaying&&audiosToPlay.Count>0)
        {
            currentRadio.PlayOneShot(audiosToPlay[0]);
            audiosToPlay.RemoveAt(0);
        }
    }

    public void PlayAudioTrack(AudioTracks audio) {
        if (currentRadio.isPlaying)
            audiosToPlay.Add(sounds[(int)audio]);
        else
        {
            int value = (int)audio;
            currentRadio.PlayOneShot(sounds[value]);
        }
    }

    public void BreakRadio()
    {
        currentRadio = helloKittyRadio;
    }
}
