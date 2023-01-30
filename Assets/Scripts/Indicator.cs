using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
  public Transform target;

  void Update()
  {
    if (target != null)
    {
      Vector3 dir = target.position - transform.position;

      transform.up = dir;
    }
  }

  public void SetTarget(Transform newTarget)
  {
    target = newTarget;
    transform.GetChild(0).gameObject.SetActive(true);
  }

  public void HideIndicator()
  {
    target = null;
    transform.GetChild(0).gameObject.SetActive(false);
  }
}
