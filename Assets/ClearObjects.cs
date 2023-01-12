using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObjects : MonoBehaviour
{
  Player player;

  Vector3 spawnPos;

  float dst = 36;

  void Start()
  {
    player = FindObjectOfType<Player>();

    spawnPos = transform.position;

    StartCoroutine(CheckDst());
  }

  IEnumerator CheckDst()
  {
    yield return new WaitForSeconds(3);

    if ((player.transform.position - spawnPos).sqrMagnitude > dst * dst)
    {
      Destroy(gameObject);
    }

    StartCoroutine(CheckDst());
  }
}
