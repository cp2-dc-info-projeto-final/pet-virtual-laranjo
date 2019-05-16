using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class movimento : MonoBehaviour//, IPointerDownHandler
{
    public Vector3 destino;
    public float velz, velmax, sensizmais, sensizmenos, distanciaMax;


    SkinnedMeshRenderer sknd_;

    [HideInInspector]
    public bool item = false, item2;
    bool andar;
    Animator ani_;
    public Camera cam_;
    Quaternion direcao;

    public int id_;

    public gerenciador gerenciador_;
    public canva canva_;
    void Start()
    {
        destino = transform.position;

        ani_ = gameObject.GetComponent<Animator>();
        //cam_ = gameObject
        canva_ = GameObject.Find("Canvas").GetComponent<canva>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            gerenciador_.colocaritemArmario(id_);
        }

        Ray raioMouse = cam_.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)){
            if(canva_.ClickouUi()){
                if(Physics.Raycast(raioMouse, out RaycastHit hit_)){
                    //Debug.Log(hit_.transform.tag);

                    if(hit_.transform.tag == "chao"){
                        destino = hit_.point;
                    }
                }
            }
        }

        if(Vector3.Distance(destino, transform.position) > distanciaMax){
            andar = true;
        }else
        {
            andar = false;
        }

        if(andar){
            
            velz += sensizmais * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation,direcao,0.1f * velz);
        }else
        {
            velz -= sensizmenos * Time.deltaTime;
        }

        velz = Mathf.Clamp(velz,0f,1f);

        direcao = Quaternion.LookRotation(destino - transform.position,Vector3.up);

        

        transform.Translate(0,0,velz * Time.deltaTime * velmax);

        ani_.SetFloat("velz", velz);

        ani_.SetBool("item", item);
    }


}
