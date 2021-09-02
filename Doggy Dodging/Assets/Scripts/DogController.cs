using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{
    [SerializeField] private BallController ball;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.ShouldMove)
            return;

        agent.SetDestination(ball.transform.position);
        anim.SetBool("Moving", agent.velocity.magnitude > 0.5f);
    }
}
