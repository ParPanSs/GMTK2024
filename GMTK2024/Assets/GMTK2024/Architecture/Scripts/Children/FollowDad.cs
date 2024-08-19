using UnityEngine;

public class FollowDad : MonoBehaviour
{
    [SerializeField] private GameObject leader;
    [SerializeField] private GameObject followPoint;
    [SerializeField] private float speed;

    private Rigidbody2D _rb;
    private float _lastRecord;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!leader) return;
        _lastRecord = leader.transform.position.x;
        _rb.position = Vector2.MoveTowards(_rb.position, new Vector2(_lastRecord, _rb.position.y),
            speed * Time.fixedDeltaTime);
    }

    public void FollowTarget(bool isFacingRight)
    {
        leader = isFacingRight switch
        {
            true => followPoint,
            false => null
        };
    }
}
