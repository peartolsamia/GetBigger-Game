using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private Transform previousScene;
    [SerializeField] private Transform nextScene;
    [SerializeField] private CameraController cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if ((collision.transform.position.x < transform.position.x))
            {
                cam.MoveToNewScene(nextScene);
            }
            else
            {
                cam.MoveToNewScene(previousScene);
            }
        }

    }

}
