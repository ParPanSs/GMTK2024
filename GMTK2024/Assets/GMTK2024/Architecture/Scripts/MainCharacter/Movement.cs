using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject legs;
    [SerializeField] private ChildrenMovement childrenMovement;
    private Rigidbody2D _rb;
    private float _horizontal;
    private float _startSpeed;
    private bool _canJump;
    private bool _isFacingRight = true;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startSpeed = speed;
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        Collider2D ray = Physics2D.OverlapCircle(legs.transform.position, .2f, whatIsGround);
        if (ray != null)
        {
            _canJump = true;
            speed = _startSpeed;
        }
        else
        {
            _canJump = false;
            speed = 7f;
        }
        if (_canJump && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(new Vector2(_rb.velocity.x, jumpForce), ForceMode2D.Impulse);
            StartCoroutine(childrenMovement.Jump());
        }
        
        Flip(); 
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speed, _rb.velocity.y);
    }
    
    private void Flip()
    {
        if (_isFacingRight && _horizontal < 0 || !_isFacingRight && _horizontal > 0)
        {
            childrenMovement.Flip(_isFacingRight, _horizontal);
            Vector3 localScale = transform.localScale;
            _isFacingRight = !_isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
