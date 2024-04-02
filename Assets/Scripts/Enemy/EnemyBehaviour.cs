using DG.Tweening;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyBehaviour : MonoBehaviour
{
    private bool _trigger = false;
    private bool _jump = false;
    private Vector3 _dir = Vector3.right;
    private float _leftPos;
    private float _rightPos;
    [SerializeField]
    private GameObject _particule;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private Rigidbody2D _rb;

    private void Start()
    {
        var enemyPos = this.transform.position.x;
        _leftPos = enemyPos - _distance;
        _rightPos = enemyPos + _distance;
    }

    void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        if(this.transform.position.x >= _rightPos && _trigger == false)
        {
            _dir = Vector3.left;
            _rb.velocity = _rb.velocity * -1;
        }

        if(this.transform.position.x <= _leftPos && _trigger == false)
        {
            _dir = Vector3.right;
            _rb.velocity = _rb.velocity * -1;
        }
        
        if(Mathf.Abs(_rb.velocity.x) <= Mathf.Abs(_speed))
        {
            _rb.AddForce(_dir*_acceleration, ForceMode2D.Force);  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {                
            if (_jump == false)
            {
                transform.DOLocalJump(transform.position + (Vector3.up*0.5f), 0.1f, 1, 0.1f);
                _particule.transform.position = new Vector3(0, 0.5f, 0);
                Instantiate(_particule, this.transform);
            }
            _jump = true;
            _trigger = true;

            _rb.velocity = (collision.transform.position - this.transform.position).normalized * _speed;
            _dir = (collision.transform.position - this.transform.position).normalized;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _trigger = false;
        _jump = false;

    }
}
