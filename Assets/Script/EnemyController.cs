using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            GameManager.Instance.EnemiesKilled += 1;
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
