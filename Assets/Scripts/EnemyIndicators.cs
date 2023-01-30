using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicators : MonoBehaviour
{
  Transform player;

  // lr = left/right, rf = roof/floor
  Transform lrIndicators;
  Transform rfIndicators;

  void Start()
  {
    player = FindObjectOfType<Player>().transform;
    lrIndicators = transform.GetChild(0);
    rfIndicators = transform.GetChild(1);
  }

  void Update()
  {
    lrIndicators.position = new Vector3(player.position.x, lrIndicators.position.y, 0);
    rfIndicators.position = new Vector3(rfIndicators.position.x, player.position.y, 0);
  }
}
