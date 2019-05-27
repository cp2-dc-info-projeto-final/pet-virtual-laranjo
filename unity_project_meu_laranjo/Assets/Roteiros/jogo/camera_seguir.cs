using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_seguir : MonoBehaviour
{
    public GameObject laranjo_;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, laranjo_.transform.position,0.5f);
    }
}
