using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MyScript : MonoBehaviour
{
    public float moveX;
    private Rigidbody2D rb;
    public float speed;
    public float JumpForce;

    public bool IsJumping;
    private int score;
    public Text scoreUI;

    public AudioClip coin;
    public AudioClip jumpsound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        //5-10
        speed = Random.Range(5, 11);
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");// -1 => 1
        rb.velocity = new Vector2(moveX * speed , rb.velocity.y);
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            rb.AddForce(new Vector2(rb.velocity.x , JumpForce));
            audioSource.PlayOneShot(jumpsound);
        }
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            IsJumping = false;
        }
    }
    private void OnCollisionExit2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Ground"))
        {
            IsJumping = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.CompareTag("Item"))
        {
            //destory
            Destroy(target.gameObject);
            score += 10; // scroe = score + 10;
            scoreUI.text = "score = " + score.ToString();
            audioSource.PlayOneShot(coin);
        }
        if (target.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (target.gameObject.CompareTag("Door"))
        {
            SceneManager.LoadScene("Level 2");
        }
    }
}
