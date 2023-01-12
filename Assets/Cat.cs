using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
  float health = 100;

  //public GameObject deathFX;

  public Animator catAnimation;

  void OnTriggerEnter2D()
  {
    TakeDamage();

    catAnimation.ResetTrigger("TakeHit");
    catAnimation.SetTrigger("TakeHit");
  }

  void TakeDamage()
  {
    health -= 40;

    if (health <= 0)
      Die();
  }

  void Die()
  {
    catAnimation.ResetTrigger("Die");
    catAnimation.SetTrigger("Die");

    //Instantiate(deathFX, transform.position, Quaternion.identity);
    Destroy(gameObject, 3);
  }
}
