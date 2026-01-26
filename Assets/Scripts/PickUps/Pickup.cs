using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    // with const we cant override value and must define it imediately
    const string playerString = "Player";
    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == playerString)
        if (other.CompareTag(playerString))
        {
            //Debug.Log(other.gameObject.name);
            OnPickup();
            Destroy(gameObject);
        }
    }
    protected abstract void OnPickup();
}
