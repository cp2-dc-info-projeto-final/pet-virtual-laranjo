using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class veiculo : MonoBehaviour
{
    public GameObject[] entrada;
    public GameObject menu_entrar;
    public botao_ui[] botao_ui_;
    public float peso = 1500, tracao,torque = 1000 ,cambio, suspencao;
    public Transform[] transf_roda;
    public WheelCollider[] coll_roda;
    Rigidbody rb_;

    //[HideInInspector]
    public bool ligado = false, teclado, press_dir, press_esq, press_fre, press_tra;
    public float angulo, direcao, dir_esq, dir_dir, velocida, vel_fre, vel_tra;
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
        rb_.mass = peso;

        Debug.Log("controle_carro_" + gameObject.name.Split('_')[1]);
        botao_ui_ = GameObject.Find("controle_carro_" + gameObject.name.Split('_')[1]).transform.GetChild(0).gameObject.GetComponent<lista_botoes_ui>().lista;
        menu_entrar = GameObject.Find("menu_entrar_carro_" + gameObject.name.Split('_')[1]).transform.GetChild(0).gameObject;
        gerenciador.instancia.carros[int.Parse(gameObject.name.Split('_')[1])] = gameObject;

        for (int i = 0; i <= 3; i++)
        {
            menu_entrar.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i < transform.GetChild(transform.childCount - 1).GetComponent<chassi>().entradas.Length; i++)
        {
            
            menu_entrar.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
        
        
        //botao_ui_ = GameObject.Find("controle_carro_" + gameObject.name.Split('_')[1]).GetComponent<lista_botoes_ui>().lista;
    }

    // Update is called once per frame
    void Update()
    {
        if(botao_ui_[0] == null){
        
        } 

        if(menu_entrar == null){
            
        }


        if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.A)){
            teclado = true;
        }

        if(botao_ui_[0].pressionado||botao_ui_[1].pressionado||botao_ui_[2].pressionado||botao_ui_[3].pressionado){
            teclado = false;
        }

        if(entrada[0].transform.childCount == 1){
            ligado = true;
        }else
        {
            ligado = false;
        }

        if(ligado){
            if(teclado){
                if(Input.GetKey(KeyCode.W)){
                    press_fre = true;
                }else
                {
                    press_fre = false;
                }
                if(Input.GetKey(KeyCode.S)){
                    press_tra = true;
                }else
                {
                    press_tra = false;
                }
                if(Input.GetKey(KeyCode.A)){
                    press_esq = true;
                }else
                {
                    press_esq = false;
                }
                if(Input.GetKey(KeyCode.D)){
                    press_dir = true;
                }else
                {
                    press_dir = false;
                }
            }else{

                if(botao_ui_[3].pressionado){
                    press_fre = true;
                }else
                {
                    press_fre = false;
                }
                if(botao_ui_[2].pressionado){
                    press_tra = true;
                }else
                {
                    press_tra = false;
                }
                if(botao_ui_[0].pressionado){
                    press_esq = true;
                }else
                {
                    press_esq = false;
                }
                if(botao_ui_[1].pressionado){
                    press_dir = true;
                }else
                {
                    press_dir = false;
                }
            }
        }
        

        if(press_esq){
            dir_esq -= Time.deltaTime;
        }else
        {
            dir_esq += Time.deltaTime * 8;
        }
        
        if(press_dir){
            dir_dir += Time.deltaTime;
        }else
        {
            dir_dir -= Time.deltaTime * 8;
        }

        dir_esq = Mathf.Clamp(dir_esq,-1,0);
        dir_dir = Mathf.Clamp(dir_dir,0,1);

        direcao = dir_esq + dir_dir;


        if(press_fre){
            vel_fre += Time.deltaTime * 6;
        }else
        {
            vel_fre -= Time.deltaTime;
        }
        
        if(press_tra){
            vel_tra -= Time.deltaTime *6;
        }else
        {
            vel_tra += Time.deltaTime;
        }

        vel_tra = Mathf.Clamp(vel_tra,-1,0);
        vel_fre = Mathf.Clamp(vel_fre,0,1);

        velocida = vel_fre + vel_tra;

        angulo = Mathf.Lerp(angulo,direcao,Time.deltaTime * 4);

        coll_roda[0].steerAngle = angulo * 45;
        coll_roda[1].steerAngle = angulo * 45;

        coll_roda[2].motorTorque = velocida*torque;
        coll_roda[3].motorTorque = velocida*torque;

        for(int i = 0; i < coll_roda.Length; i++){
            Quaternion rot_;
            Vector3 pos_;
            coll_roda[i].GetWorldPose(out pos_,out rot_);
            transf_roda[i].position = pos_;
            transf_roda[i].rotation = rot_;
        }

    }
}
