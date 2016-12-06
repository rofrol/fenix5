using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyTargeter : MonoBehaviour
{

    List<Enemy> enemiesInRange = new List<Enemy>();

    void Update()
    {
        Enemy closestEnemy = null;
        if (enemiesInRange.Count > 0)
            closestEnemy = enemiesInRange[0];

        foreach(Enemy enemy in enemiesInRange)
        {
            if(Vector3.Distance(this.transform.position, closestEnemy.transform.position) >
                Vector3.Distance(this.transform.position, enemy.transform.position)) {
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            Debug.LogError(closestEnemy.name);
            this.transform.LookAt(closestEnemy.transform.position);
        }
            
    }

    void OnTriggerEnter(Collider col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        Debug.LogError(col.gameObject.name);

        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
        }
        else
            Debug.LogError(enemy);
    }

    void OnTriggerExit(Collider col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        Debug.LogError(col.gameObject.name);

        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
        }
        else
            Debug.LogError(enemy);
    }

}