using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JIGameManager : MonoBehaviour
{
    public Action<int> ON_LEVEL_COMPLETED_ACTION;

    public static JIGameManager Instance;

    public bool play;
    public bool playerRunControl;
    public bool trajectoryOn;
    public Rigidbody player;
    public bool finish;
    public bool fastSpeedOn;

    [Header("Fire works")]
    public List<GameObject> blast;
    public GameObject paperParticle1;
    public GameObject paperParticle2;

    [Header("Sliders only")]
    public GameObject slidersParent;

    [Header("Player Positions and Players")]
    public List<Transform> playerPos;
    public int playerPositionCounter;
    public List<GameObject> players;

    [Header("Animations for other player's")]
    public GameObject otherPlayer1;
    public GameObject otherPlayer2;

    [Header("UI elements")]
    public Image timeImage;
    public TextMeshProUGUI timeText;
    public int integerTimer;
    public GameObject timer;
    public GameObject startTimer;

    [Header("Particle effects")]
    public GameObject jumpEffect;
    public GameObject finishEffect;
    public GameObject freezEffect;

    [Space]
    public GameObject tookJumpEffect;
    public GameObject tooklongJumpEffect;
    public GameObject tookFreezeEffect;

    [Space]
    public GameObject trailEffectSpine;
    public GameObject trailEffectLeftHand;
    public GameObject trailEffectRighttHand;

    [Header("Trajectories")]
    public GameObject smallTrajectory;
    public GameObject longTrajectory;

    public float longJumpControlTimer;


    public bool calculated;
    public bool gotPower;
    public bool otherPlayerFreeze;

    public float time = 5;
    public float tempTime = 5;

    public GameObject levelCompletePanel;

    [Header("All Sounds")]
    public AudioClip jump;
    public AudioClip powerPickUp;
    public AudioClip fall;
    public AudioClip win;
    public AudioClip stage;
    private AudioSource _audioSource;
    public int finishedPlayers;

    private void Awake()
    {
        Instance = this;
        finishedPlayers = 0;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && !play)
        {
            startTimer.SetActive(true);
            StartCoroutine(StartRunning());
        }

        if(fastSpeedOn)
        {
            tempTime -= Time.deltaTime;
            timeImage.fillAmount = tempTime / time;
            integerTimer = Mathf.RoundToInt(tempTime);
            timeText.text = integerTimer.ToString();
            if(tempTime < 0)
            {
                timer.SetActive(false);
            }
        }
    }

    public void CharacterJump()
    {
        player.GetComponent<Animator>().SetInteger("Character Animator", 4);
    }

    public void CharacterLand()
    {
        player.GetComponent<Animator>().SetInteger("Character Animator", 5);
    }

    public void CharacterFall()
    {
        player.GetComponent<Animator>().SetInteger("Character Animator", 6);
        playerRunControl = false;
        trajectoryOn = false;
        StartCoroutine(PlayerFall());
    }

    public void StoneHit()
    {
        if(!fastSpeedOn)
        {
            player.GetComponent<Animator>().SetInteger("Character Animator", 9);
            playerRunControl = false;
            trajectoryOn = false;
            StartCoroutine(PlayerFall());
        }
        else
        {
            playerRunControl = true;
        }
       
    }
    public void FireWorks()
    {
        for (int i = 0; i < blast.Count; i++)
        {
            blast[i].SetActive(true);
        }
        StartCoroutine(PaperParticleBlast());
    }

    public void SlidersOff()
    {
        slidersParent.SetActive(false);
    }

    public void CalculatePlayerPositions(int playerNumber, int playerPosition)
    {
        players[playerNumber].transform.position = playerPos[playerPosition].position;
        players[playerNumber].GetComponent<Animator>().SetInteger("WinAnimation", (playerPosition + 1));
    }

    public void CalculateOtherPlayers()
    {
        
        players[1].transform.position = playerPos[1].position;
        players[1].GetComponent<Animator>().SetInteger("WinAnimation", (1 + 1));

        players[2].transform.position = playerPos[2].position;
        players[2].GetComponent<Animator>().SetInteger("WinAnimation", (2 + 1));

        calculated = true;
    }

    public void FastSpeedOnFunction()
    {
        fastSpeedOn = true;
        timer.SetActive(true);
        JITrajectoryScript.Instance.carMoveSpeed = 15;
        trailEffectSpine.SetActive(true);
        trailEffectLeftHand.SetActive(true);
        trailEffectRighttHand.SetActive(true);
        playerRunControl = true;
        StartCoroutine(SpeedControler());
    }

    public void WinSFX()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0) _audioSource.PlayOneShot(win);
    }

    public void PowerPickUpSFX()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0) _audioSource.PlayOneShot(powerPickUp);
    }

    public void JumpSFX()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0) _audioSource.PlayOneShot(jump);
    }

    public void FallSFX()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0) _audioSource.PlayOneShot(fall);
    }

    public void StageSFX()
    {
        if (PlayerPrefs.GetInt("Audio", 0) == 0) _audioSource.PlayOneShot(stage);
    }

    private void FastSpeedOffFunction()
    {
        fastSpeedOn = false;
        JITrajectoryScript.Instance.carMoveSpeed = 10;
        JITrajectoryScript.Instance.lineRenderer.enabled = false;
        trailEffectSpine.SetActive(false);
        trailEffectLeftHand.SetActive(false);
        trailEffectRighttHand.SetActive(false);
        timer.SetActive(false);
        tempTime = time;
    }

    public void LongJumpOnFunction()
    {
        time = 3;
        tempTime = 3;
        timer.SetActive(true);
        fastSpeedOn = true;
        StartCoroutine(LongJumpControlFunction());
    }

    public void FreeeTimerOn()
    {
        time = 3;
        tempTime = 3;
        timer.SetActive(true);
        fastSpeedOn = true;
        StartCoroutine(FreeTimerControl());
    }

    private void FreeTimerOff()
    {
        fastSpeedOn = false;
    }

    private void LongJumpOffFunction()
    {
        longTrajectory.GetComponent<JITrajectoryScript>().lineRenderer.enabled = false;
        smallTrajectory.GetComponent<JITrajectoryScript>().PointsCounterReset();
        longTrajectory.SetActive(false);
        smallTrajectory.SetActive(true);
        fastSpeedOn = false;
        player.useGravity = true;
        playerRunControl = true;
        player.GetComponent<Animator>().SetInteger("Character Animator", 3);
    }

    public void LevelCompletePanelOn()
    {
        ON_LEVEL_COMPLETED_ACTION?.Invoke(finishedPlayers + 1);
        levelCompletePanel.SetActive(true);
        
    }

    public void FreezePlayers()
    {
        otherPlayer1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameObject freezCrystal1 = Instantiate(freezEffect, new Vector3(otherPlayer1.transform.position.x, otherPlayer1.transform.position.y + 0.5f, otherPlayer1.transform.position.z), freezEffect.transform.rotation);
        otherPlayer1.GetComponent<Animator>().SetInteger("Character Animator", 8);
        Destroy(freezCrystal1, 3);

        otherPlayer2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameObject freezCrystal2 = Instantiate(freezEffect, new Vector3(otherPlayer2.transform.position.x, otherPlayer2.transform.position.y + 0.5f, otherPlayer2.transform.position.z), freezEffect.transform.rotation);
        otherPlayer2.GetComponent<Animator>().SetInteger("Character Animator", 8);
        Destroy(freezCrystal2, 3);

        otherPlayerFreeze = true;
        StartCoroutine(FreezeControl());
    }

    private void UnFreezePlayers()
    {
        otherPlayerFreeze = false;
        otherPlayer1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        otherPlayer2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        otherPlayer1.GetComponent<Animator>().SetInteger("Character Animator", 3);
        otherPlayer2.GetComponent<Animator>().SetInteger("Character Animator", 3);
    }

    IEnumerator FreeTimerControl()
    {
        yield return new WaitForSeconds(3);
        FreeTimerOff();
    }

    IEnumerator FreezeControl()
    {
        yield return new WaitForSeconds(3);
        UnFreezePlayers();
    }

    IEnumerator LongJumpControlFunction()
    {
        yield return new WaitForSeconds(longJumpControlTimer);
        playerRunControl = true;
        player.GetComponent<Animator>().SetInteger("Character Animator", 3);
        smallTrajectory.GetComponent<JITrajectoryScript>().lineRenderer.enabled = false;
        smallTrajectory.GetComponent<JITrajectoryScript>().PointsCounterReset();
        smallTrajectory.SetActive(false);
        longTrajectory.SetActive(true);

        yield return new WaitForSeconds(3f);
        LongJumpOffFunction();
    }

    IEnumerator SpeedControler()
    {
        yield return new WaitForSeconds(5);
        FastSpeedOffFunction();
    }

    IEnumerator PlayerFall()
    {
        yield return new WaitForSeconds(3);
        player.GetComponent<Animator>().SetInteger("Character Animator", 3);        
        playerRunControl = true;
        trajectoryOn = true;
    }

    IEnumerator PaperParticleBlast()
    {
        yield return new WaitForSeconds(2);
        paperParticle1.SetActive(true);
        paperParticle2.SetActive(true);
    }

    IEnumerator StartRunning()
    {
        yield return new WaitForSeconds(2.5f);
    
        play = true;
        playerRunControl = true;
        trajectoryOn = true;
        player.GetComponent<Animator>().SetInteger("Character Animator", 3);
    
        if (otherPlayer1)
        {
            otherPlayer1.GetComponent<Animator>().SetInteger("Character Animator", 3);
        }
    
        if (otherPlayer2)
        {
            otherPlayer2.GetComponent<Animator>().SetInteger("Character Animator", 3);
        }

        startTimer.SetActive(false);
    }
}
