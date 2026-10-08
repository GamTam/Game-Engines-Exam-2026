using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] protected Vector2 _velocity = Vector2.zero;
    [SerializeField] protected float _gravityRateUp = 20;
    [SerializeField] protected float _gravityRateDown = 30;
    [SerializeField] private float _floorDistance = 0.1f;

    protected RaycastHit2D hit;
    protected LayerMask _layerMask;

    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Floor");
    }
    
    protected void Update()
    {
        hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), _floorDistance, _layerMask);

        if (!hit)
        {
            if (_velocity.y > 0) _velocity.y -= _gravityRateUp * Time.deltaTime;
            else _velocity.y -= _gravityRateDown * Time.deltaTime;
        }
    }

    protected void LateUpdate()
    {
        transform.position = (Vector2) transform.position + (_velocity * Time.deltaTime);
    }
}