using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
