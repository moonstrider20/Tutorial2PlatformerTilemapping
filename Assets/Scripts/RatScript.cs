using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatScript : MonoBehaviour
{
    //Animator anim;
    private bool facingRight = true;

    public Transform startMarker;
    public Transform endMarker;

    public float speed = 1.0f;

    private float startTime;

    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        startTime = Time.time;

        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));

        /*if (transform.position == endMarker.position && facingRight == true)
            Flip();
        else if (transform.position == startMarker.position && facingRight == false)
            Flip();*/

        if (distCovered == journeyLength && facingRight == true)
            Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
}