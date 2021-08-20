using UnityEngine;

public class DeactiveBalls : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        // Debug.Log(other.gameObject.tag);
        if (other.gameObject.layer == 8)
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;

            //other.transform.GetChild(0).GetComponent<TrailRenderer>().emitting = false;
            other.gameObject.SetActive(false);
            other.transform.parent = GameManager.Instance.deactiveBalls;

        }
    }



}


