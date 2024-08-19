using UnityEngine;

public class ButtonToPress : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Movement>())
        {
            
        }
    }
}
