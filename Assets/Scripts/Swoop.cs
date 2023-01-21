using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swoop : MonoBehaviour
{
  public Player player;

  public GameObject warning;

  public Animator swoopAnimation;

  AudioSource bird;

  Music music;

  void Start()
  {
    bird = GetComponent<AudioSource>();
    music = FindObjectOfType<Music>();
    StartCoroutine(CheckSwoop());
  }

  IEnumerator CheckSwoop()
  {
    yield return new WaitForSeconds(Random.Range(30, 60));
    GiveWarning();
    yield return new WaitForSeconds(10);
    warning.SetActive(false);
    swoopAnimation.ResetTrigger("Swoop");
    swoopAnimation.SetTrigger("Swoop");

    StartCoroutine(CheckSwoop());
  }

  public void GiveWarning()
  {
    warning.SetActive(true);
    music.ChangeMusic(music.swoopMusic);
  }

  public void TrySnatchPlayer()
  {
    if (!player.hiding)
    {
      player.SnatchPlayer();
    }

    bird.Play();
    music.ChangeMusic(music.normalMusic);
  }
}
