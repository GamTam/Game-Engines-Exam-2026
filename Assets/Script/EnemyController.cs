using System;
using UnityEngine;

public class EnemyController : FallingObject
{
    new void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
