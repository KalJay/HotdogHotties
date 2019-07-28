using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 velocity;

    public float smoothTimeY;
    public float smoothTimeX;
    public float smoothTimeZ;

    public float spaceX;
    public float spaceY;
    public float spaceZ;

    public GameObject player;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + spaceX, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + spaceY, ref velocity.y, smoothTimeY);
        float posZ = Mathf.SmoothDamp(transform.position.z, player.transform.position.z - spaceZ, ref velocity.z, smoothTimeZ);

        transform.position = new Vector3(posX, posY, posZ);

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                                             Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                                             Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }
    }

    public void SetMinCamPosition()
    {
        minCameraPos = gameObject.transform.position;
    }

    public void SetMaxCamPosition()
    {
        maxCameraPos = gameObject.transform.position;
    }
}
