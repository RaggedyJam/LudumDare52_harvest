using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

  bool lookLeft;
  public bool harvesting;
  public bool hiding;
  public bool snatched = false;

  bool canRestart = false;

  int harvestLayer = 1 << 10;
  int hideLayer = 1 << 11;

  public bool countDown;
  int goodieCount = 6;
  int farmerCount = 1;

  float speed = 9;

  public Color goodieFlashColour;
  public Color goodieDefaultColour;
  public Color farmerFlashColour;
  public Color farmerDefaultColour;

  public AudioSource harvestSound;
  public AudioSource hideSound;
  public AudioSource death;

  public Animator playerAnimation;

  public GameObject deathUI;

  HarvestObject currentHarvest;

  public Transform gfxTransform;

  Animator hayPile;

  public Text goodieText;
  public Text farmerText;

  public HitBox hitBox;

  public Music music;

  public Indicator indicator;

  public RandomGen randomGen;

  OffScreenSpawning farmers;

  void Start()
  {
    if (!countDown)
      goodieCount = 0;

    goodieText.text = "X " + goodieCount;
    farmerText.text = "X " + farmerCount;

    GetReferences();
  }

  void Update()
  {
    if (!snatched)
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

        transform.position += Input.GetAxisRaw("Horizontal") * Vector3.right * Time.deltaTime * speed;
        transform.position += Input.GetAxisRaw("Vertical") * Vector3.up * Time.deltaTime * speed;


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

        harvestSound.Stop();

        if (currentHarvest != null)
          currentHarvest.harvesting = false;
      }
    }

    if (canRestart)
    {
      if (Input.GetKeyDown(KeyCode.R))
      {
        SceneManager.LoadScene(1);
      }
    }

    if (Input.GetKeyDown(KeyCode.Escape))
    {
      SceneManager.LoadScene(0);
    }
  }

  void GetReferences()
  {
    farmers = FindObjectOfType<OffScreenSpawning>();
  }

  public void CompleteHarvest()
  {
    harvesting = false;

    playerAnimation.SetBool("Harvesting", false);

    indicator.HideIndicator();

    currentHarvest = null;

    if (!snatched)
      indicator.SetTarget(randomGen.SpawnGoodie(), 1);

    StartCoroutine(UpdateGoodieCount());

    harvestSound.Stop();
  }

  IEnumerator UpdateGoodieCount()
  {
    if (!countDown)
      goodieCount++;
    else
      goodieCount--;

    goodieText.text = "X " + goodieCount;

    float t = 0;

    while (t < 1)
    {
      float scale = 2;

      scale = Mathf.Lerp(2, 1, t);

      Color currentColour = Color.Lerp(goodieFlashColour, goodieDefaultColour, t);

      goodieText.color = currentColour;
      goodieText.transform.localScale = Vector3.one * scale;

      yield return null;
      t += Time.deltaTime * 3;
    }

    if (countDown)
    {
      if (goodieCount == 0)
      {
        StartCoroutine(IncreaseFarmerCount());
        goodieCount = 6;
      }
    } else
    {
      if (goodieCount % 6 == 0)
      {
        StartCoroutine(IncreaseFarmerCount());
      }
    }
  }

  IEnumerator IncreaseFarmerCount()
  {
    bool maxReached = false;

    farmers.interval -= .1f;

    farmerCount ++;

    if (farmers.interval < .35f)
    {
      farmers.interval = .35f;
      maxReached = true;
    }

    if (maxReached)
      farmerText.text = "MAX!";
    else
    {
      farmerText.text = "X " + farmerCount;
    }

    if (farmerCount < 9)
    {
      float t = 0;

      while (t < 1)
      {
        float scale = 2;

        scale = Mathf.Lerp(2, 1, t);

        Color currentColour = Color.Lerp(farmerFlashColour, farmerDefaultColour, t);

        farmerText.color = currentColour;
        farmerText.transform.localScale = Vector3.one * scale;

        yield return null;
        t += Time.deltaTime * 3;
      }
    }

    yield return null;
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

        harvestSound.Play();
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

      hideSound.Play();

      indicator.ShowIndicator();

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

        hideSound.Play();

        indicator.HideIndicator();

        hayPile.ResetTrigger("Hide");
        hayPile.SetTrigger("Hide");
      }
    }
  }

  public void SnatchPlayer()
  {
    if (!snatched)
    {
      snatched = true;

      gfxTransform.gameObject.SetActive(false);

      music.StopMusic();
      death.Play();

      indicator.HideIndicator();

      StartCoroutine(ShowDeathUI());

      print("SnatchPlayer");
    }
  }

  IEnumerator ShowDeathUI()
  {
    yield return new WaitForSeconds(3);

    canRestart = true;

    deathUI.SetActive(true);
  }

  IEnumerator StopHiding()
  {
    yield return new WaitForSeconds(.333f);
    hiding = false;
  }
}
