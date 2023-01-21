using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestObject : MonoBehaviour
{
  Player player;

  public bool harvesting;

  float harvestYield = 100;

  public Animator harvestAnimator;

  public AudioSource pickup;

  bool dead;

  void Start()
  {
    player = FindObjectOfType<Player>();
  }

  void Update()
  {
    if (harvesting)
    {
      harvestYield -= Time.deltaTime * 100;
    }

    if (harvestYield <= 0 && !dead)
      FinishHarvest();
  }

  void FinishHarvest()
  {
    dead = true;
    player.CompleteHarvest();
    harvestAnimator.SetTrigger("AddItem");
    transform.GetChild(0).gameObject.layer = 1;

    pickup.Play();

    Destroy(gameObject, 2);
  }

  void OnTriggerEnter()
  {

  }
}
