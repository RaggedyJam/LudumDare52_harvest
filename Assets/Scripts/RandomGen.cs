using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen : MonoBehaviour
{
  public bool singleObjects;

  public Transform player;

  Vector3 lastPos;

  public GameObject goodie;
  public GameObject hayPile;
  public GameObject hazard;

  void Start()
  {
    if (!singleObjects)
    {
      lastPos = player.position;

      StartCoroutine(CheckDst());
    }
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
        int spawnRoll = Random.Range(0, 100);

        GameObject spawnObject = null;

        if (spawnRoll <= 75)
        {
          spawnObject = goodie;
        } else if (spawnRoll <= 100)
        {
          spawnObject = hayPile;
        }

        Instantiate(spawnObject, targetPos, Quaternion.identity, transform);
        spawns++;
      }

      attempts++;
      yield return null;
    }
  }

  public Transform SpawnGoodie()
  {
    if (singleObjects)
    {
      return SpawnThing(goodie);
    } else
    {
      return null;
    }
  }

  public Transform SpawnHayPile()
  {
    if (singleObjects)
    {
      return SpawnThing(hayPile);
    } else
    {
      return null;
    }
  }

  Transform SpawnThing(GameObject spawnObject)
  {
    Vector3 targetPos;

    float angle = Mathf.Deg2Rad * Random.Range(0, 360);

    Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Random.Range(20, 30);

    targetPos = player.position + pos;

    GameObject newObject = Instantiate(spawnObject, targetPos, Quaternion.identity, transform);

    return newObject.transform;
  }
}
