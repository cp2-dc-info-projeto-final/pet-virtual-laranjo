using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class gerDados : MonoBehaviour
{
    UnityWebRequest link;
    public string site, cont_;
    public string[] resposta;

    public static gerDados instancia {get; set;}
    public dados dados_;

    public int[] itemAtual = new int[10]; 

    public TextMeshProUGUI textoMoeda, textoDolar;
    bool online = false;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        instancia = this;

        loginConfigs();
        
        gerenciador.instancia = GameObject.Find("ger_jogo").GetComponent<gerenciador>();
        carregar();
        //Debug.Log(ferramentas.Serializar<dados>(dados_));

        for(int i = 0; i < (dados_.outfit.Length * 64);i++){
            if(temItemOutfFit(i)){
                for(int i_ = 0; i_ < gerenciador.instancia.itens.Count - 1; i++){
                    if(gerenciador.instancia.itens[i_] != null){
                        if(gerenciador.instancia.itens[i_].id == i){
                            gerenciador.instancia.laranjo.GetComponent<design>().MudarMesh(gerenciador.instancia.itens[i]);

                            if((int)gerenciador.instancia.itens[i_].posicao == 5){
                                gerenciador.instancia.laranjo.GetComponent<Animator>().SetBool("item",gerenciador.instancia.itens[i].seguraItem);
                            }
                            itemAtual[(int)gerenciador.instancia.itens[i_].posicao] = i;
                        }
                    }
                }
            }
        }
    }

    private IEnumerator Start() {

        link = UnityWebRequest.Get(site);


        yield return link.SendWebRequest();
        if(link.isNetworkError || link.isHttpError){
            cont_ = "ERROOOU " + link.error;
        }else
        {
            cont_ = link.downloadHandler.text;
        }


    }


    private void Update() {
        if(Input.GetKeyDown(KeyCode.P)){
            dados_.moedas = 14;
            dados_.itens[0] = 1023;
            salvar();
            Debug.Log(ferramentas.Serializar<dados>(dados_));

            Debug.Log(10/3);
        }

        textoMoeda.text = dados_.moedas.ToString();
        textoDolar.text = dados_.dolares.ToString();
    }

    public void loginConfigs(){
        if(PlayerPrefs.HasKey("logado")){
            if(PlayerPrefs.GetInt("logado") == 1){
                print("Voce esta logado como '");
                print(PlayerPrefs.GetString("login" + "' "));
                for(int i = 1; i <= PlayerPrefs.GetString("senha").Length;i++){
                    print("*");
                }
                print(" c:");

                //quando estiver com login:

            }else{
                print("Voce nao esta logado :c");

                //quando estiver sem login:

            }
        }else
        {
            PlayerPrefs.SetInt("logado", 0);
            print("Primeiro uso, registrando dados...");

            //quando estiver entrando plea primeira vez:
            
        }
    }
    public void salvar(){
        PlayerPrefs.SetString("dados",ferramentas.Serializar<dados>(dados_));
    }

    public void carregar(){
        if(PlayerPrefs.HasKey("dados")){
            dados_ = ferramentas.Desserializar<dados>(PlayerPrefs.GetString("dados"));
            if(EstaOnline()){
                if(dados_.id!= 0){
                    
                }else
                {
                    dados_.ult_ctt = timeStampDeDate(DateTime.UtcNow);
                    salvar();
                }
            }else
            {
                dados_.ult_ctt = timeStampDeDate(DateTime.UtcNow);
                salvar();
            }
        }else
        {
            dados_ = new dados();
            dados_.ult_ctt = timeStampDeDate(DateTime.UtcNow);
            salvar();
        }
    }

    public bool temItem(int id_){
        return (dados_.itens[id_/64] &  (1 << (id_ % 64))) != 0;
    }

    public void adicionarItem(int id_){
        dados_.itens[id_/64] |= 1 << (id_ % 64);
    }

    public void removerItem(int id_){
        dados_.itens[id_/64] ^= 1 << (id_ % 64);
    }


    public bool temItemOutfFit(int id_){
        return (dados_.outfit[id_/64] &  (1 << (id_ % 64))) != 0;
    }
    public void adicionarOutFit(int id_){
        dados_.outfit[id_/64] |= 1 << (id_ % 64);
    }

    public void removerOutFit(int id_){
        dados_.outfit[id_/64] ^= 1 << (id_ % 64);
    }

    public void trocarOutFit(int id_){
        removerOutFit(itemAtual[(int)gerenciador.instancia.itemDeId(id_).posicao]);
        itemAtual[(int)gerenciador.instancia.itemDeId(id_).posicao] = gerenciador.instancia.itemDeId(id_).id;
        adicionarOutFit(id_);
    }

    public bool EstaOnline(){

        StartCoroutine(checarConexao());

        return online;
    }

    IEnumerator checarConexao(){
        link = UnityWebRequest.Post(site,new WWWForm());

        yield return link.SendWebRequest();

        if(link.isNetworkError || link.isHttpError){

            online = false;
            //st1 = "ERROOOU " + link.error;
            //if(link.error == UnityWebRequest.)
        }else
        {
            //return true;
            online = true;
        }
    }

    IEnumerator salvarDadosOnline(int acao_){

        WWWForm form = new WWWForm();

        form.AddField("acao",acao_);
        form.AddField("id", dados_.id.ToString());
        form.AddField("nick",dados_.nick);
        form.AddField("moedas",dados_.moedas);
        form.AddField("dolares",dados_.dolares);
        form.AddField("nivel",dados_.nivel.ToString());
        form.AddField("id_casa",dados_.id_casa);
        form.AddField("quant_gar",dados_.quant_gar);
        form.AddField("ult_ctt",dados_.ult_ctt);

        link = UnityWebRequest.Post(site,form);

        yield return link.SendWebRequest();

        if(link.isNetworkError || link.isHttpError){

            Debug.Log("deu ruim :l");

        }else
        {
            
            resposta = link.downloadHandler.text.Split(',');
            
            if(resposta[0] == "1"){
                dados_.ult_ctt = resposta[1];
            }

        }
    }

    public string timeStampDeDate(DateTime data_){
        string dataString_ = "";

        dataString_ = data_.Year.ToString() + "-"+ data_.Month.ToString() + "-"+ data_.Day.ToString() + " " + data_.Hour.ToString() + ":" + data_.Minute.ToString() + ":" + data_.Second.ToString();

        return dataString_;
    }


}
