using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform target;   
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        FindPlayer();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            target = playerObj.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition =
            Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(
            smoothedPosition.x,
            smoothedPosition.y,
            transform.position.z
        );
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
