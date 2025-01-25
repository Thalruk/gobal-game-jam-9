using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform from;
    [SerializeField] float minScale = 0.5f;
    [SerializeField] Transform to;
    [SerializeField] float maxScale = 1.5f;

    [SerializeField] GameObject elevatorPrefab;
    [SerializeField] GameObject elevatorObject;
    [SerializeField] float speed;
    Vector2 direction;

    private void Awake()
    {
        elevatorObject = Instantiate(elevatorPrefab, from.transform.position, Quaternion.identity, transform);
        direction = (to.position - from.position).normalized;
        elevatorObject.transform.localScale *= Random.Range(minScale, maxScale);
        elevatorObject.GetComponent<Rigidbody2D>().velocity = direction * speed * Random.Range(minScale, maxScale);
    }

    private void Update()
    {
        if (Vector2.Distance((Vector2)elevatorObject.transform.position, to.position) < 1f)
        {
            elevatorObject.transform.position = from.position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(from.position, 0.5f);
        Gizmos.DrawWireSphere(to.position, 0.5f);
    }
}
