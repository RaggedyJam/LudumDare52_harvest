using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour
{
  public Transform sprinkle;

  Camera cam;

  public Vector3 offset;

  void Start()
  {
    cam = Camera.main;

    //Cursor.lockState = CursorLockMode.Locked;
  }

  void Update()
  {
    sprinkle.position = cam.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)) + offset;
  }
}
