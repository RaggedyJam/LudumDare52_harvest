using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen : MonoBehaviour
{
  public Transform player;

  Vector3 lastPos;

  public GameObject goodie;
  public GameObject HayPile;
  public GameObject hazard;

  void Start()
  {
    lastPos = player.position;

    StartCoroutine(CheckDst());
  }

  IEnumerator CheckDst()
  {
    yield return new WaitForSeconds(3);

    if ((player.transform.position - lastPos).magnitude > 20)
    {
      //print("spawn thing");
      StartCoroutine(GenerateThings());
      lastPos = player.position;
    }

    StartCoroutine(CheckDst());
  }

  IEnumerator GenerateThings()
  {
    int spawns = 0;
    int attempts = 0;

    List<Vector3> spawnPoints = new List<Vector3>();

    Vector3 targetPos;

    while (spawns < 20 || attempts >= 100)
    {

      bool invalidSpawn = false;

      float angle = Mathf.Deg2Rad * Random.Range(0, 360);

      Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Random.Range(20, 30);

      targetPos = player.position + pos;

      for (int i = 0; i < spawnPoints.Count; i++)
      {
        if ((targetPos - spawnPoints[i]).sqrMagnitude < 9)
        {
          invalidSpawn = true;
          break;
        }
      }

      if (!invalidSpawn)
      {
        spawnPoints.Add(targetPos);
        Instantiate(goodie, targetPos, Quaternion.identity, transform);
        spawns++;
      }

      attempts++;
      yield return null;
    }
  }
}
