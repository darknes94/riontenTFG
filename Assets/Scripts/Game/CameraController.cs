using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform follow;
    private Vector2 minCamPos, maxCamPos;
    float FollowSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*float posX = follow.position.x;
        float posY = follow.position.y;

        transform.position = new Vector3(
            posX, //Mathf.Clamp(posX, minCamPos.x, maxCamPos.x)
            posY, //Mathf.Clamp(posY, minCamPos.y, maxCamPos.y)
            transform.position.z);*/

        Vector3 newPosition = follow.position;
        newPosition.z = -10;
        transform.position = Vector3.Slerp(transform.position, newPosition,
            FollowSpeed * Time.deltaTime);
    }
}
