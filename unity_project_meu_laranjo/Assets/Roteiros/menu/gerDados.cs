using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class gerDados : MonoBehaviour
{
    UnityWebRequest link;
    public string site, cont_;
    public string[] resposta;

    public static gerDados instancia {get; set;}
    public dados dados_;

    public TextMeshProUGUI textoNick;
    public TextMeshProUGUI[] textosMoedas;
    public TextMeshProUGUI[] textosDolar;
    public bool online = false, inFirst = false, inicializacao = true;
    public GameObject botao_logout, menu_carregando, menu_ERRO, menu_esta_atualizado, menu_baixar_atualizacao, menu_sem_internet;

    private void Awake() {
        inicializacao = true;

        DontDestroyOnLoad(gameObject);
        instancia = this;
        
        gerenciador.instancia = GameObject.Find("ger_jogo").GetComponent<gerenciador>();

        loginConfigs();
        //carregar();
        //Debug.Log(ferramentas.Serializar<dados>(dados_));


        // colocar roupas do outfit
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

            aplicarDados();
        }
    }

    public void aplicarDados(){
        
        textoNick.text = dados_.nick;

        foreach(TextMeshProUGUI txt_ in textosMoedas){
            txt_.text = dados_.moedas.ToString();
        }

        foreach(TextMeshProUGUI txt_ in textosDolar){
            txt_.text = dados_.dolares.ToString();
        }


        aplicarOutfit(gerenciador.instancia.laranjo,dados_.outfit,dados_.nivel);

        gerLinguas.instancia.atualizarLingua();
    }

    public void loginConfigs(){
        if(PlayerPrefs.HasKey("logado")){
            if(PlayerPrefs.GetInt("logado") == 1){
                
                

                botao_logout.SetActive(true);

                //quando estiver com login:

                carregar();

                Debug.Log("voce esta logado com o id: " + dados_.id.ToString());

            }else{
                Debug.Log("Voce nao esta logado :c");

                botao_logout.SetActive(false);

                //quando estiver sem login:

                carregar();

                StartCoroutine(verificarVersao());

            }
        }else
        {
            PlayerPrefs.SetInt("logado", 0);
            Debug.Log("Primeiro uso, registrando dados...");

            dados_ = new dados();

            salvarDadosOffline();

            aplicarDados();

            StartCoroutine(verificarVersao());

            //quando estiver entrando plea primeira vez:
            
        }
    }
    public void salvar(){

        ///////////////////////////////////////

        if(PlayerPrefs.GetInt("logado") == 1){
                StartCoroutine(salvarDadosOnline(1, false));
                Debug.Log("LOGADO, TENTANDO SALVAR DADOS ONLINE...");
        }else{
            salvarDadosOffline();
            Debug.Log("DESLOGADO, DADOS SALVOS OFFLINE");

            aplicarDados();
        }

        //////////////////////////////////////

        
    }

    public void salvar(bool importante_){

        ///////////////////////////////////////

        if(PlayerPrefs.GetInt("logado") == 1){
                StartCoroutine(salvarDadosOnline(1, importante_));
                Debug.Log("LOGADO, TENTANDO SALVAR DADOS ONLINE...");
        }else{
            PlayerPrefs.SetString("dados",ferramentas.Serializar<dados>(dados_));
            Debug.Log("DESLOGADO, DADOS SALVOS OFFLINE");

            aplicarDados();
        }

        //////////////////////////////////////

        
    }

    public void salvarDadosOffline(){
        PlayerPrefs.SetString("dados",ferramentas.Serializar<dados>(dados_));
    }

    public void carregar(){
        if(PlayerPrefs.HasKey("dados")){

            carregarDadosOffline();

            salvar();

        }else
        {
            dados_ = new dados();
            dados_.ult_ctt = timeStampDeDate(DateTime.UtcNow);
            salvar();
        }
    }

    public void carregarDadosOffline(){
        dados_ = ferramentas.Desserializar<dados>(PlayerPrefs.GetString("dados"));
    }

    public void baixarDados(){
        StartCoroutine(salvarDadosOnline(1, false));
    }

    public void aplicarOutfit(GameObject laranjo_, int[] outfit_, float nivel_){

        for(int i = 1; i < (outfit_.Length * 32 + 1);i++){
            if(temItemOutfFit(i - 1, outfit_)){

                //Debug.Log("i = " + i +",item de tipo: \"" + gerenciador.instancia.itemDeId(i).posicao + "\", com idex: "+ ((int)gerenciador.instancia.itemDeId(i).posicao-1));

                laranjo_.GetComponent<design>().MudarMesh(gerenciador.instancia.itemDeId(i),nivel_);

                if((int)gerenciador.instancia.itemDeId(i).posicao == 5){
                    laranjo_.GetComponent<Animator>().SetBool("item",gerenciador.instancia.itemDeId(i).seguraItem);
                }
            }
        }
    }

    public bool temItem(int id_){
        
        return (dados_.itens[(id_-1)/32] &  (1 << ((id_-1) % 32))) != 0;
    }

    public void adicionarItem(int id_){
        dados_.itens[(id_-1)/32] |= 1 << ((id_-1) % 32);
    }

    public void removerItem(int id_){
        dados_.itens[(id_-1)/32] ^= 1 << ((id_-1) % 32);
    }

    public void deslogar(){
        dados_ = new dados();
        //PlayerPrefs.SetString("id",dados_.id.ToString());
        PlayerPrefs.SetInt("logado", 0);

        salvarDadosOffline();
        aplicarDados();
    }


    public bool temItemOutfFit(int id_, int[] outfit_){

        return (outfit_[(id_)/32] &  (1 << (((id_) % 32)))) != 0;
    }
    public void adicionarOutFit(int id_, int[] outfit_){
        foreach(item item_ in gerenciador.instancia.itens){
            if(item_ != null){
                if(item_.posicao == gerenciador.instancia.itemDeId(id_).posicao){
                    removerOutFit(item_.id,outfit_);
                }
            }
        }

        outfit_[(id_-1)/32] |= 1 << ((id_-1) % 32);
    }

    public void removerOutFit(int id_, int[] outfit_){
        outfit_[(id_-1)/32] &= ~(1 << ((id_-1) % 32));
    }

    IEnumerator verificarVersao(){
        UnityWebRequest tst_ = UnityWebRequest.Post(gerenciador.instancia.host_ + "/pps/lar_proc_at.php",new WWWForm());

        yield return tst_.SendWebRequest();

        if(tst_.isNetworkError || tst_.isHttpError){

            //erro na conexao

            Debug.Log("nao foi possivel verificar a versao");


            if(inicializacao){
                menu_sem_internet.SetActive(true);

                inicializacao = false;
            }
            

        }else
        {
            Debug.Log("versao instalada: " + gerenciador.instancia.versao.ToString() + ". versao mais atual disponivel: " + tst_.downloadHandler.text.Split(',')[0]);

            if(int.Parse(tst_.downloadHandler.text.Split(',')[0]) > gerenciador.instancia.versao){

                //jogo desatualizado

                Debug.Log("jogo desatualizado");

                menu_baixar_atualizacao.SetActive(true);

            }else
            {
                //jogo atualizado

                Debug.Log("jogo atualizado");

                if(inicializacao){
                    menu_esta_atualizado.SetActive(true);

                    inicializacao = false;
                }


            }
        }
    }

    IEnumerator salvarDadosOnline(int acao_, bool importante_){

        UnityWebRequest tst_ = UnityWebRequest.Post(gerenciador.instancia.host_ + "/pps/lar_proc_at.php",new WWWForm());

        if(importante_){
            menu_carregando.SetActive(true);
        }

        yield return tst_.SendWebRequest();

        if(tst_.isNetworkError || tst_.isHttpError){

            Debug.Log("erro de rede :l (" + link.error + ")   salvando dados offline");

            dados_.ult_ctt = timeStampDeDate(DateTime.UtcNow);

            salvarDadosOffline();

            aplicarDados();

            if(importante_){
                menu_carregando.SetActive(false);
            }

            if(inicializacao){
                menu_sem_internet.SetActive(true);

                inicializacao = false;
            }


            menu_ERRO.SetActive(true);

            menu_ERRO.transform.Find("desc").gameObject.GetComponent<TextMeshProUGUI>().text = "erro na rede (" + link.error + ")";

        }else
        {
            Debug.Log("versao instalada: " + gerenciador.instancia.versao.ToString() + ". versao mais atual disponivel: " + tst_.downloadHandler.text.Split(',')[0]);

            if(int.Parse(tst_.downloadHandler.text.Split(',')[0]) > gerenciador.instancia.versao){

                // jogo desatualizado

                Debug.Log("jogo desatualizado");

                dados_.ult_ctt = timeStampDeDate(DateTime.UtcNow);

                salvarDadosOffline();

                aplicarDados();

                if(importante_){
                    menu_carregando.SetActive(false);
                }
                
                menu_baixar_atualizacao.SetActive(true);


            }else{

                // jogo atualizado

                Debug.Log("jogo atualizado");

                if(inicializacao){

                    menu_esta_atualizado.SetActive(true);

                    inicializacao = false;

                }

                Debug.Log("salvando dados online de..." + dados_.id.ToString() + ". UTC: " + dados_.ult_ctt);

                WWWForm form = new WWWForm();

                form.AddField("acao",acao_);
                form.AddField("id", dados_.id.ToString());
                form.AddField("nick",dados_.nick);
                form.AddField("moedas",dados_.moedas.ToString());
                form.AddField("dolares",dados_.dolares.ToString());
                form.AddField("nivel",dados_.nivel.ToString().Replace(',','.'));
                form.AddField("id_casa",dados_.id_casa);
                form.AddField("quant_gar",dados_.quant_gar);
                form.AddField("ult_ctt",dados_.ult_ctt);

                

                dados base_ = new dados();

                for(int i_ = 0; i_ < dados_.itens.Length; i_++){
                    base_.itens[i_] = dados_.itens[i_];
                }

                dados_.itens = base_.itens;



                for(int i_ = 0; i_ < dados_.outfit.Length; i_++){
                    base_.outfit[i_] = dados_.outfit[i_];
                }

                dados_.outfit = base_.outfit;



                for(int i_ = 0; i_ < dados_.recordes.Length; i_++){
                    base_.recordes[i_] = dados_.recordes[i_];
                }

                dados_.recordes = base_.recordes;

                

                form.AddField("itens",string.Join("-",dados_.itens));
                form.AddField("outfit",string.Join("-",dados_.outfit));
                form.AddField("moveis",string.Join("-",dados_.moveis));
                form.AddField("records",string.Join("-",dados_.recordes));

                form.AddField("carros",stringDeCarro(dados_.carro[1]) + "-" + stringDeCarro(dados_.carro[2]) + "-" + stringDeCarro(dados_.carro[3]));


                link = UnityWebRequest.Post(gerenciador.host + site,form);

                if(importante_){
                    menu_carregando.SetActive(true);
                }

                yield return link.SendWebRequest();

                if(link.isNetworkError || link.isHttpError){

                    Debug.Log("erro de rede :l (" + link.error + ")   salvando dados offline");

                    dados_.ult_ctt = timeStampDeDate(DateTime.UtcNow);

                    salvarDadosOffline();

                    aplicarDados();

                    if(importante_){
                        menu_carregando.SetActive(false);
                    }


                    menu_ERRO.SetActive(true);

                    menu_ERRO.transform.Find("desc").gameObject.GetComponent<TextMeshProUGUI>().text = "erro na rede (" + link.error + ")";

                }else
                {
                    Debug.Log("rede ok :)");
                    
                    resposta = link.downloadHandler.text.Split(',');
                    
                    if(resposta[0] == "1"){

                        Debug.Log("DADOS ATUALIZADOS ONLINE, UTC: " + resposta[1]);

                        PlayerPrefs.SetInt("logado", 1);
                        
                        dados_.ult_ctt = resposta[1];

                        salvarDadosOffline();

                        aplicarDados();
                    }

                    if(resposta[0] == "2"){

                        Debug.Log("DADOS ONLINE CRIADOS, UTC: " + resposta[1]);

                        PlayerPrefs.SetInt("logado", 1);

                        dados_.ult_ctt = resposta[1];

                        salvarDadosOffline();

                        aplicarDados();
                    }

                    if(resposta[0] == "3"){
                        Debug.Log("DADOS ONLINE BAIXADOS, UTC: " + resposta[8]);
                        

                        int lingua_ = dados_.lingua;

                        dados_ = new dados(long.Parse(resposta[1]),resposta[2],float.Parse(resposta[3].Replace('.',',')),lingua_,int.Parse(resposta[4]),int.Parse(resposta[5]),int.Parse(resposta[6]),int.Parse(resposta[7]),resposta[8]);

                        dados_.itens = Array.ConvertAll(resposta[9].Split('-'),int.Parse);

                        dados_.outfit = Array.ConvertAll(resposta[10].Split('-'),int.Parse);

                        dados_.moveis = Array.ConvertAll(resposta[11].Split('-'),int.Parse);

                        dados_.recordes = Array.ConvertAll(resposta[12].Split('-'),long.Parse);
                        
                        dados_.carro = Array.ConvertAll(new string[4] {null,string.Join("-",resposta[13].Split('-').Skip(0).Take(13).ToArray()),string.Join("-",resposta[13].Split('-').Skip(13).Take(13).ToArray()),string.Join("-",resposta[13].Split('-').Skip(26).Take(13).ToArray())},new Converter<string,carro_dados>(carroDeString));

                        salvarDadosOffline();

                        aplicarDados();

                        
                    }

                    if(resposta[0] == "4"){
                        Debug.Log("DADOS SINCRONIZADOS, UTC: " + resposta[1]);

                        PlayerPrefs.SetInt("logado", 1);
                        
                        dados_.ult_ctt = resposta[1];

                        salvarDadosOffline();

                        aplicarDados();

                    }
                    if(importante_){
                        menu_carregando.SetActive(false);
                    }

                }
            }
        }


    }

    public string timeStampDeDate(DateTime data_){
        string dataString_ = "";

        dataString_ = data_.Year.ToString() + "-"+ data_.Month.ToString() + "-"+ data_.Day.ToString() + " " + data_.Hour.ToString().PadLeft(2,'0') + ":" + data_.Minute.ToString() + ":" + data_.Second.ToString();

        return dataString_;
    }

    public string stringDeCarro(carro_dados car_){
        string textoCarro_ = "";

        if(car_ == null){
            textoCarro_ = "0" + "-" + "0-0-0-0" +  "-" + "0-0" + "-" + "0-0-0-0-0-0";
        }else
        {
            textoCarro_ = car_.id_chassi.ToString() + "-" + string.Join("-",car_.nivel) + "-" + string.Join("-",car_.cor_id) + "-" + string.Join("-",car_.acessorios);
        }

        return textoCarro_;
    }


    public carro_dados carroDeString(string dads_)
    {
        carro_dados carroTexto_ = null;

        if(dads_ != null){
            if(dads_.Split('-')[0] != "0"){
                carroTexto_ = new carro_dados();

                carroTexto_.id_chassi = int.Parse(dads_.Split('-')[0]);

                carroTexto_.nivel = new int[4]{int.Parse(dads_.Split('-')[1]),int.Parse(dads_.Split('-')[2]),int.Parse(dads_.Split('-')[3]),int.Parse(dads_.Split('-')[4])};

                carroTexto_.cor_id = new int[2]{int.Parse(dads_.Split('-')[5]),int.Parse(dads_.Split('-')[6])};

                carroTexto_.acessorios = new int[6]{int.Parse(dads_.Split('-')[7]),int.Parse(dads_.Split('-')[8]),int.Parse(dads_.Split('-')[9]),int.Parse(dads_.Split('-')[10]),int.Parse(dads_.Split('-')[11]),int.Parse(dads_.Split('-')[12])};
            }
        }

        

        return carroTexto_;
    }


}
