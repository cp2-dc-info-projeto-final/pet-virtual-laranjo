using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_seguir_carro : MonoBehaviour
{
    public GameObject carro_;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, carro_.transform.position,Time.deltaTime * 2);
        transform.localRotation = Quaternion.Euler(0,Mathf.LerpAngle(transform.rotation.eulerAngles.y,carro_.transform.rotation.eulerAngles.y,Time.deltaTime * 1),0);

    }
}
