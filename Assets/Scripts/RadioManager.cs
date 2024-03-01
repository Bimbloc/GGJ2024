using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RadioManager : MonoBehaviour
{
    [SerializeField] private AudioSource normalRadio, helloKittyRadio;
    [SerializeField] private List<AudioClip> sounds = new List<AudioClip>();
    private List<Tuple<AudioClip,bool>> audiosToPlay = new List<Tuple<AudioClip, bool>>();
    private AudioSource currentRadio;
    private AudioTracks currentTrack; public enum AudioTracks
    {
        IntroDialogue,
        Puzzle1Music,
        Puzzle2Dialogue,
        Puzzle2Music,
        Puzzle3Dialogue,
        Puzzle3Music,
        Dialogue4,
        CreditsMusic,
        Interference,
        FinalDialogue,
        Silence,
    }
    public bool radioPlaying() { return currentRadio.isPlaying; }
    public bool finalAudioPlaying() { return AudioTracks.FinalDialogue == currentTrack; }

    private void Start()
    {
        GameManager.GetInstance().setRadioManager(this);
        currentRadio = normalRadio;
        audiosToPlay.Add(new Tuple<AudioClip,bool> (sounds[(int)AudioTracks.Silence],false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.IntroDialogue], false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Interference], false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Puzzle1Music], true));
    }
    
    private void Update()
    {
        if (!currentRadio.isPlaying&&audiosToPlay.Count>0)
        {
            if (audiosToPlay[0].Item2)
                currentRadio.PlayOneShot(audiosToPlay[0].Item1);
            else
            {
                currentRadio.loop = true;
                currentRadio.PlayOneShot(audiosToPlay[0].Item1);
            }
            audiosToPlay.RemoveAt(0);
        }
    }

    //public void PlayAudioTrack(AudioTracks audio) {
    //    if (currentRadio.isPlaying)
    //        audiosToPlay.Add(sounds[(int)audio]);
    //    else
    //    {
    //        currentTrack = audio; 
    //        currentRadio.PlayOneShot(sounds[(int)audio]);
    //    }
    //}

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
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Puzzle2Dialogue], false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Interference], false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Puzzle2Music], true));
    }
    public void SetPuzzle3()
    {
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Puzzle3Dialogue], false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Interference], false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Dialogue4], false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Interference], false));
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.Puzzle3Music], true));
    }

    public void EndGameConversation()
    {
        audiosToPlay.Add(new Tuple<AudioClip, bool>(sounds[(int)AudioTracks.FinalDialogue], false));
    }
}
