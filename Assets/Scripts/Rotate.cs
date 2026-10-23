using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    // Constantly rotate an object
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
