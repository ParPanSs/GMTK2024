using System.Collections.Generic;
using UnityEngine;

public class FollowDad : MonoBehaviour
{
    [SerializeField] private GameObject leader;
    [SerializeField] private int steps;

    private Rigidbody2D _rb;
    public Queue<float> _record = new();
    private float _lastRecord;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() 
    {
        if (leader.transform.position.x != _lastRecord)
            _record.Enqueue(leader.transform.position.x);

        if (_record.Count <= steps) return;
        _lastRecord = _record.Dequeue();
        _rb.position += new Vector2(_lastRecord, transform.position.y);
    }
}
