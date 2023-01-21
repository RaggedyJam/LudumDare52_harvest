using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMan : MonoBehaviour
{
  public HitBox hitBox;

  public void Recover()
  {
    hitBox.BeginRecovery();
  }
}
