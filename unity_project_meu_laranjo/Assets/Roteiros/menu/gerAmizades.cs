using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class gerAmizades : MonoBehaviour
{
    
    public static gerAmizades instancia;
    UnityWebRequest link_amz;
    public bool carregando = false;
    public string site_amz, id, id_02;
    public string[] resposta; // = new List<string>();
    public TMP_InputField pesquisa_nick;
    public TextMeshProUGUI perfil_nick, perfil_moedas, perfil_dolares;
    public Slider perfil_nivel;
    public GameObject[] botoes_acao;
    public GameObject perfil_amigo, prefab_peca_amigo, prefab_peca_sem_amigos, pivot_peca_amigo, avisoCarregando, menu_login;
    public Button botao_mostrar_amigos, botao_mostrar_solicitaoes;


    // Start is called before the first frame update
    void Awake() {
        instancia = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void botao_abrir_ger_amigos(){
        if(PlayerPrefs.GetInt("logado") == 1){
            botao_mostrar_amigos.interactable = false;
            botao_mostrar_solicitaoes.interactable = true;
            botaoMostrarAmigos(4);
            GetComponent<animar_UI>().mostrar_ocultar();
        }else
        {
            menu_login.GetComponent<animar_UI>().mostrar_ocultar();
        }
    }

    public void botao_pesquisar(){
        StartCoroutine(pesquisar_nick());
    }

    public IEnumerator pesquisar_nick(){

        if(pesquisa_nick.text.Length > 0){

            WWWForm form = new WWWForm();
            form.AddField("nick", pesquisa_nick.text);
            form.AddField("acao", 2);


            link_amz = UnityWebRequest.Post(gerenciador.host + site_amz,form);

            carregando = true;

            avisoCarregando.SetActive(true);

            yield return link_amz.SendWebRequest();

            carregando = false;

            avisoCarregando.SetActive(false);

            if(link_amz.isNetworkError || link_amz.isHttpError){

                Debug.Log("erro na rede (" + link_amz.error + ")");

            }else
            {
            resposta = link_amz.downloadHandler.text.Split(',');

            if(resposta[1] == "0"){
                Debug.Log("PESQUISA FEITA, NENHUM RESULTADO");
            }

            if(resposta[1] == "1"){

                Debug.Log("PESQUISA FEITA, " + (resposta.Length - 2).ToString() +" RESULTADO(S)");

                for (int i_ = pivot_peca_amigo.transform.childCount - 1; i_ > 0; i_--)
                {
                    Destroy(pivot_peca_amigo.transform.GetChild(i_).gameObject);
                }
                
                for(int i_ = 2; i_ < resposta.Length; i_ ++){

                    string[] dados_ = resposta[i_].Split('-');

                    if(long.Parse(dados_[0]) != gerDados.instancia.dados_.id){

                        GameObject inst_ = Instantiate(prefab_peca_amigo,pivot_peca_amigo.transform);
                    
                        inst_.GetComponent<peca_amigo>().id = long.Parse(dados_[0]);
                        inst_.GetComponent<peca_amigo>().nick = dados_[1];
                        inst_.GetComponent<peca_amigo>().nivel = float.Parse(dados_[2].Replace('.',','));
                        
                    }

                    
                }

                botao_mostrar_amigos.interactable = true;
                botao_mostrar_solicitaoes.interactable = true;
            }
        }
        }

    }



    public void botaoMostraPerfil(long id_){
        id_02 = id_.ToString();
        StartCoroutine(mostrar_perfil(id_));
    }

    public IEnumerator mostrar_perfil(long id_){

        WWWForm form = new WWWForm();
        form.AddField("id_proprio", gerDados.instancia.dados_.id.ToString());
        form.AddField("id_outro", id_.ToString());
        form.AddField("acao", 1);


        link_amz = UnityWebRequest.Post(gerenciador.host + site_amz,form);

        carregando = true;

        avisoCarregando.SetActive(true);

        yield return link_amz.SendWebRequest();

        carregando = false;

        avisoCarregando.SetActive(false);

        if(link_amz.isNetworkError || link_amz.isHttpError){

            Debug.Log("erro na rede (" + link_amz.error + ")");

        }else
        {
            resposta = link_amz.downloadHandler.text.Split(',');

            if(resposta[0] == "1"){

                Debug.Log("MOSTRANDO PERFIL");

                perfil_amigo.GetComponent<animar_UI>().mostrar_ocultar();

                gerenciador.instancia.ligarCameraPreview();

                perfil_nick.text = resposta[1];
                perfil_nivel.value = float.Parse(resposta[2].ToString().Replace('.',','));
                perfil_moedas.text = resposta[3];
                perfil_dolares.text = resposta[4];

                //mostrar outfit (resposta[6])

                gerDados.instancia.aplicarOutfit(gerenciador.instancia.laranjo_preview,Array.ConvertAll(resposta[6].Split('-'),int.Parse),float.Parse(resposta[2].ToString().Replace('.',',')));

                //mostrar botao relativo as acoes

                foreach(GameObject bot_ in botoes_acao){
                    bot_.SetActive(false);
                }

                if(resposta[7] == "a"){
                    botoes_acao[1].SetActive(true);
                }

                if(resposta[7] == "r"){
                    botoes_acao[3].SetActive(true);
                }

                if(resposta[7] == "e"){
                    botoes_acao[2].SetActive(true);
                }

                if(resposta[7] == "0"){
                    botoes_acao[0].SetActive(true);
                }


            }

            
        }

    }

    public void botaoAdicionar(){
        StartCoroutine(modificarRelacionamento(1));
    }

    public void botaoRemover(){
        StartCoroutine(modificarRelacionamento(0));
    }

    public void botaoResponder(){
        StartCoroutine(modificarRelacionamento(2));
    }

    public IEnumerator modificarRelacionamento(int acao_2_){

        WWWForm form = new WWWForm();
        form.AddField("id_proprio", gerDados.instancia.dados_.id.ToString());
        form.AddField("id_outro", id_02.ToString());
        form.AddField("acao", 3);
        form.AddField("acao_2",acao_2_);


        link_amz = UnityWebRequest.Post(gerenciador.host + site_amz,form);

        carregando = true;

        perfil_amigo.GetComponent<animar_UI>().mostrar_ocultar();

        avisoCarregando.SetActive(true);

        yield return link_amz.SendWebRequest();

        carregando = false;

        avisoCarregando.SetActive(false);

        if(link_amz.isNetworkError || link_amz.isHttpError){

            Debug.Log("erro na rede (" + link_amz.error + ")");

        }else
        {
            resposta = link_amz.downloadHandler.text.Split(',');

            Debug.Log(link_amz.downloadHandler.text);

            Debug.Log("MODIFICACAO (" + resposta[2] + ") FEITA");
        
            StartCoroutine(mostrar_perfil(long.Parse(id_02)));
        }
    }

    public void botaoMostrarAmigos(int acao_){
        StartCoroutine(mostrarAmigos(acao_));
    }
    public IEnumerator mostrarAmigos(int acao_){
        WWWForm form = new WWWForm();
        form.AddField("id_proprio", gerDados.instancia.dados_.id.ToString());
        form.AddField("acao", acao_);


        link_amz = UnityWebRequest.Post(gerenciador.host + site_amz,form);

        carregando = true;

        avisoCarregando.SetActive(true);

        yield return link_amz.SendWebRequest();

        carregando = false;

        avisoCarregando.SetActive(false);

        if(link_amz.isNetworkError || link_amz.isHttpError){

            Debug.Log("erro na rede (" + link_amz.error + ")");

        }else
        {
        resposta = link_amz.downloadHandler.text.Split(',');

        if(resposta[1] == "0"){
            Debug.Log("NENHUM AMIGO :c");

            for (int i_ = pivot_peca_amigo.transform.childCount - 1; i_ > 0; i_--)
            {
                Destroy(pivot_peca_amigo.transform.GetChild(i_).gameObject);
            }

            GameObject inst_ = Instantiate(prefab_peca_sem_amigos,pivot_peca_amigo.transform);
        }else{

            Debug.Log("PESQUISA FEITA, " + (resposta.Length - 2).ToString() +" RESULTADO(S)");

            for (int i_ = pivot_peca_amigo.transform.childCount - 1; i_ > 0; i_--)
            {
                Destroy(pivot_peca_amigo.transform.GetChild(i_).gameObject);
            }
            
            for(int i_ = 1; i_ < resposta.Length -1; i_ ++){
                GameObject inst_ = Instantiate(prefab_peca_amigo,pivot_peca_amigo.transform);

                string[] dados_ = resposta[i_].Split('-');

                inst_.GetComponent<peca_amigo>().id = long.Parse(dados_[0]);
                inst_.GetComponent<peca_amigo>().nick = dados_[1];
                inst_.GetComponent<peca_amigo>().nivel = float.Parse(dados_[2].Replace('.',','));
            }
        }
    }

    }
}
