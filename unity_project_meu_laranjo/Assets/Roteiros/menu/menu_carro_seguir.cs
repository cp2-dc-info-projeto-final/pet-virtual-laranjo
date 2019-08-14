using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class menu_carro_seguir : MonoBehaviour {
    public int indice_carro;
    public Transform target;
    public Camera cam;
    public GameObject carro;
    public RectTransform ui_,ui2_;

    void Start()
    {
        //cam = GetComponent<Camera>();
    }

    void Update()
    {

        if(GameObject.Find("carro_0"+indice_carro.ToString()) != null){
            carro = GameObject.Find("carro_0"+indice_carro.ToString());
            target = GameObject.Find("carro_0"+indice_carro.ToString()).transform.Find("piv_menu");
        

            //cam = Camera.main;
            Vector3 screenPos = cam.WorldToScreenPoint(target.position);
            //Debug.Log("target is " + screenPos.x + " pixels from the left");

            ui_.position = screenPos;
            ui2_.rotation = Quaternion.Euler(0,0,(carro.transform.rotation.eulerAngles.y * -1) + cam.transform.rotation.eulerAngles.y);
        }
    }
}