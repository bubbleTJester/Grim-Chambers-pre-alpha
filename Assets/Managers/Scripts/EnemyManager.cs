using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    // according to ALT+Tab this is a more efficient way to do this, don't know if I'll be able to find every similar statement though
    public List<EnemyBehavior> enemiesInTrigger = new(); 

    public void AddEnemy(EnemyBehavior enemy)
    {
        enemiesInTrigger.Add(enemy);
    }
    public void RemoveEnemy(EnemyBehavior enemy)
    {
        foreach(var enem in enemiesInTrigger) // weird bug caused enemies bundled together to get nulled out but not removed, this should fix it
        {
            if(enem == null)
            {
                enemiesInTrigger.Remove(enem);
            }
        }
        enemiesInTrigger.Remove(enemy);
    }
}
