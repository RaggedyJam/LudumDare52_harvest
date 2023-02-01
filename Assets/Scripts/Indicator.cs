using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
  Transform[] targets; // index 0 has most priority

  public Transform start;

  void Start()
  {
    targets = new Transform[2];

    SetTarget(start, 1);
  }

  void Update()
  {
    int index = 0;

    if (targets[0] == null && targets[1] != null)
    {
      index = 1;
    }

    if (targets[index] != null)
    {
      Vector3 dir = targets[index].position - transform.position;

      transform.up = dir;
    }
  }

  public void SetTarget(Transform newTarget, int index)
  {
    targets[index] = newTarget;
    transform.GetChild(0).gameObject.SetActive(true);
  }

  public void RemoveTarget(int index)
  {
    targets[index] = null;
  }

  public void ShowIndicator()
  {
    transform.GetChild(0).gameObject.SetActive(true);
  }

  public void HideIndicator()
  {
    // for (int i = 0; i < targets.Length; i++)
    //   targets[i] = null;

    transform.GetChild(0).gameObject.SetActive(false);
  }
}
