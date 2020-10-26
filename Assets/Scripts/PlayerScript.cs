using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    Animator anim;

    public float speed;
    public float jumpVelocity;
    public float hozMovement;

    private int scoreValue = 0;
    private int lifeValue = 3;
    
    public Text winText;
    public Text meText;
    public Text endText;
    public Text score;
    public Text life;
    public Text dirText;
    public Text loseText;

    private bool facingRight = true;

    public AudioClip bgMusic;
    public AudioClip endingMusic;
    public AudioClip loseMusic;
    public AudioClip munch;
    public AudioClip mouse;

    public AudioSource musicSource;
    public AudioSource soundSource;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        score.text = "Cookies: " + scoreValue.ToString();
        life.text = "Lives: " + lifeValue.ToString();

        //Music
        musicSource.clip = bgMusic;
        soundSource.clip = munch;
        musicSource.Play();

        winText.text = "";
        endText.text = "";
        meText.text = "";
        loseText.text = "";
        StartCoroutine(TextTime());
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hozMovement = Input.GetAxisRaw("Horizontal");

        rd2d.AddForce(new Vector2(hozMovement * speed, 0f));

        anim.SetInteger("State", Mathf.Abs((int)hozMovement));

        //Restart Game
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("LevelOne");
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void Update()
    {
        if (facingRight == false && hozMovement > 0)
            Flip();
        else if (facingRight == true && hozMovement < 0)
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Cookie")
        {
            soundSource.Stop();
            soundSource.clip = munch;
            soundSource.Play();
            scoreValue += 1;
            Destroy(collision.collider.gameObject);
            SetScoreText();
        }

        if (collision.collider.tag == "Rat")
        {
            soundSource.Stop();
            soundSource.clip = mouse;
            soundSource.Play();
            lifeValue -= 1;
            Destroy(collision.collider.gameObject);
            SetLifeText();
        }

        if (collision.collider.tag == "Ground")
        {
            anim.SetBool("Jumping", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                rd2d.velocity = Vector2.up * jumpVelocity;
                anim.SetBool("Jumping", true);
                anim.SetInteger("State", 2);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void SetScoreText()
    {
        score.text = "Cookies: " + scoreValue.ToString();

        if (scoreValue == 4)
        {
            transform.position = new Vector3(41.5f, -1f, 0f);
            lifeValue = 3;
            SetLifeText();
        }

        if (scoreValue == 8)
        {
            //music
            musicSource.Stop();
            musicSource.clip = endingMusic;
            musicSource.Play();

            winText.text = "You Win!";
            meText.text = "Created by Amorina Tabera\nThank you for playing!";
            endText.text = "Press \"Esc\" to quit or \"R\" to restart";
        }
    }

    void SetLifeText()
    {
        life.text = "Lives: " + lifeValue.ToString();

        if (lifeValue == 0 && scoreValue != 8)
        {
            //music
            musicSource.Stop();
            musicSource.clip = loseMusic;
            musicSource.Play();

            anim.SetInteger("State", 3);

            loseText.text = "You Lose!";
            endText.text = "Press \"Esc\" to quit or \"R\" to restart";
        }
    }

    //Display temproary text
    IEnumerator TextTime()
    {
        dirText.text = "Move Cat with AD or arrows and press W to Jump\n\n Collect the Cookies!\n\nAVOID THE RATS!!!";
        yield return new WaitForSecondsRealtime(5);
        dirText.text = "";
    }
}
