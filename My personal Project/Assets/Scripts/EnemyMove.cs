using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    Rigidbody2D rigid;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 3);

    }


    void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2(nextMove, rigid.linearVelocityY);
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Think", 3);
    }
}
