using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
  float health = 100;

  bool dead;

  //public GameObject deathFX;

  public Animator catAnimation;

  void OnTriggerEnter2D()
  {
    TakeDamage();

    if (!dead)
    {
      catAnimation.ResetTrigger("TakeHit");
      catAnimation.SetTrigger("TakeHit");
    }
  }

  void TakeDamage()
  {
    health -= 40;

    if (health <= 0 && !dead)
    {
      Die();
    }
  }

  void Die()
  {
    catAnimation.SetBool("Die", true);

    //Instantiate(deathFX, transform.position, Quaternion.identity);
    Destroy(gameObject, 3);

    dead = true;
  }
}
