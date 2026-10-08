using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float _timeUntilDeath = 10;
    [SerializeField] private float _momentum = 3f;

    public void FlipMomentum()
    {
        _momentum *= -1;
    }
    
    private void Update()
    {
        Vector3 pos = transform.position;
        _timeUntilDeath -= Time.deltaTime;

        if (_momentum < 0.1f) _momentum += Time.deltaTime * 7;
        else if (_momentum > 0.1f) _momentum -= Time.deltaTime * 7;
        else _momentum = 0;

        pos.x += _momentum * Time.deltaTime;

        if (_timeUntilDeath <= 0)
        {
            foreach (EnemyController obj in GetComponentsInChildren<EnemyController>())
            {
                obj.transform.parent = null;
            }
            Destroy(gameObject);
        }

        transform.position = pos;
    }
}
