using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarning : MonoBehaviour
{
  public GameObject enemy;

  void Start()
  {
    StartCoroutine(SpawnEnemy());
  }

  IEnumerator SpawnEnemy()
  {
    yield return new WaitForSeconds(1);

    Instantiate(enemy, transform.position, transform.rotation, transform.parent);

    Destroy(gameObject);
  }
}
