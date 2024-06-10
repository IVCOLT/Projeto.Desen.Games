using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; 
    public bool facingRight = true;
    public Transform groundCheck;
    public LayerMask groundLayer;

    void Update()
    {
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (!grounded)
        {
            Flip();
        }

        Vector3 movement = Vector3.right * speed * Time.deltaTime;
        if (!facingRight)
        {
            movement *= -1;
        }
        transform.Translate(movement);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; 
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
