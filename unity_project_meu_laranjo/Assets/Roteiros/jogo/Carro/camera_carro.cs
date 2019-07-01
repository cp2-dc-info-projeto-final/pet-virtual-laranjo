using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class camera_carro : MonoBehaviour//, IPointerDownHandler
{

    public float velz, velmax, sensizmais, sensizmenos, distanciaMax;
    

    public GameObject cam_carro;

    public Quaternion cam_rot;


    SkinnedMeshRenderer sknd_;

    [HideInInspector]
    public bool item = false, item2;

    public bool pode_andar = true, apertando = false, rodar_visao = false;

    public float visao_origem, mouse_origem;
    bool andar;
    Animator ani_;
    public Camera cam_;
    Quaternion direcao;

    public int id_;



    public gerenciador gerenciador_;
    public canva canva_;
    void Start()
    {

        ani_ = gameObject.GetComponent<Animator>();
        //cam_ = gameObject
        canva_ = GameObject.Find("Canvas").GetComponent<canva>();

    }

    // Update is called once per frame
    void Update()
    {
        //---girar camera---

        cam_carro.transform.localRotation = Quaternion.Lerp(cam_carro.transform.localRotation, cam_rot,Time.deltaTime * 8);

        //---mover camera---

        Ray raioMouse = cam_.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)){

            if(!canva.instancia.ClickouUi()){
                if(Input.mousePosition.x > Screen.height/2){
                    mouse_origem = Input.mousePosition.x;
                    visao_origem = cam_rot.eulerAngles.y;
                    apertando = true;
                }
            }
            
        }

        if(Input.GetMouseButtonUp(0)){
            apertando = false;

            if(!rodar_visao){
                cam_rot = Quaternion.Euler(0,0,0);
            }

            rodar_visao = false;
        }

        if(apertando){
            if(Mathf.Abs(Input.mousePosition.x - mouse_origem) > 20){
                rodar_visao = true;
            }
        }

        if(rodar_visao){
            cam_rot = Quaternion.Euler(0,visao_origem + ((Input.mousePosition.x - mouse_origem)/Screen.width * 360),0);
        }

    }


}
