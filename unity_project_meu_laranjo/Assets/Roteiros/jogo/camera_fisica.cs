using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_fisica : MonoBehaviour
{
    public float distancia = 12;
    public GameObject pivot;
    public int layer_mask;
    // Start is called before the first frame update
    void Start()
    {
        layer_mask = LayerMask.GetMask("Default", "TransparentFX");
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(pivot.transform.position, - pivot.transform.forward,out RaycastHit hit_,distancia,layer_mask)){
            transform.localPosition = new Vector3(0,1.1f, (- Vector3.Distance(pivot.transform.position, hit_.point) + 0.2f));
        }else{
            transform.localPosition = new Vector3(0,1.1f,- distancia);
        }
    }
}
