using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelLocator : MonoBehaviour
{
    // [SerializeField] private List<GameObject> floors = new();
    [SerializeField] private List<Vector2> bottomLeftCorner = new(), topRightCorner = new();
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject endPrefab;
    public void StoreFloor(GameObject floorMesh, Vector3 bottomLeft, Vector3 topRight)
    {
        // floors.Add(floorMesh); // smol function but I like doing it this way
        bottomLeftCorner.Add(bottomLeft);
        topRightCorner.Add(topRight);
    }

    public void CreateEnd()
    {
        // GameObject endLocation = new GameObject();
        int farthest = 0;
        for (int i = 0; i < topRightCorner.Count; i++)
        {
            if (Vector2.Distance(topRightCorner[i], playerTransform.position) > Vector2.Distance(topRightCorner[farthest], playerTransform.position))
            {
                farthest = i; 
                // we can't use the transform.position of this mesh because it's actually 0, 0, 0. instead, we find its center using the information about its corners
            }
        }
        Vector2 endDoorVector2 = bottomLeftCorner[farthest] + ((topRightCorner[farthest] - bottomLeftCorner[farthest])/2);




        Instantiate(endPrefab, new Vector3(endDoorVector2.x, 0, endDoorVector2.y), Quaternion.identity);
    }






}

