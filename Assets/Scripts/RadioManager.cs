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
        audiosToPlay.Add(sounds[(int)AudioTracks.IntroDialogue]);
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
        CreditsMusic,
        Interference
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
            currentRadio.PlayOneShot(sounds[(int)audio]);
        }
    }

    public void IntereferenceSkip()
    {
        currentRadio.Stop();
        currentRadio.PlayOneShot(sounds[(int)AudioTracks.Interference]);
    }

    public void BreakRadio()
    {
        currentRadio = helloKittyRadio;
    }
}
