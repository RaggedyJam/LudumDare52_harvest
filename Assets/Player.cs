using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

  bool lookLeft;
  public bool harvesting;
  public bool hiding;

  int harvestLayer = 1 << 10;
  int hideLayer = 1 << 11;

  int goodieCount;

  public Animator playerAnimation;

  HarvestObject currentHarvest;

  public Transform gfxTransform;

  Animator hayPile;

  public Text goodieText;

  public HitBox hitBox;

  void Update()
  {
    if (!harvesting && !hiding)
    {
      Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

      if (Input.GetAxisRaw("Horizontal") != 0)
      {
        gfxTransform.eulerAngles = Vector3.up * (-Input.GetAxis("Horizontal") * .5f + .5f) * 180;
        lookLeft = Input.GetAxisRaw("Horizontal") < 0;
      }
      else if (lookLeft)
      {
        gfxTransform.eulerAngles = Vector3.up * 180;
      } else
      {
        gfxTransform.eulerAngles = Vector3.zero;
      }

      transform.position += input.normalized * Time.deltaTime * 6;


      if (Input.GetKeyDown(KeyCode.E))
      {
        if (!CheckForHarvest())
        {
          CheckForHidingPlace();
        } else
        {
          //print("tryHide");
        }
      }

      if (Input.GetKeyDown(KeyCode.Mouse0))
      {
        hitBox.TryStrike();
      }
    } else if(!harvesting)
    {
      if (Input.GetKeyDown(KeyCode.E))
        CheckForHidingPlace();
    }

    if (Input.GetKeyUp(KeyCode.E))
    {
      harvesting = false;
      playerAnimation.SetBool("Harvesting", false);

      if (currentHarvest != null)
        currentHarvest.harvesting = false;
    }
  }

  public void CompleteHarvest()
  {
    harvesting = false;

    playerAnimation.SetBool("Harvesting", false);

    currentHarvest = null;

    goodieCount++;

    goodieText.text = "X " + goodieCount;
  }

  bool CheckForHarvest()
  {

    bool foundHarvest = Physics2D.OverlapCircle(transform.position, 1, harvestLayer);

    if (!harvesting)
    {
      if (foundHarvest)
      {
        currentHarvest = Physics2D.OverlapCircle(transform.position, 1, harvestLayer).gameObject.GetComponentInParent<HarvestObject>();
        print("sweet");
        harvesting = true;
        playerAnimation.SetBool("Harvesting", true);

        currentHarvest.harvesting = true;
      } else
      {
        print("yeah nah thats bullshit");
      }
    } else
    {
      foundHarvest = false;
    }

    return foundHarvest;
  }

  void CheckForHidingPlace()
  {
    if (hiding)
    {
      gfxTransform.transform.eulerAngles = Vector3.zero;

      playerAnimation.SetBool("Hide", false);

      hayPile.ResetTrigger("Hide");
      hayPile.SetTrigger("Hide");

      StartCoroutine(StopHiding());
    } else
    {
      if (Physics2D.OverlapCircle(transform.position, 1, hideLayer))
      {
        hiding = true;

        Transform hayPileT = Physics2D.OverlapCircle(transform.position, 1, hideLayer).transform;
        gfxTransform.transform.eulerAngles = Vector3.zero;
        transform.position = hayPileT.parent.position;
        //print("sweet");

        playerAnimation.SetBool("Hide", true);

        hayPile = hayPileT.GetComponent<Animator>();

        hayPile.ResetTrigger("Hide");
        hayPile.SetTrigger("Hide");
      }
    }
  }

  IEnumerator StopHiding()
  {
    yield return new WaitForSeconds(.333f);
    hiding = false;
  }
}
