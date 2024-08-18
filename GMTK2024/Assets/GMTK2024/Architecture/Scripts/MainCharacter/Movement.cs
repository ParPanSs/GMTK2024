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
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startSpeed = speed;
    }

    private void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        RaycastHit2D ray = Physics2D.Raycast(legs.transform.position, Vector2.down, .1f, 
            whatIsGround);
        if (ray.collider != null)
        {
            _canJump = true;
            speed = _startSpeed;
        }
        else
        {
            _canJump = false;
            speed = 5f;
        }
        if (_canJump && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(new Vector2(_rb.velocity.x, jumpForce), ForceMode2D.Impulse);
            StartCoroutine(childrenMovement.Jump());
        }
    }
    
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * speed, _rb.velocity.y);
    }
}
