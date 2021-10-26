using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // reference to the player object
    public GameObject player;
    // store the offset distance
    private Vector3 offset;

    void Start()
    {
        // initialization
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's
        transform.position = player.transform.position+offset;
    }
}
