using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
  public bool recovering;

  public bool active;

  public bool haveHit;

  bool haveStruck;

  public int comboProgress;

  public Animator playerAnimator;

  void Update()
  {
    playerAnimator.SetBool("Recovering", recovering);
  }

  public void TryStrike()
  {
    if (!recovering)
    {
      comboProgress++;

      if (comboProgress > 3)
        comboProgress = 1;

      playerAnimator.SetInteger("ComboProgression", comboProgress);
      playerAnimator.ResetTrigger("Strike");
      playerAnimator.SetTrigger("Strike");

      haveStruck = true;
    }
  }

  public void BeginRecovery()
  {
    StartCoroutine(Recover());
  }

  IEnumerator Recover()
  {
    haveStruck = false;
    yield return new WaitForSeconds(.2f);
    if (!haveStruck)
      comboProgress = 0;
  }
}
