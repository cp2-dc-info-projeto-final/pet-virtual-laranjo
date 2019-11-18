using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class veiculo : MonoBehaviour
{
    public float SHOW_torque1, SHOW_torque2;
    public float SHOW_B_torque1, SHOW_B_torque2;

    public float SHOW_RPM_1, SHOW_RPM_2;

    public GameObject[] entrada;
    public GameObject menu_entrar;
    public botao_ui[] botao_ui_;

    public int motor = 1, cambio = 1, freios = 1, tracao = 1;

    public float sensibilidade_frente = 8, sensibilidade_traz = 4; 

    public float peso = 1500, torque = 1000, rmp_maximo = 230, forca_freios = 1200;
    public Transform[] transf_roda;
    public WheelCollider[] coll_roda;
    Rigidbody rb_;

    //[HideInInspector]
    public bool ligado = false, teclado, press_dir, press_esq, press_fre, press_tra;
    public float angulo, direcao, dir_esq, dir_dir, velocidade, vel_fre, vel_tra, freio_natural;
    public bool frfr = false;
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
        rmp_maximo = 200 + (motor - 1) * (600 / 9);

        torque = 800 + (cambio - 1) * (5000 / 9);

        forca_freios = 1200 + (freios - 1) * (3000 / 9);

        foreach(WheelCollider rod_ in coll_roda){

            WheelFrictionCurve curvaF_ = new WheelFrictionCurve();

            curvaF_.extremumSlip = 0.4f + (tracao - 1) * (0.6f / 9);
            curvaF_.extremumValue = 1 + (tracao - 1) * (1f / 9);
            curvaF_.asymptoteSlip = 0.8f + (tracao - 1) * (0.2f / 9);
            curvaF_.asymptoteValue = 0.5f + (tracao - 1) * (1.5f / 9);
            curvaF_.stiffness = 1 + (tracao - 1) * (1f / 9);

            rod_.forwardFriction = curvaF_;

            WheelFrictionCurve curvaS_ = new WheelFrictionCurve();

            curvaS_.extremumSlip = 0.2f + (tracao - 1) * (0.8f / 9);
            curvaS_.extremumValue = 1 + (tracao - 1) * (1f / 9);
            curvaS_.asymptoteSlip = 0.5f + (tracao - 1) * (0.5f / 9);
            curvaS_.asymptoteValue = 0.75f + (tracao - 1) * (1.25f / 9);
            curvaS_.stiffness = 1 + (tracao - 1) * (2f / 9);

            rod_.sidewaysFriction = curvaS_;
        }
        

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

        /*
        if(entrada[0].transform.childCount == 1){
            ligado = true;
        }else
        {
            ligado = false;
        }
        */

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
            dir_esq -= Time.deltaTime * 12;
        }else
        {
            dir_esq += Time.deltaTime * 16;
        }
        
        if(press_dir){
            dir_dir += Time.deltaTime * 12;
        }else
        {
            dir_dir -= Time.deltaTime * 16;
        }

        dir_esq = Mathf.Clamp(dir_esq,-1,0);
        dir_dir = Mathf.Clamp(dir_dir,0,1);

        direcao = dir_esq + dir_dir;


        if(press_fre){
            vel_fre += Time.deltaTime * sensibilidade_frente;
        }else
        {
            vel_fre -= Time.deltaTime * (sensibilidade_frente / 2);
        }
        
        if(press_tra){
            vel_tra -= Time.deltaTime *sensibilidade_frente;
        }else
        {
            vel_tra += Time.deltaTime * (sensibilidade_frente / 2);
        }

        vel_tra = Mathf.Clamp(vel_tra,-1,0);
        vel_fre = Mathf.Clamp(vel_fre,0,1);

        velocidade = vel_fre + vel_tra;

        angulo = Mathf.Lerp(angulo,direcao,Time.deltaTime * 4);

        coll_roda[0].steerAngle = angulo * 25;
        coll_roda[1].steerAngle = angulo * 25;

        

        

        if(!press_fre && !press_tra){
            coll_roda[2].brakeTorque = freio_natural;
            coll_roda[3].brakeTorque = freio_natural;

            frfr = true;
        }else
        {

            coll_roda[2].brakeTorque = 0;
            coll_roda[3].brakeTorque = 0;
                  

            frfr = false;
        }

        if(transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).z > 0 && press_tra){
            coll_roda[2].brakeTorque = forca_freios;
            coll_roda[3].brakeTorque = forca_freios;
        }


        if(((coll_roda[2].rpm + coll_roda[3].rpm) / 2) < rmp_maximo && ((coll_roda[2].rpm + coll_roda[3].rpm) / 2) > - rmp_maximo / 2){
            coll_roda[2].motorTorque = velocidade*torque;
            coll_roda[3].motorTorque = velocidade*torque;
        }else
        {
            coll_roda[2].motorTorque = 0;
            coll_roda[3].motorTorque = 0;
        }
        

        for(int i = 0; i < coll_roda.Length; i++){
            Quaternion rot_;
            Vector3 pos_;
            coll_roda[i].GetWorldPose(out pos_,out rot_);
            transf_roda[i].position = pos_;
            transf_roda[i].rotation = rot_;
        }

        SHOW_torque1 = coll_roda[2].motorTorque;
        SHOW_torque2 = coll_roda[3].motorTorque;

        SHOW_B_torque1 = coll_roda[2].brakeTorque;
        SHOW_B_torque2 = coll_roda[3].brakeTorque;

        SHOW_RPM_1 = coll_roda[2].rpm;
        SHOW_RPM_2 = coll_roda[3].rpm;

    }
}
