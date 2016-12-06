using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyTargeter : MonoBehaviour
{

    List<Enemy> enemiesInRange = new List<Enemy>();

    IEnumerator currentEnumerator = null;

    IEnumerator TargetClosestEnemy()
    {
        while (true)
        {
            Enemy closestEnemy = null;
            if (enemiesInRange.Count > 0)
                closestEnemy = enemiesInRange[0];

            foreach (Enemy enemy in enemiesInRange)
            {
                if (Vector3.Distance(this.transform.position, closestEnemy.transform.position) >
                    Vector3.Distance(this.transform.position, enemy.transform.position))
                {
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                Debug.LogError(closestEnemy.name);
                this.transform.LookAt(closestEnemy.transform.position);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void Update()
    {

            
    }

    void OnTriggerEnter(Collider col)
    {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        Debug.LogError(col.gameObject.name);

        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);

            if(currentEnumerator == null)
            {
                currentEnumerator = TargetClosestEnemy();
                StartCoroutine(currentEnumerator);
            }
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

            if(currentEnumerator != null)
            {
                StopCoroutine(currentEnumerator);
                currentEnumerator = null;
            }
        }
        else
            Debug.LogError(enemy);
    }

}