using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class registrar : MonoBehaviour
{

    UnityWebRequest link;
    public string site, site_ver, nick, email, st1, st2;
    public string[] resposta; // = new List<string>(); 
    public TMP_InputField reg_nick, reg_nome, reg_sobrenome, reg_email, reg_senha1, reg_senha2, reg_nascimento_dia, reg_nascimento_mes, reg_nascimento_ano;
    public TMP_Dropdown reg_lingua;
    public Image botao_senha_imagem;
    public Sprite[] senha_visivel = new Sprite[2];
    public Image[] bordas = new Image[7];
    public bool[] campoOk = new bool[7], mudouValor = new bool[7];
    public bool fim_reg = false;
    public Button botao_registrar;
    public GameObject menu_conf;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(checarCampo("rogerin",2));
    }

    // Update is called once per frame
    void Update()
    {
        if(mudouValor[0]){
            if(reg_nick.text.Length >= 3){
                StartCoroutine(checarCampo(reg_nick.text,2));
                mudouValor[0] = false;
            }else
            {
                campoOk[0] = false;
                bordas[0].color = Color.red;
                mudouValor[0] = false;
            }
        }

        if(mudouValor[1]){
            if(reg_nome.text.Length >= 3){
                campoOk[1] = true;
                bordas[1].color = Color.green;
            }else
            {
                campoOk[1] = false;
                bordas[1].color = Color.red;
            }
        }
        

        if(mudouValor[2]){
            if(reg_sobrenome.text.Length >= 3){
                campoOk[2] = true;
                bordas[2].color = Color.green;
            }else
            {
                campoOk[2] = false;
                bordas[2].color = Color.red;
            }
        }

        if(mudouValor[3]){
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
                    StartCoroutine(checarCampo(reg_email.text,1));
                    mudouValor[3] = false;
                }else
                {
                    campoOk[3] = false;
                    bordas[3].color = Color.red;
                    //mudouValor[3] = false;
                }

                
            }else
            {
                campoOk[3] = false;
                bordas[3].color = Color.red;
                //mudouValor[3] = false;
            }
        }

        if(mudouValor[4]){
        if(reg_senha1.text.Length >= 6){
            campoOk[4] = true;
            bordas[4].color = Color.green;
        }else
        {
            campoOk[4] = false;
            bordas[4].color = Color.red;
        }
        }

        if(mudouValor[5]){
        if(reg_senha2.text.Length >= 6){
            if(reg_senha2.text == reg_senha1.text){
                campoOk[5] = true;
                bordas[5].color = Color.green;
            }else
            {
                campoOk[5] = false;
                bordas[5].color = Color.red;
            }
            
        }else
        {
            campoOk[5] = false;
            bordas[5].color = Color.red;
        }
        }

        


        if(mudouValor[6]){
            if(reg_nascimento_dia.text.Length >= 1 && reg_nascimento_mes.text.Length >= 1 && reg_nascimento_ano.text.Length == 4){
                campoOk[6] = true;
                bordas[6].color = Color.green;
            }else
            {
                campoOk[6] = false;
                bordas[6].color = Color.red;
            }
        }



        if(!fim_reg){
            if(campoOk[0] && campoOk[1] && campoOk[2] && campoOk[3] && campoOk[4] && campoOk[5] && campoOk[6]){
                botao_registrar.interactable = true;
            }else{
                botao_registrar.interactable = false;
            }
        }

        mudarDia();
        mudarMes();
    }

    public void mudarValor(int id_){
        mudouValor[id_] = true;
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

    public void botaoSenha(){
        string aaa1, aaa2;

        if(reg_senha1.inputType == TMP_InputField.InputType.Password){
            reg_senha1.inputType = TMP_InputField.InputType.Standard;
            reg_senha2.inputType = TMP_InputField.InputType.Standard;

            aaa1 = reg_senha1.text;
            aaa2 = reg_senha2.text;

            reg_senha1.text = "";
            reg_senha2.text = "";

            reg_senha1.text = aaa1;
            reg_senha2.text = aaa2;

            botao_senha_imagem.sprite = senha_visivel[1];
        }else
        {
            reg_senha1.inputType = TMP_InputField.InputType.Password;
            reg_senha2.inputType = TMP_InputField.InputType.Password;

            aaa1 = reg_senha1.text;
            aaa2 = reg_senha2.text;

            reg_senha1.text = "";
            reg_senha2.text = "";

            reg_senha1.text = aaa1;
            reg_senha2.text = aaa2;

            botao_senha_imagem.sprite = senha_visivel[0];
        }
    }

    public void botaoRegistrar(){

        string lingua_ = null, nascimento_ = null;



        if(reg_lingua.value == 0){
            lingua_ = "pt-br";
        }

        if(reg_lingua.value == 1){
            lingua_ = "pt-pt";
        }

        if(reg_lingua.value == 2){
            lingua_ = "en-us";
        }

        if(reg_lingua.value == 3){
            lingua_ = "en-uk";
        }

        if(reg_lingua.value == 4){
            lingua_ = "es-mx";
        }

        nascimento_ = (reg_nascimento_ano.text + "-" + reg_nascimento_mes.text.PadLeft(2, '0') + "-" + reg_nascimento_dia.text.PadLeft(2,'0'));



        StartCoroutine(fazerRegistro(reg_nick.text,reg_nome.text,reg_sobrenome.text,reg_email.text,reg_senha1.text,lingua_,nascimento_));
    }

    IEnumerator fazerRegistro(string nick_, string nome_, string sobrenome_, string email_, string senha_, string lingua_, string nascimento_){
        
        WWWForm form = new WWWForm();

        form.AddField("nick", nick_);
        form.AddField("nome", nome_);
        form.AddField("sobrenome", sobrenome_);
        form.AddField("email", email_);
        form.AddField("senha", senha_);
        form.AddField("lingua", lingua_);
        form.AddField("nascimento", nascimento_);

        link = UnityWebRequest.Post(site,form);

        fim_reg = true;

        botao_registrar.interactable = false;

        yield return link.SendWebRequest();

        resposta = link.downloadHandler.text.Split(',');
        
        botao_registrar.interactable = true;

        fim_reg = false;

        if(resposta[1] == "1"){
            menu_conf.SetActive(true);

            reg_nick.text = "";
            reg_nome.text = "";
            reg_sobrenome.text = "";
            reg_email.text = "";
            reg_senha1.text = "";
            reg_senha2.text = "";
            reg_nascimento_dia.text = "";
            reg_nascimento_mes.text = "";
            reg_nascimento_ano.text = "";
            
            menu_conf.GetComponent<confirmar>().id = resposta[2];
        }

    }

    IEnumerator checarCampo(string campo_, int verif_){
        
        //Debug.Log("checando " + verif_);

        WWWForm form = new WWWForm();
        form.AddField("campo", campo_);
        form.AddField("verif", verif_.ToString());

        link = UnityWebRequest.Post(site_ver,form);

        //carregando = true;

        if(verif_ == 1){
            bordas[3].color = new Color(1,0.5f,0);
        }

        if(verif_ == 2){
            bordas[0].color = new Color(1,0.5f,0);
        }

        yield return link.SendWebRequest();

        //carregando = false;

        if(link.isNetworkError || link.isHttpError){
            if(verif_ == 1){
                bordas[3].color = Color.red;
            }

            if(verif_ == 2){
                bordas[0].color = Color.red;
            }
        }else
        {
            resposta = link.downloadHandler.text.Split(',');

            if(verif_ == 1){

                if(resposta[0] == "0"){
                    bordas[3].color = Color.red;
                    campoOk[3] = false;
                }

                if(resposta[0] == "1"){
                    bordas[3].color = Color.green;
                    campoOk[3] = true;
                }

                if(resposta[0] == "2"){
                    bordas[3].color = Color.red;
                    campoOk[3] = false;
                }

                if(resposta[0] == "99"){
                    bordas[3].color = Color.red;
                    campoOk[3] = false;
                }
                
            }

            if(verif_ == 2){

                if(resposta[0] == "1"){
                    bordas[0].color = Color.green;
                    campoOk[0] = true;
                }

                if(resposta[0] == "2"){
                    bordas[0].color = Color.red;
                    campoOk[0] = false;
                }

                if(resposta[0] == "99"){
                    bordas[0].color = Color.red;
                    campoOk[0] = false;
                }
            }
        }

    }
}
