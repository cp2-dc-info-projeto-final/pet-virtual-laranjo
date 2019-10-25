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
    public bool online = false, inFirst = false;

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

    private void Start() {
        /*
        link = UnityWebRequest.Get(site);


        yield return link.SendWebRequest();
        if(link.isNetworkError || link.isHttpError){
            cont_ = "ERROOOU " + link.error;
        }else
        {
            cont_ = link.downloadHandler.text;
        }*/


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
                
                Debug.Log("voce esta logado com o id: " + PlayerPrefs.GetString("id"));

                //quando estiver com login:

            }else{
                Debug.Log("Voce nao esta logado :c");

                //quando estiver sem login:

            }
        }else
        {
            PlayerPrefs.SetInt("logado", 0);
            Debug.Log("Primeiro uso, registrando dados...");

            //quando estiver entrando plea primeira vez:
            
        }
    }
    public void salvar(){

        if(EstaOnline()){

            StartCoroutine(salvarDadosOnline(1));
            Debug.Log("DADOS SALVOS ON LINE");

        }else{

            Debug.Log("NAO FOI POSSIVEL SALVAR DADOS ONLINE. SEM CONEXAO COM A INTENET");

        }

        PlayerPrefs.SetString("dados",ferramentas.Serializar<dados>(dados_));
    }

    public void carregar(){
        if(PlayerPrefs.HasKey("dados")){
            dados_ = ferramentas.Desserializar<dados>(PlayerPrefs.GetString("dados"));
            if(online){
                if(dados_.id!= 0){
                    Debug.Log("SALVANDO");
                    salvar();
                    
                }else
                {
                    Debug.Log("ID 0000");
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

    public void baixarDados(){
        StartCoroutine(salvarDadosOnline(1));
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

    public void deslogar(){
        dados_ = new dados();
        PlayerPrefs.SetString("id",dados_.id.ToString());
        PlayerPrefs.SetInt("logado", 0);
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

        ///////////////////WaitForSeconds(5);
        //StartCoroutine(DoLast());

        return online;
    }

    IEnumerator DoLast() {
 
        while(inFirst)       
        yield return new WaitForSeconds(0.1f);
        print("Do stuff.");
    }

    IEnumerator checarConexao(){

        inFirst = true;

        link = UnityWebRequest.Post(site,new WWWForm());

        yield return link.SendWebRequest();

        if(link.isNetworkError || link.isHttpError){

            online = false;
            Debug.Log("OFFFF");
            //st1 = "ERROOOU " + link.error;
            //if(link.error == UnityWebRequest.)
        }else
        {
            //return true;
            online = true;
            Debug.Log("ONNNN");
        }

        inFirst = false;
    }

    IEnumerator salvarDadosOnline(int acao_){

        Debug.Log("salvando dados online de..." + dados_.id.ToString() + ". UTC: " + dados_.ult_ctt);

        WWWForm form = new WWWForm();

        form.AddField("acao",acao_);
        form.AddField("id", dados_.id.ToString());
        form.AddField("nick",dados_.nick);
        form.AddField("moedas",dados_.moedas.ToString());
        form.AddField("dolares",dados_.dolares.ToString());
        form.AddField("nivel",dados_.nivel.ToString());
        form.AddField("id_casa",dados_.id_casa);
        form.AddField("quant_gar",dados_.quant_gar);
        form.AddField("ult_ctt",dados_.ult_ctt);

        link = UnityWebRequest.Post(site,form);

        yield return link.SendWebRequest();

        if(link.isNetworkError || link.isHttpError){

            Debug.Log("erro de rede :l");

        }else
        {
            Debug.Log("rede ok :)");
            
            resposta = link.downloadHandler.text.Split(',');
            
            if(resposta[0] == "1"){
                Debug.Log("DADOS ATUALIZADOS ONLINE, UTC: " + resposta[1]);
                dados_.ult_ctt = resposta[1];
            }

            if(resposta[0] == "3"){
                Debug.Log("DADOS ONLINE BAIXADOS, UTC: " + resposta[8]);

                int lingua_ = dados_.lingua;

                dados_ = new dados(long.Parse(resposta[1]),resposta[2],float.Parse(resposta[3]),lingua_,long.Parse(resposta[4]),long.Parse(resposta[5]),int.Parse(resposta[6]),int.Parse(resposta[7]),resposta[8]);
            }

        }

        Debug.Log("dados solvos! (ou nao k k)");
    }

    public string timeStampDeDate(DateTime data_){
        string dataString_ = "";

        dataString_ = data_.Year.ToString() + "-"+ data_.Month.ToString() + "-"+ data_.Day.ToString() + " " + data_.Hour.ToString() + ":" + data_.Minute.ToString() + ":" + data_.Second.ToString();

        return dataString_;
    }


}
