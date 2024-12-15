using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoIa : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform pkPlayer;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        pkPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        agent.SetDestination(pkPlayer.position);
    }
}
