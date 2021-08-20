using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] float speed = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AlreadyJumpBall")
            return;
        if (other.CompareTag("NewBall"))
        {
            other.gameObject.name = "AlreadyJumpBall";
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            // other.gameObject.GetComponent<Rigidbody>().AddForce(positions[Random.Range(0, positions.Length)].position.normalized * speed);
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * speed);
        }
    }
}
