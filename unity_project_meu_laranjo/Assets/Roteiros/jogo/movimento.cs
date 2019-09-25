using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class movimento : MonoBehaviour//, IPointerDownHandler
{
    public TMP_Text textoX;
    public GameObject destino, prefab_destino;
    public float velz, velmax, sensizmais, sensizmenos, distanciaMax;
    

    public GameObject cam_fora, cam_fora2, entrada_carro, controle_carro;

    public Quaternion cam_rot, cam_rot2;


    SkinnedMeshRenderer sknd_;

    [HideInInspector]
    public bool item = false, item2;

    public bool pode_andar = true, apertando = false, rodar_visao = false, apertou_ui = false;

    public float visao_origem, visao_origem2, mouse_origem, mouse_origem2, quantidade_rot = 0;
    public bool andar, indo_carro = false, dentro_carro= false, entrou_carro = false;
    Animator ani_;
    public Camera cam_;
    Quaternion direcao;

    public int id_, indice_carro;
    [HideInInspector]
    public Collider col_;
    [HideInInspector]
    public Rigidbody rb_;
    public GameObject HITHIT_;
    int layer_mask;


    void Start()
    {

        layer_mask = LayerMask.GetMask("Default", "TransparentFX");

        destino = Instantiate(prefab_destino, transform.position + new Vector3(0,0,0.1f),Quaternion.Euler(0,0,0));

        col_ = gameObject.GetComponent<Collider>();

        rb_ = gameObject.GetComponent<Rigidbody>();

        ani_ = gameObject.GetComponent<Animator>();
        //cam_ = gameObject
        canva.instancia = GameObject.Find("Canvas").GetComponent<canva>();

    }

    // Update is called once per frame
    void Update()
    {
        cam_fora.transform.localRotation = Quaternion.Lerp(cam_fora.transform.localRotation ,cam_rot, Time.deltaTime * 8);

        cam_fora2.transform.localRotation = Quaternion.Lerp(cam_fora2.transform.localRotation ,cam_rot2, Time.deltaTime * 8);

        if(Input.GetKeyDown(KeyCode.E)){
            gerenciador.instancia.colocaritemArmario(id_);
        }


        //---mover camera ou andar---

        Ray raioMouse = cam_.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0)){
            if(!canva.instancia.ClickouUi()){
                if(!dentro_carro){
                    mouse_origem = Input.mousePosition.x;
                    visao_origem = cam_rot.eulerAngles.y;

                    mouse_origem2 = Input.mousePosition.y;
                    visao_origem2 = cam_rot2.eulerAngles.x;
                    apertando = true;
                }
                
            }else
            {
                apertou_ui = true;
            }
            
        }

        if(Input.GetMouseButtonUp(0)){
            apertando = false;

            if(!rodar_visao){
                if(pode_andar){
                    if(!canva.instancia.ClickouUi()){
                        if(Physics.Raycast(raioMouse, out RaycastHit hit_,Mathf.Infinity,layer_mask)){

                            HITHIT_ = hit_.transform.gameObject;
                            Debug.Log("H I T H I T + +");
                            //Debug.Log(hit_.transform.tag);
                            if(!apertou_ui){
                                if(hit_.transform.tag == "chao"){
                                    foreach(GameObject dest_ in GameObject.FindGameObjectsWithTag("destino")){
                                        Destroy(dest_);
                                    }
                                    destino = Instantiate(prefab_destino,hit_.point,Quaternion.Euler(0,0,0));

                                    foreach(GameObject car_ in gerenciador.instancia.carros){
                                        if(car_ != null){
                                            car_.GetComponent<veiculo>().menu_entrar.SetActive(false);
                                        }
                                    }
                                    //GameObject.FindGameObjectWithTag("carro").GetComponent<veiculo>().menu_entrar.SetActive(false);
                                    indo_carro = false;
                                }

                                if(hit_.transform.tag == "carro"){
                                    //hit_.transform.gameObject.GetComponent<veiculo>().menu_entrar.SetActive(true);

                                    gerenciador.instancia.carros[int.Parse(hit_.transform.name.Split('_')[1])].GetComponent<veiculo>().menu_entrar.SetActive(true);
                                    
                                    indice_carro = int.Parse(hit_.transform.name.Split('_')[1]);
                                }
                            }else
                            {
                                apertou_ui = false;
                            }
                        }
                    }
                }
            }

            rodar_visao = false;
        }

        if(apertando){
            textoX.text = string.Format("{0:0.#}", Input.mousePosition.x);
            if(Mathf.Abs(Input.mousePosition.x - mouse_origem) > 8 || Mathf.Abs(Input.mousePosition.y - mouse_origem2) > 8){
                rodar_visao = true;
            }
        }

        if(rodar_visao){
            cam_rot = Quaternion.Euler(0,visao_origem + ((Input.mousePosition.x - mouse_origem)/Screen.width * 360),0);

            

            quantidade_rot += (mouse_origem2 - Input.mousePosition.y) / 3;

            mouse_origem2 = Input.mousePosition.y;

            quantidade_rot = Mathf.Clamp(quantidade_rot,0f,90f);

            cam_rot2 = Quaternion.Euler(quantidade_rot,0,0);
            //cam_rot2 = Quaternion.Euler(90-Mathf.Clamp(visao_origem2 + ((Input.mousePosition.y - mouse_origem2)/Screen.width * 360),0,90),0,0);

            /*if(cam_rot2.eulerAngles.x <= 0){
                mouse_origem2 = Input.mousePosition.y;
                visao_origem2 = cam_rot2.eulerAngles.x;
            }
            */
        }

        if(pode_andar){
            if(!indo_carro){
                if(Vector3.Distance(new Vector3(destino.transform.position.x,0,destino.transform.position.z), new Vector3(transform.position.x,0,transform.position.z)) > distanciaMax){
                    andar = true;
                }else
                {
                    andar = false;
                    if(GameObject.FindGameObjectWithTag("destino") != null){
                        foreach(GameObject dest_ in GameObject.FindGameObjectsWithTag("destino")){
                            if(dest_.GetComponent<seta_dest>().seta != null){
                                Destroy(dest_.GetComponent<seta_dest>().seta);
                            }
                        }
                    }
                    
                    
                }
            }

            if(andar){
                
                velz += sensizmais * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation,direcao,0.1f);
            }else
            {
                velz -= sensizmenos * Time.deltaTime;
            }

            
            if(indo_carro){
                if(entrada_carro.transform.childCount == 0){

                
                destino = entrada_carro;

                if(Vector3.Distance(new Vector3(destino.transform.position.x,0,destino.transform.position.z), new Vector3(transform.position.x,0,transform.position.z)) > distanciaMax){
                    andar = true;
                }else
                {
                    andar = false;
                }

                if(!andar){
                    transform.SetParent(entrada_carro.transform);
                    dentro_carro = true;
                    entrou_carro = true;
                    indo_carro = false;

                    //gerenciador.instancia.cameras[2].transform.SetParent(gerenciador.instancia.carros[indice_carro].transform);
                    gerenciador.instancia.cameras[2].GetComponent<camera_carro>().carro_ = gerenciador.instancia.carros[indice_carro];
                    gerenciador.instancia.cameras[2].transform.localPosition = new Vector3(0,0,0);
                    //gerenciador.instancia.cameras[2].transform.localRotation = Quaternion.Euler(0,0,0);
                    gerenciador.instancia.cameras[2].GetComponent<camera_carro>().cam_rot =  Quaternion.Euler(0,0,0);

                    gerenciador.instancia.mudar_camera("camera_carro");
                }
            }else
            {
                destino = Instantiate(prefab_destino,transform.position,Quaternion.Euler(0,0,0));
                indo_carro = false;
            }
            }

            velz = Mathf.Clamp(velz,0f,1f);

            direcao = Quaternion.LookRotation(new Vector3(destino.transform.position.x,0,destino.transform.position.z) - new Vector3(transform.position.x,0,transform.position.z),Vector3.up);

            

            transform.Translate(0,0,velz * Time.deltaTime * velmax);

            ani_.SetFloat("velz", velz);

            ani_.SetBool("item", item);
        }

        if(dentro_carro){

            pode_andar = false;
            transform.localRotation = Quaternion.Lerp(transform.localRotation,Quaternion.Euler(0,0,0),Time.deltaTime * 8);
            transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3 (0,0,0),Time.deltaTime * 8);
            if(entrou_carro){
                //ani_.applyRootMotion = false;
                //col_.isTrigger = true;
                col_.enabled = false;
                rb_.constraints = RigidbodyConstraints.FreezePosition;
                ani_.SetTrigger("entrar_esq");
                entrou_carro = false;
            }
            
            GameObject.Find("controle_carro_0"+indice_carro).transform.Find("controle").gameObject.SetActive(true);
            //controle_carro.SetActive(true);

        }

        
    }

    public void botao_menu_carro(int num_entrada_){
        foreach(GameObject dest_ in GameObject.FindGameObjectsWithTag("destino")){
            Destroy(dest_);
        }
        if(gerenciador.instancia.carros[indice_carro].GetComponent<veiculo>().entrada[num_entrada_].transform.childCount == 0){
            entrada_carro = gerenciador.instancia.carros[indice_carro].GetComponent<veiculo>().entrada[num_entrada_];
            indo_carro = true;
        }else{
            destino = Instantiate(prefab_destino,transform.position,Quaternion.Euler(0,0,0));
        }
        
        foreach (GameObject car_ in gerenciador.instancia.carros)
        {
            if(car_ != null){
                car_.GetComponent<veiculo>().menu_entrar.SetActive(false);
            }
        }
    }

    public void botao_sair_carro(){
        //col_.isTrigger = false;
        col_.enabled = true;
        rb_.constraints = RigidbodyConstraints.None;
        rb_.constraints = RigidbodyConstraints.FreezeRotation;


        // desativar controles de carro
        for (int i = 1; i <= 3; i++){
            GameObject.Find("controle_carro_0" + i).transform.Find("controle").gameObject.SetActive(false);

            if(gerenciador.instancia.carros[i] != null){
                gerenciador.instancia.carros[i].transform.position = gerenciador.instancia.casa_pivot[i].transform.position;
                gerenciador.instancia.carros[i].transform.rotation = Quaternion.Euler(0,0,0);//gerenciador.instancia.casa_pivot[i].transform.rotation;

                gerenciador.instancia.carros[i].GetComponent<Rigidbody>().velocity = Vector3.zero;

                foreach (WheelCollider roda_ in gerenciador.instancia.carros[i].GetComponent<veiculo>().coll_roda)
                {
                    roda_.brakeTorque = Mathf.Infinity;
                }
            }
        }

        ani_.SetTrigger("sair_esq");
        entrada_carro.transform.DetachChildren();
        dentro_carro = false;
        pode_andar = true;
        destino = Instantiate(prefab_destino,transform.position,Quaternion.Euler(0,0,0));
        gerenciador.instancia.mudar_camera("camera_fora");

        gerenciador.instancia.restartTerrenos();
    }

}
