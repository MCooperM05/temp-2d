using UnityEditor.SearchService;
using UnityEngine;

public class EnterArea : MonoBehaviour
{
    public Camera exploreCamera;
    public Transform defaultTransform = null;
    public Transform areaTransform;
    public int defaultSize = 5;
    public int areaSize;
    public GameObject[] texturesForRoom;
    public GameObject[] entranceColliders;


    void Start()
    {
        if (defaultTransform == null)
        {
            defaultTransform = exploreCamera.transform;
        }
        if (exploreCamera == null)
        {
            GameObject camObj = GameObject.FindWithTag("MainCamera");
            exploreCamera = camObj.GetComponent<Camera>();
        }
        for (int i = 0; i < texturesForRoom.Length; i++)
        {
            texturesForRoom[i].SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            exploreCamera.transform.SetPositionAndRotation(areaTransform.position, areaTransform.rotation);
            exploreCamera.orthographicSize = areaSize;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < texturesForRoom.Length; i++)
            {
                texturesForRoom[i].SetActive(true);
            }
            for (int i = 0; i < entranceColliders.Length; i++)
            {
                entranceColliders[i].SetActive(false);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            exploreCamera.transform.SetPositionAndRotation(defaultTransform.position, defaultTransform.rotation);
            exploreCamera.orthographicSize = defaultSize;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            for (int i = 0; i < texturesForRoom.Length; i++)
            {
                texturesForRoom[i].SetActive(false);
            }
            for (int i = 0; i < entranceColliders.Length; i++)
            {
                entranceColliders[i].SetActive(false);
            }
        }
    }
}
