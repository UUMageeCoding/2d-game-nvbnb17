using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    public float moveSpeed;
    private Transform target;

    void Start()
    {
        target = FindAnyObjectByType<PlayerController_TopDown>().transform;
    }
    
    void Update()
    {
        theRB.linearVelocity = (target.position - transform.position).normalized * moveSpeed;
    }
}
