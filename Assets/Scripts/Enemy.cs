using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  Player player;

  public Animator enemyAnimation;

  void Start()
  {
    player = FindObjectOfType<Player>();
  }

  void Update()
  {
    Vector3 dsp = player.transform.position - transform.position;

    if (dsp.sqrMagnitude >= 50 * 50)
    {
      Destroy(gameObject);
    }

    transform.position += transform.up * Time.deltaTime * 40;
  }

  void OnTriggerEnter2D()
  {
    if (!player.snatched && !player.hiding)
    {
      player.SnatchPlayer();
      enemyAnimation.SetTrigger("PitchFork");
    }
  }
}
