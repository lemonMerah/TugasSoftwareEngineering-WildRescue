using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    public Transform playerTransform; // Assign in Inspector
    public float detectionRange = 5.0f;
    private Wandering wanderingMovement;
    private Follow followingMovement;

    void Start()
    {
        wanderingMovement = GetComponent<Wandering>();
        followingMovement = GetComponent<Follow>();
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform not assigned in the Inspector");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionRange && distanceToPlayer <= followingMovement.maxFollowDistance)
        {
            followingMovement.isFollowing = true;
            wanderingMovement.enabled = false;
        }
        else
        {
            followingMovement.isFollowing = false;
            wanderingMovement.enabled = true;
        }
    }
}
