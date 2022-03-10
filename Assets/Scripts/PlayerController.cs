using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public static GameObject player;
    public static GameObject currentPlatform;
    public static AudioSource[] sfx;
    bool canTurn = false;
    Vector3 startPosition;
    public static bool isDead = false; 
    Rigidbody rb;

    public GameObject magic;
    public Transform magicStartPos;
    Rigidbody mRb;

    int livesLeft;

    public Texture aliveIcon;
    public Texture deadIcon;
    public RawImage[] icons;

    public GameObject gameOverPanel;

    public Text highScore;

    bool falling = false;

    void RestartScene()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    void OnCollisionEnter(Collision other) 
    {
        if ((falling || other.gameObject.tag == "Fire" || other.gameObject.tag == "Wall") && !isDead)
        {
            if (falling)
                anim.SetTrigger("isFalling");
            else
                anim.SetTrigger("isDead");
            sfx[6].Play();
            isDead = true;
            livesLeft--;
            PlayerPrefs.SetInt("lives", livesLeft);

            if (livesLeft > 0)
                Invoke("RestartScene", 2f);
            else
            {
                icons[0].texture = deadIcon;
                gameOverPanel.SetActive(true);
                PlayerPrefs.SetInt("lastscore", PlayerPrefs.GetInt("score"));
                if(PlayerPrefs.HasKey("highscore"))
                {
                    int hs = PlayerPrefs.GetInt("highscore");
                    if(hs < PlayerPrefs.GetInt("score"))
                    {
                        PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score"));
                    }
                    else
                    PlayerPrefs.SetInt("highscore", hs);
                }
            }
        }
        else
            currentPlatform = other.gameObject;
    }

    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        mRb = magic.GetComponent<Rigidbody>();
        sfx = GameObject.FindWithTag("GameData").GetComponentsInChildren<AudioSource>();

        player = this.gameObject;
        GenerateWorld.RunDummy();
        if(PlayerPrefs.HasKey("highscore"))
            highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore");
        else
            highScore.text = "High Score: 0";

        startPosition = player.transform.position;

        isDead = false;
        livesLeft = PlayerPrefs.GetInt("lives");

        for (int i = 0; i < icons.Length; i++)
        {
            if (i >= livesLeft)
                icons[i].texture = deadIcon;
        }
    }

    void CastMagic()
    {
        magic.transform.position = magicStartPos.position;
        magic.SetActive(true);
        mRb.velocity = Vector3.zero;
        mRb.AddForce(this.transform.forward * 1000);
        Invoke("KillMagic", 1);
    }

    void KillMagic()
    {
        magic.SetActive(false);
    }

    void PlayMagic()
    {
        sfx[7].Play();
    }

    void Footstep1()
    {
        sfx[4].Play();
    }

    void Footstep2()
    {
        sfx[3].Play();
    }

    void OnTriggerEnter(Collider other) 
    {   
        if (other is BoxCollider && GenerateWorld.lastPlatform.tag != "platformTSection")
            GenerateWorld.RunDummy();

        if (other is SphereCollider)
            canTurn = true;
    }

    void OnTriggerExit(Collider other) 
    {
        if (other is SphereCollider)
            canTurn = false;
    }

    void Update()
    {
        if(isDead) return;

        if(currentPlatform != null)
        {
            if(this.transform.position.y < currentPlatform.transform.position.y - 5)
            {
                falling = true;
                OnCollisionEnter(null);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isJumping") == false)
        {
            anim.SetBool("isJumping", true);
            sfx[2].Play();
            rb.AddForce(Vector3.up * 200);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("isMagic", true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * 90);
            GenerateWorld.dummyTraveler.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);

            canTurn = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && canTurn)
        {
            this.transform.Rotate(Vector3.up * -90);
            GenerateWorld.dummyTraveler.transform.forward = -this.transform.forward;
            GenerateWorld.RunDummy();

            if (GenerateWorld.lastPlatform.tag != "platformTSection")
                GenerateWorld.RunDummy();

            this.transform.position = new Vector3(startPosition.x, this.transform.position.y, startPosition.z);

            canTurn = false;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            this.transform.Translate(-0.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            this.transform.Translate(0.5f, 0, 0);
        }
    }

    void StopJump()
    {
        anim.SetBool("isJumping", false);
    }

    void StopMagic()
    {
        anim.SetBool("isMagic", false);
    }
}
