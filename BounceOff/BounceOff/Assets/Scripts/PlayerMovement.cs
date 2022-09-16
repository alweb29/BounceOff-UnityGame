using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // false - left, true - right
    public bool direction = false;

    // horizontal movement speed
    public float horziontalSpeed = 300;
    // force of jump
    public float forceOfJump = 2f;
    //sprite ref
    public SpriteRenderer spriterenderer;
    public Sprite [] sprites;
    // rb ref
    public Rigidbody2D rb;
    //gamemenager ref
    public GameMenager gameMenager;
    public GameObject jumpEffect;

    void Start()
    {
        if (PlayerPrefs.GetString(PlayerPrefs.GetInt("birdImageIndex").ToString()) == "Bought")
        {
            spriterenderer.sprite = sprites[PlayerPrefs.GetInt("birdImageIndex")];
        }else
        {
            spriterenderer.sprite = sprites[0];
        }
        
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (direction)
        {//right direction
            rb.AddRelativeForce(Time.deltaTime * Vector2.right * horziontalSpeed);
        }
        else
        {//left direction
            rb.AddRelativeForce(Time.deltaTime * Vector2.left * horziontalSpeed);
        }
    }
    public void Jump()
    {
        Vector2 spawnVector = this.transform.position;

        rb.AddForce(Vector2.up * forceOfJump ,ForceMode2D.Impulse);
        Debug.Log("Jump");
        rb.isKinematic = false;

        // jump audio
        FindObjectOfType<AudioManager>().Play("jumpSound");
    }
    // on collision 
    private void OnCollisionEnter2D(Collision2D other)
    {
        //bounce off wall
        if (other.collider.CompareTag("Wall"))
        {
            BoolChange();
            spriterenderer.flipX = !spriterenderer.flipX;
            //add point at bounce
            gameMenager.AddPoint();
            // changes direction in gamemenager
            WallChange();
            // change spikes if bounces
            gameMenager.SpikeCheck();

        }// die on spike
        else if (other.collider.CompareTag("Spike")|| other.collider.CompareTag("WallSpikes"))
        {
            Debug.Log("spike");
            Death();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Diamond")
        {
            int amountofDimonds = PlayerPrefs.GetInt("diamondAmount");
            amountofDimonds++;
            PlayerPrefs.SetInt("diamondAmount", amountofDimonds);
            gameMenager.DiamondCollected();
        }
    }
    public void Death()
    {
        Destroy(gameObject);
        gameMenager.onDieCanvas.SetActive(true);
    }
    public void BoolChange()
    {
        if (direction)
        {
            direction = false;
        }
        else
        {
            direction = true;
        }
    }
    public void WallChange()
    {
        if (gameMenager.leftWall)
        {
            gameMenager.leftWall = false;
        }
        else
        {
            gameMenager.leftWall = true;
        }
    }


}
