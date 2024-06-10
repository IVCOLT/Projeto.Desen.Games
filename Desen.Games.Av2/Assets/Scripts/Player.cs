using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator playerAnim;
    private Rigidbody2D rbPlayer;
    public float velocidade;
    private SpriteRenderer sr;
    public float jumpForce;
    public bool inFloor = true;
    public int coins;

    private GameController gcPlayer;

    void Start()
    {
        gcPlayer = FindObjectOfType<GameController>();
        if (gcPlayer == null)
        {
            Debug.LogError("GameController não encontrado na cena!");
        }
        else
        {
            gcPlayer.coins = 0;
        }

        playerAnim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rbPlayer = GetComponent<Rigidbody2D>();
        Debug.Log("Player script started.");
    }

    private void FixedUpdate()
    {
        MovimentoPlayer();
    }

    void Update()
    {
        Jump();
    }

    void MovimentoPlayer()
    {
        float movimentoHorizontal = Input.GetAxisRaw("Horizontal");
        rbPlayer.velocity = new Vector2(movimentoHorizontal * velocidade, rbPlayer.velocity.y);

        if (movimentoHorizontal > 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = false;
        }
        else if (movimentoHorizontal < 0)
        {
            playerAnim.SetBool("Walk", true);
            sr.flipX = true;
        }
        else
        {
            playerAnim.SetBool("Walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && inFloor)
        {
            playerAnim.SetBool("Jump", true);
            rbPlayer.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            inFloor = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerAnim.SetBool("Jump", false);
            inFloor = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Debug.Log("Coin collected: " + collision.gameObject.name);
            Destroy(collision.gameObject);
            if (gcPlayer != null)
            {
                gcPlayer.coins++;
                gcPlayer.UpdateCoinsText();
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player morreu");
    
        Destroy(gameObject);
    
    }
}
