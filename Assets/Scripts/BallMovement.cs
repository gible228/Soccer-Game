using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float kickForce = 10f;
    public int score = 0;
    public Rigidbody2D playerRb;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ballRb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 move = playerRb.linearVelocity.normalized;
            if (move != Vector2.zero)
            {
                ballRb.AddForce(move * kickForce, ForceMode2D.Impulse);
            }
        }
    }
}
