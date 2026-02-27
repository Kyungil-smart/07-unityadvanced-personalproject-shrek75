using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float h = 0;

        if (Keyboard.current.aKey.isPressed)
            h = -1;
        else if (Keyboard.current.dKey.isPressed)
            h = 1;

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
    }
}
