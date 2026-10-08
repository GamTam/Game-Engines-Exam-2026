using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxMoveSpeed = 9;
    [SerializeField] private float _moveIncreaseSpeed = 25;
    [SerializeField] private float _jumpImpulse = 8;
    [SerializeField] private float _gravityRateUp = 20;
    [SerializeField] private float _gravityRateDown = 30;
    [SerializeField] private float _horiDrag = 35;
    [SerializeField] private float _quickTurnSped = 70;
    [SerializeField] private float _floorDistance = 1f;
    [SerializeField] private int _maxJumpCount = 1;
    [SerializeField] private Vector2 _velocity = Vector2.zero;
    [Space]
    [SerializeField] private Bubble _bubblePrefab;

    private bool _facingLeft;
    private LayerMask _layerMask;
    private int _currentJumps;
    private SpriteRenderer _spriteRenderer;

    private bool _shouldKillThisFrame;
    private bool _landedOnEnemyThisFrame;
    
    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Floor");
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _currentJumps < _maxJumpCount)
        {
            _currentJumps += 1;
            _velocity.y = _jumpImpulse;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Vector3 pos = transform.position;
            pos.x += _facingLeft ? -1 : 1;
            pos.y += 1;
            
            Bubble bubbleInstance = Instantiate(_bubblePrefab, pos, Quaternion.identity);
        }

        float moveX = Input.GetAxis("Horizontal");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), _floorDistance, _layerMask);

        if (!hit)
        {
            if (_velocity.y > 0) _velocity.y -= _gravityRateUp * Time.deltaTime;
            else _velocity.y -= _gravityRateDown * Time.deltaTime;
        }

        _velocity.x += _moveIncreaseSpeed * moveX * Time.deltaTime;

        if (Mathf.Abs(moveX) > 0.1f)
        {
            _facingLeft = _velocity.x <= 0;
        }
        
        if (Mathf.Abs(_velocity.x) > 0.1 && Mathf.Abs(moveX) < 0.1f)
        {
            _velocity.x += _velocity.x > 0 ? _horiDrag * Time.deltaTime * -1 : _horiDrag * Time.deltaTime;
        }
        else if (Mathf.Abs(moveX) < 0.1f)
        {
            _velocity.x = 0f;
        }
        
        if (_velocity.x > 0.1 && moveX < -0.5)
        {
            _velocity.x -= _quickTurnSped * Time.deltaTime;
        }
        else if (_velocity.x < -0.1 && moveX > 0.5)
        {
            _velocity.x += _quickTurnSped * Time.deltaTime;
        }
        
        _velocity.x = Mathf.Clamp(_velocity.x, -_maxMoveSpeed, _maxMoveSpeed);

        if (hit && _velocity.y < 0)
        {
            _currentJumps = 0;
            _velocity.y = 0;
            transform.position = new Vector3(transform.position.x, hit.collider.transform.position.y + hit.collider.bounds.extents.y, transform.position.z);
        }
        
        transform.position = (Vector2) transform.position + (_velocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (_shouldKillThisFrame && !_landedOnEnemyThisFrame)
        {
            Destroy(gameObject);
        }

        _shouldKillThisFrame = false;
        _landedOnEnemyThisFrame = false;
    }

    public void EnemyKillCheck(float amount, GameObject enemy, int score)
    {
        if (_velocity.y < 0f)
        {
            _velocity.y = amount;
            Destroy(enemy.gameObject);
            _landedOnEnemyThisFrame = true;
        }
    }

    public void PlayerKillCheck(float bottomPoint)
    {
        if (_velocity.y >= 0f || transform.position.y < bottomPoint)
        {
            _shouldKillThisFrame = true;
        }
    }
}
