using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshBaker : MonoBehaviour
{
    [SerializeField] NavMeshSurface navMeshSurface;
    [SerializeField] EnemySpawner enemies;

    // can't prebake the navmesh so this gets called after the floor mesh is built by the dungeonCreator.cs script
    // fun fact: originally I thought each mesh needed a NavMeshSurface so this used to be pretty laggy
    // another fun fact: it is late at night and I wish I could get back to doing the fun parts of this project,
    // like enemy AI and player movement BUT I DUG THIS GRAVE OF MY OWN FRUITION
    public void Bake()
    {
        navMeshSurface.BuildNavMesh();

        // making sure things don't fall into the infinite void by forcing them to be inside the navmesh

        // set player position

        // set enemy position
        enemies.SpawnEnemy();
    }

}
