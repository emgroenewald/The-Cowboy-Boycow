using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2.0f;
    public Transform[] moveSpots;
    private int currentSpot = 0;

    void Update()
    {
        Transform targetSpot = moveSpots[currentSpot];
        transform.position = Vector2.MoveTowards(transform.position, targetSpot.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetSpot.position) < 0.2f)
        {
            currentSpot = (currentSpot + 1) % moveSpots.Length;
        }
    }
}
