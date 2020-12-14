using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform[] Target;
    private int index ;
    public Transform sphere;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(Target[index].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            agent.SetDestination(sphere.position);
        }
        else
        {
            if (agent.remainingDistance <= 0.05f)
            {
                index++;
                if (index >= Target.Length)
                {
                    index = 0;
                }
                agent.SetDestination(Target[index].position);
            }
        }
        
    }
}
