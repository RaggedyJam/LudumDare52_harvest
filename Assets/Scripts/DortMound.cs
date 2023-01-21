using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DortMound : MonoBehaviour
{
  float hydrationNeeded = 100;

  public float hydration = 0;

  Animator dortAnimation;

  public GameObject seed;

  void Start()
  {
    dortAnimation = GetComponent<Animator>();
  }

  void Update()
  {
    if (hydration >= hydrationNeeded)
    {
      hydration = 100;

      dortAnimation.SetBool("Hydrated", true);
    } else
    {
      dortAnimation.SetBool("Hydrated", false);
    }
  }

  void OnTriggerStay2D()
  {
    hydration += Time.deltaTime * 20;
  }
}
