using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed; // camera movement speed (increase to slower movement)
    private float currentPosX; // which position should camera go
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float targetX = Mathf.Clamp(currentPosX, minX, maxX); 
        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, speed); // parameters SmoothDamp(1, 2, 3, 4) : 1 -> current position, 2 -> destination position, 3 -> velocity, 4 -> speed
    }

    public void MoveToNewScene(Transform _newScene)
    {
        currentPosX = _newScene.position.x;
    }
}
