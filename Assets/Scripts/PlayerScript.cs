using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
    }

    // Update is called once per frame
    void Update() //Fixed update
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        //my jumping kind works, want different setup T-T
        /*if (Input.GetKeyDown(KeyCode.W))
        {
            float jumpVelocity = 10f;
            rd2d.velocity = Vector2.up * jumpVelocity;
            //make gravity 5 and friction .1 and speed 2
        }*/

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }


    private void OnCollisioinStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            //...i don't like this
            if (Input.GetKeyDown(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }


    }
}
