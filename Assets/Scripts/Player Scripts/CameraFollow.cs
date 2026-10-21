using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector2 offset = Vector2.zero;

    [Header("Map Edges")]
    public Transform mapBottomLeft;
    public Transform mapTopRight;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (target == null)
        {
            PlayerController player = FindFirstObjectByType<PlayerController>();
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 goal = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 newPos = Vector3.Lerp(transform.position, goal, smoothSpeed * Time.deltaTime);

        if (mapBottomLeft != null & mapTopRight != null)
        {
            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;

            float minX = mapBottomLeft.position.x + halfWidth;
            float maxX = mapTopRight.position.x - halfHeight;
            float minY = mapBottomLeft.position.y + halfHeight;
            float maxY = mapTopRight.position.y - halfWidth;

            if (minX < maxX)
            {
                newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            } else
            {
                newPos.x = (mapBottomLeft.position.x + mapTopRight.position.x) / 2f;
            }

            if (minY < maxY)
            {
                newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
            } else
            {
                newPos.y = (mapBottomLeft.position.y + mapTopRight.position.y) / 2f;
            }
        }

        transform.position = newPos;
    }
}
