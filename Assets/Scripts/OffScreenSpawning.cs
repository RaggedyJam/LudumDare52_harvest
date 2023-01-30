using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenSpawning : MonoBehaviour
{
  // lr = left/right, rf = roof/floor
  public Transform lrIndicators;
  public Transform rfIndicators;

  public GameObject spawnObject;
  public GameObject warning;

  public Player player;

  void Start()
  {
    StartCoroutine(SpawnLoop());
  }

  // start at the top side and going clockwise 0 = top, 1 = right
  int GetScreenSide()
  {
    return Random.Range(0, 4);
  }

  Vector3 GetSpawnPosition(int screenSide)
  {
    Vector3 spawnPos;

    if (screenSide < 1)
    {
      spawnPos = Vector3.up * 6;

      spawnPos += Vector3.right * Random.Range(-6, 6);
    } else if (screenSide < 2)
    {
      spawnPos = Vector3.right * 10;

      spawnPos += Vector3.up * Random.Range(-4, 4);
    } else if (screenSide < 3)
    {
      spawnPos = Vector3.up * -6;

      spawnPos += Vector3.right * Random.Range(-6, 6);
    } else
    {
      spawnPos = Vector3.right * -10;

      spawnPos += Vector3.up * Random.Range(-4, 4);
    }

    return spawnPos;
  }

  Vector3 GetRotation(int screenSide)
  {
    Vector3 rotation;

    if (screenSide < 1)
    {
      rotation = Vector3.forward * 180;
    } else if (screenSide < 2)
    {
      rotation = Vector3.forward * 90;
    } else if (screenSide < 3)
    {
      rotation = Vector3.zero;
    } else
    {
      rotation = Vector3.forward * 270;
    }

    return rotation;
  }

  IEnumerator SpawnLoop()
  {
    yield return new WaitForSeconds(.5f);
    StartCoroutine(SpawnObject());
    StartCoroutine(SpawnLoop());
  }

  IEnumerator SpawnObject()
  {
    int screenSide = GetScreenSide();

    Vector3 spawnPos = GetSpawnPosition(screenSide);

    Vector3 rotation = GetRotation(screenSide);

    Transform newTransform = null;

    if (screenSide % 2 == 0)
      newTransform = Instantiate(warning, player.transform.position + spawnPos, Quaternion.Euler(rotation), rfIndicators).transform;
    else
      newTransform = Instantiate(warning, player.transform.position + spawnPos, Quaternion.Euler(rotation), lrIndicators).transform;

    yield return new WaitForSeconds(1);

    Instantiate(spawnObject, newTransform.position, Quaternion.Euler(rotation), transform);
  }
}
