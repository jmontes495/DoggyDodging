using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private float speed = 5f;
    private float force = 300f;
    private DogMouth dogMouthDef;

    void Start()
    {
        Cursor.visible = false;
    }
    void OnCollisionEnter(Collision other)
    {
        DogMouth dogMouth = other.gameObject.GetComponent<DogMouth>();

        if (dogMouth == null)
        {
            rb.velocity = Vector3.zero;
            Vector3 direction = other.GetContact(0).normal;
            if (direction.y <= 0)
                direction.y = 1f;
            rb.AddForce(direction.normalized * force);
        }
        else
        {
            dogMouthDef = dogMouth;
            GameController.Instance.Lose();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Coin coin = other.gameObject.GetComponent<Coin>();

        if (coin != null)
        {
            GameController.Instance.IncreaseCount();
            coin.Restart();
        }
    }

    void FixedUpdate()
    {
        rb.useGravity = GameController.Instance.ShouldMove;
        rb.isKinematic = !GameController.Instance.ShouldMove;

        if (!GameController.Instance.ShouldMove)
        {
            if (GameController.Instance.InMouth)
                transform.position = dogMouthDef.gameObject.transform.position;
            return;
        }

        float rotateHorizontal = Input.GetAxis("Horizontal");
        float rotateVertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(rotateHorizontal, 0, rotateVertical) * speed * Time.deltaTime;
    }
}
