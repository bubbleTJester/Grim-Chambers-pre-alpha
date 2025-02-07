using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    [SerializeField] float awarenessRadius;
    public bool isAggro = true;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }
    private void Update()
    {
        var dist = Vector3.Distance(transform.position, playerTransform.position);

        if(dist < awarenessRadius) { isAggro = true; }
        else { isAggro = false; }
    }
}
