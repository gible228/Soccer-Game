using UnityEngine;

public class BallGoal : MonoBehaviour
{
    public int score = 0;
    public Transform Respawn;
    public Transform RespawnPlayer;
    public Transform RespawnPlayer2;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject Player;
    public GameObject Player2;

    private Transform meeple;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Goal 1 (Left)")
        {
            score += 1;
            Debug.Log("Score: " + score);
            meeple = hp1.gameObject.transform.GetChild(0);
            Destroy(meeple.gameObject);
        }

        if (other.gameObject.name == "Goal 1 (Right)")
        {
            score += 1;
            Debug.Log("Score: " + score);
            meeple = hp2.gameObject.transform.GetChild(0);
            Destroy(meeple.gameObject);
        }

        if (other.gameObject.CompareTag("Goal1"))
        {
            gameObject.transform.position = Respawn.position;
            Player.gameObject.transform.position = RespawnPlayer.position;
            Player2.gameObject.transform.position = RespawnPlayer2.position;
        }

        
    }
    
}
