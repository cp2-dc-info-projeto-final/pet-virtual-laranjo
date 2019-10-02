using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class registrar : MonoBehaviour
{
    public TMP_InputField reg_nick, reg_nome, reg_sobrenome, reg_email, reg_senha1, reg_senha2, reg_nascimento_dia, reg_nascimento_mes, reg_nascimento_ano;
    public TMP_Dropdown reg_lingua;
    public bool[] campoOk = new bool[7];

    public Button botao_registrar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(reg_nick.text.Length >= 3){
            campoOk[0] = true;
        }else
        {
            campoOk[0] = false;
        }


        if(reg_nome.text.Length >= 3){
            campoOk[1] = true;
        }else
        {
            campoOk[1] = false;
        }


        if(reg_sobrenome.text.Length >= 3){
            campoOk[2] = true;
        }else
        {
            campoOk[2] = false;
        }


        if(reg_email.text.Length >= 6){

            bool tem_arroba = false, tem_ponto = false;
            int quant_arroba = 0;

            for(int i_ = 0; i_ < reg_email.text.Length; i_ ++){
                if(reg_email.text[i_] == '@'){
                    tem_arroba = true;
                    quant_arroba ++;
                }
            }

            for(int i_ = 0; i_ < reg_email.text.Length; i_ ++){
                if(reg_email.text[i_] == '.'){
                    tem_ponto = true;
                }
            }

            if(tem_arroba && tem_ponto && quant_arroba == 1){
                campoOk[3] = true;
            }else
            {
                campoOk[3] = false;
            }

            
        }else
        {
            campoOk[3] = false;
        }


        if(reg_senha1.text.Length >= 6){
            campoOk[4] = true;
        }else
        {
            campoOk[4] = false;
        }


        if(reg_senha2.text.Length >= 6){
            campoOk[5] = true;
        }else
        {
            campoOk[5] = false;
        }

        



        if(reg_nascimento_dia.text.Length >= 1 && reg_nascimento_mes.text.Length >= 1 && reg_nascimento_ano.text.Length == 4){
            campoOk[6] = true;
        }else
        {
            campoOk[6] = false;
        }

        if(campoOk[0] && campoOk[1] && campoOk[2] && campoOk[3] && campoOk[4] && campoOk[5] && campoOk[6]){
            botao_registrar.interactable = true;
        }else{
            botao_registrar.interactable = false;
        }

        mudarDia();
        mudarMes();
    }

    public void mudarDia(){
        if(reg_nascimento_dia.text != ""){
            if(int.Parse(reg_nascimento_dia.text) > 31 || int.Parse(reg_nascimento_dia.text) < 1){
                if(int.Parse(reg_nascimento_dia.text) < 1){
                    reg_nascimento_dia.text = "1";
                }

                if(int.Parse(reg_nascimento_dia.text) > 31){
                    reg_nascimento_dia.text = "31";
                }
                
            }
            
            if(reg_nascimento_mes.text != ""){
                if(int.Parse(reg_nascimento_mes.text) == 4 || int.Parse(reg_nascimento_mes.text) == 6 || int.Parse(reg_nascimento_mes.text) == 9 || int.Parse(reg_nascimento_mes.text) == 11){
                    if(int.Parse(reg_nascimento_dia.text) > 30){
                        reg_nascimento_dia.text = "30";
                    }
                }

                if(int.Parse(reg_nascimento_mes.text) == 2){
                    if(reg_nascimento_ano.text != ""){
                        if(int.Parse(reg_nascimento_ano.text) % 4 == 0){
                            if(int.Parse(reg_nascimento_dia.text) > 29){
                                reg_nascimento_dia.text = "29";
                            }
                        }else
                        {
                            if(int.Parse(reg_nascimento_dia.text) > 28){
                                reg_nascimento_dia.text = "28";
                            }
                        }
                    }
                }
            }
        
        }
    }

    public void mudarMes(){
        if(reg_nascimento_mes.text != ""){
            if(int.Parse(reg_nascimento_mes.text) > 12){
                reg_nascimento_mes.text = "12";
            }
            if(int.Parse(reg_nascimento_mes.text) < 1){
                reg_nascimento_mes.text = "1";
            }
        }
    }
}
