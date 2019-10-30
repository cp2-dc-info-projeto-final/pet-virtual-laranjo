using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class logar : MonoBehaviour
{
    UnityWebRequest link_log;
    public TMP_InputField log_nick, log_senha;
    public string site_log, nick, senha, st1, st2;
    public string[] resposta; // = new List<string>(); 
    public bool carregando = false;
    public Slider barra_carregamento;
    public GameObject avisoCarregando, menu_conf, menu_logreg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        avisoCarregando.SetActive(carregando);

        if(carregando){
            barra_carregamento.value = Mathf.Lerp(barra_carregamento.value, (link_log.uploadProgress + link_log.downloadProgress) / 2,Time.deltaTime * 4);
        }
    }

    public void botao_login(){
        nick = log_nick.text;
        senha = log_senha.text;

        barra_carregamento.value = 0.0f;
        StartCoroutine(fazerLogin(nick,senha));
    }

    IEnumerator fazerLogin(string login_, string senha_){

        //Debug.Log("aaaa");
        

        WWWForm form = new WWWForm();
        form.AddField("nickPost", login_);
        form.AddField("senhaPost", senha_);

        link_log = UnityWebRequest.Post(site_log,form);

        carregando = true;

        yield return link_log.SendWebRequest();

        carregando = false;

        if(link_log.isNetworkError || link_log.isHttpError){

            Debug.Log("erro na rede (" + link_log.error + ")");

            avisoCarregando.SetActive(false);
            
        }else
        {
            resposta = link_log.downloadHandler.text.Split(',');

            if(resposta[0]=="0"){
                // usuario nao encontrado

                Debug.Log("usuario nao encontrado");
                
                log_nick.text = "";
                log_senha.text = "";
            }

            if(resposta[0]=="1"){
                // login bem sucedido
                
                if(resposta[1] == "1"){
                    //dados existentes

                    gerDados.instancia.dados_.id = long.Parse(resposta[2]);
                    gerDados.instancia.dados_.ult_ctt = "2001-10-28 22:10:04";

                    PlayerPrefs.SetInt("logado", 1);

                    gerDados.instancia.baixarDados();

                    Debug.Log("login bem sucedido, dados existentes");

                    avisoCarregando.SetActive(false);
                    menu_logreg.SetActive(false);
                    gerDados.instancia.botao_logout.SetActive(true);
                }

                if(resposta[1] == "2"){
                    //dados inexistentes
                    
                    gerDados.instancia.dados_.id = long.Parse(resposta[2]);
                    gerDados.instancia.dados_.nick = resposta[3];

                    PlayerPrefs.SetInt("logado", 1);

                    gerDados.instancia.salvar();

                    Debug.Log("login bem sucedido, dados inexistentes");

                    avisoCarregando.SetActive(false);
                    menu_logreg.SetActive(false);
                    gerDados.instancia.botao_logout.SetActive(true);
                }
            }

            

            if(resposta[0]=="2"){
                // senha incorreta

                Debug.Log("senha incorreta");
                
                log_senha.text = "";
            }

            if(resposta[0]=="3"){
                // conta nao confirmada
                menu_conf.SetActive(true);

                menu_conf.GetComponent<confirmar>().id = resposta[1];
            }
        }
    }
}
