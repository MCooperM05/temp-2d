using UnityEngine;

public class EnterArea : MonoBehaviour
{
    public Camera exploreCamera;
    public Transform defaultTransform = null;
    public Transform areaTransform;
    public int defaultSize = 5;
    public int areaSize;


    void Start()
    {
        if (defaultTransform == null)
        {
            defaultTransform = exploreCamera.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            exploreCamera.transform.SetPositionAndRotation(areaTransform.position, areaTransform.rotation);
            exploreCamera.orthographicSize = areaSize;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            exploreCamera.transform.SetPositionAndRotation(defaultTransform.position, defaultTransform.rotation);
            exploreCamera.orthographicSize = defaultSize;
        }
    }
}
