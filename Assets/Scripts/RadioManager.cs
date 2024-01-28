using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
        audiosToPlay.Add(sounds[(int)AudioTracks.Interference]);
        audiosToPlay.Add(sounds[(int)AudioTracks.Puzzle1Music]);
    }
    public enum AudioTracks
    {
        IntroDialogue,
        Puzzle1Music,
        Puzzle2Dialogue,
        Puzzle2Music,
        Puzzle3Dialogue,
        Puzzle3Music,
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
        audiosToPlay.Clear();
        currentRadio.Stop();
        currentRadio.PlayOneShot(sounds[(int)AudioTracks.Interference]);
    }

    public void BreakRadio()
    {
        currentRadio = helloKittyRadio;
    }

    public void SetPuzzle2()
    {
        audiosToPlay.Add(sounds[(int)AudioTracks.Puzzle2Dialogue]);
        audiosToPlay.Add(sounds[(int)AudioTracks.Interference]);
        audiosToPlay.Add(sounds[(int)AudioTracks.Puzzle2Music]);
    }
    public void SetPuzzle3()
    {
        audiosToPlay.Add(sounds[(int)AudioTracks.Puzzle3Dialogue]);
        audiosToPlay.Add(sounds[(int)AudioTracks.Interference]);
        audiosToPlay.Add(sounds[(int)AudioTracks.Puzzle3Music]);
    }
}
