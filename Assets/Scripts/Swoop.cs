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

  RandomGen randomGen;

  Indicator indicator;

  void Start()
  {
    bird = GetComponent<AudioSource>();
    music = FindObjectOfType<Music>();
    randomGen = FindObjectOfType<RandomGen>();
    indicator = FindObjectOfType<Indicator>();
    StartCoroutine(CheckSwoop());
  }

  IEnumerator CheckSwoop()
  {
    yield return new WaitForSeconds(Random.Range(30, 60));
    GiveWarning();
    indicator.SetTarget(randomGen.SpawnHayPile(), 0);
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

    indicator.RemoveTarget(0);
    bird.Play();
    music.ChangeMusic(music.normalMusic);
  }
}
