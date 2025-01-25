using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform from;
    [SerializeField] Transform to;

    [SerializeField] GameObject elevatorPrefab;
    [SerializeField] GameObject elevatorObject;
    [SerializeField] float speed;
    Vector2 direction;

    private void Awake()
    {
        elevatorObject = Instantiate(elevatorPrefab, from.transform.position, Quaternion.identity, transform);
        direction = (to.position - from.position).normalized;
        elevatorObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    private void Update()
    {
        if (Vector2.Distance((Vector2)elevatorObject.transform.position, to.position) < 1f)
        {
            elevatorObject.transform.position = from.position;
        }
    }
}
