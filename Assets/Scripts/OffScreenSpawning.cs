using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenSpawning : MonoBehaviour
{
  public GameObject spawnObject;

  public Player player;

  void Start()
  {
    StartCoroutine(SpawnObject());
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

  IEnumerator SpawnObject()
  {
    int screenSide = GetScreenSide();

    Vector3 spawnPos = GetSpawnPosition(screenSide);

    Vector3 rotation = GetRotation(screenSide);

    yield return new WaitForSeconds(1);

    Instantiate(spawnObject, player.transform.position + spawnPos, Quaternion.Euler(rotation));

    StartCoroutine(SpawnObject());
  }
}
