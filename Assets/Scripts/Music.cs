using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
  AudioSource music;

  public AudioClip normalMusic;
  public AudioClip swoopMusic;

  bool done;

  void Start()
  {
    music = GetComponent<AudioSource>();
  }

  public void ChangeMusic(AudioClip newClip)
  {
    if (!done)
    {
      music.clip = newClip;
      music.Play();
    }
  }

  public void StopMusic()
  {
    music.Stop();
    done = true;
  }
}
