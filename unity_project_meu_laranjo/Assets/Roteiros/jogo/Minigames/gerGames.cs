using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gerGames : MonoBehaviour
{
    public int minigame_atual = -1, indice_carro = 0, chances_extras = 1;
    public int  moedas_atuais = 0, dolares_atuais = 0;
    public float pontuacao_atual = 0, tempo_restante = 0;
    public bool iniciado = false, reiniciado = false, gameOver = false;
    public static gerGames instancia;
    public GameObject uis_casa, menu_minigame, menu_pontuacao, menu_restart;
    public Slider slider_gasolina;
    public TextMeshProUGUI nome, descricao, texto_pontos, texto_moedas;
    public GameObject[] fundo, cenarios_minigames;
    public Color[] paleta;
    public GameObject rank_peca, rank_piv, cenario_0, menu_game_over;
    public List<mini_game> games;
    // Start is called before the first frame update

    void Awake(){
        instancia = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ---configuracoes do passeio
        if(minigame_atual == 0){
            if(iniciado){
                tempo_restante -= Time.deltaTime;

                tempo_restante = Mathf.Clamp(tempo_restante,0,60);

                slider_gasolina.value = tempo_restante;

                if(tempo_restante > 0){
                    gerenciador.instancia.carros[indice_carro].GetComponent<veiculo>().ligado = true;
                    
                    gerenciador.instancia.carros[indice_carro].GetComponent<Rigidbody>().isKinematic = false;

                    pontuacao_atual += Time.deltaTime;
                
                }else
                {
                    gerenciador.instancia.carros[indice_carro].GetComponent<veiculo>().ligado = false;



                    if(!gameOver){
                        StartCoroutine(checharGameOver());

                        gameOver = true;
                    }
                    
                }

                texto_moedas.text = moedas_atuais.ToString();
                texto_pontos.text = ((Mathf.FloorToInt(pontuacao_atual)) / 60).ToString() + ":" + ((Mathf.FloorToInt(pontuacao_atual)) % 60).ToString().PadLeft(2,'0')+"mins";
            }
        }

        // ---configuracoes do flepi laranjo

        if(minigame_atual == 1){

            if(iniciado){
                
                if(cenarios_minigames[0].GetComponentInChildren<flepi_laranjo>().vivo == true){
                    menu_restart.SetActive(false);
                }else
                {
                    menu_restart.SetActive(true);
                }
            }

            texto_moedas.text = moedas_atuais.ToString();
            texto_pontos.text = Mathf.FloorToInt(pontuacao_atual).ToString();

            if(reiniciado){
                cenarios_minigames[0].GetComponentInChildren<flepi_laranjo>().restartFlepiLaranjo();
                reiniciado = false;
            }
        }


        // ---configuracoes do pulo infinito

        if(minigame_atual == 2){

            if(iniciado){
                
                if(cenarios_minigames[1].GetComponentInChildren<pulo_infinito>().vivo == true){
                    menu_restart.SetActive(false);
                }else
                {
                    menu_restart.SetActive(true);
                }
            }

            texto_moedas.text = moedas_atuais.ToString();
            texto_pontos.text = Mathf.FloorToInt(pontuacao_atual).ToString();

            if(reiniciado){
                cenarios_minigames[1].GetComponentInChildren<pulo_infinito>().restartPuloInfinito();
                reiniciado = false;
            }
        }

        // ---configuracoes do copo aleatorio

        if(minigame_atual == 3){
            if(iniciado){
                
                if(cenarios_minigames[2].GetComponent<copo_aleatorio>().vivo == true){
                    menu_restart.SetActive(false);
                }else
                {
                    menu_restart.SetActive(true);
                }

                texto_moedas.text = moedas_atuais.ToString();
                texto_pontos.text = Mathf.FloorToInt(pontuacao_atual).ToString();

                if(reiniciado){
                    StartCoroutine(cenarios_minigames[2].GetComponent<copo_aleatorio>().restartCopoAleatorio());
                    reiniciado = false;
                }
            }
        }
    }

    public void AbrirGame(int id_game){

        uis_casa.SetActive(false);

        iniciado = false;

        minigame_atual = id_game;

        if(id_game == 0){
            iniciarPasseio();
        }

        if(id_game == 1){
            iniciarFlepiLaranjo();
        }

        if(id_game == 2){
            iniciarPuloInfinito();
        }

        if(id_game == 3){
            iniciarCopoAleatorio();
        }

        menu_minigame.GetComponent<animar_UI>().mostrar_ocultar();

        nome.text = gameDeId(id_game).nome[gerDados.instancia.dados_.lingua];

        descricao.text = gameDeId(id_game).desc[gerDados.instancia.dados_.lingua];



        fundo[0].GetComponent<Image>().color = paleta[id_game * 2];
        fundo[1].GetComponent<Image>().color = paleta[id_game * 2];
        fundo[2].GetComponent<Image>().color = paleta[id_game * 2 + 1];



        // --- limpar rank
        for ( int i= rank_piv.transform.childCount-1; i>=0; --i )
        {
            GameObject child = rank_piv.transform.GetChild(i).gameObject;
            Destroy(child );
        }



        GameObject rank_ = Instantiate(rank_peca,rank_piv.transform);

        rank_.GetComponent<peca_rank>().SetarDados(true,gerDados.instancia.dados_.nick,1,gerDados.instancia.dados_.recordes[id_game],gameDeId(id_game).tipoDePont);
    }

    public void iniciarPasseio(){

        gerenciador.instancia.instanciarTerrenos();
        
        pontuacao_atual = 0;
        moedas_atuais = 0;
        dolares_atuais = 0;
        pontuacao_atual = 0;
        tempo_restante = Random.Range(40f,60f);

        slider_gasolina.gameObject.SetActive(true);
    }

    public void iniciarFlepiLaranjo(){
        pontuacao_atual = 0;
        moedas_atuais = 0;
        dolares_atuais = 0;

        cenario_0.SetActive(false);
        cenarios_minigames[0].SetActive(true);

        gerDados.instancia.aplicarOutfit(cenarios_minigames[0].GetComponentInChildren<design>().gameObject,gerDados.instancia.dados_.outfit);

        cenarios_minigames[0].GetComponentInChildren<flepi_laranjo>().restartFlepiLaranjo();
        cenarios_minigames[0].GetComponentInChildren<flepi_laranjo>().vivo = false;
    }

    public void iniciarPuloInfinito(){
        pontuacao_atual = 0;
        moedas_atuais = 0;
        dolares_atuais = 0;

        cenario_0.SetActive(false);
        cenarios_minigames[1].SetActive(true);

        gerDados.instancia.aplicarOutfit(cenarios_minigames[1].GetComponentInChildren<design>().gameObject,gerDados.instancia.dados_.outfit);

        cenarios_minigames[1].GetComponentInChildren<pulo_infinito>().restartPuloInfinito();
        cenarios_minigames[1].GetComponentInChildren<pulo_infinito>().vivo = false;
    }

    public void iniciarCopoAleatorio(){
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        pontuacao_atual = 0;
        moedas_atuais = 0;
        dolares_atuais = 0;

        cenario_0.SetActive(false);
        cenarios_minigames[2].SetActive(true);

        gerDados.instancia.aplicarOutfit(cenarios_minigames[2].GetComponentInChildren<design>().gameObject,gerDados.instancia.dados_.outfit);

        StartCoroutine(cenarios_minigames[2].GetComponent<copo_aleatorio>().restartCopoAleatorio());
        cenarios_minigames[2].GetComponent<copo_aleatorio>().vivo = false;
    }

    public mini_game gameDeId(int id_){
        mini_game game_ = null;

        foreach(mini_game gm_ in games){
            if(gm_.id == id_){
                game_ = gm_;
            }
        }

        return game_;
    }

    public void coletar(TipoDeColetavel tipo_){
        if(tipo_ == TipoDeColetavel.moeda){
            moedas_atuais ++;
        }

        if(tipo_ == TipoDeColetavel.dolar){
            dolares_atuais ++;
        }

        if(tipo_ == TipoDeColetavel.gasolina){
            tempo_restante += Random.Range(20f,40f);

            if(gameOver){
                menu_game_over.GetComponent<animar_UI>().mostrar_ocultar(false);

                gameOver = false;
            }
        }
    }

    public void pontuar(){
        pontuacao_atual += 1;
    }

    public void pontuar(float pont_){
        pontuacao_atual += pont_;
    }

    public void iniciarMiniGame(){

        chances_extras = 1;

        iniciado = true;

        menu_pontuacao.SetActive(true);

        if(minigame_atual == 1){
            cenarios_minigames[0].GetComponentInChildren<flepi_laranjo>().vivo = true;
        }

        if(minigame_atual == 2){
            cenarios_minigames[1].GetComponentInChildren<pulo_infinito>().vivo = true;
        }

        if(minigame_atual == 3){
            StartCoroutine(cenarios_minigames[2].GetComponent<copo_aleatorio>().restartCopoAleatorio());
        }
    }

    public void reiniciarMiniGame(){

        if(gerDados.instancia.dados_.recordes[minigame_atual] < Mathf.FloorToInt(pontuacao_atual)){
            gerDados.instancia.dados_.recordes[minigame_atual] = Mathf.FloorToInt(pontuacao_atual);

            Debug.Log("NOVO RECORD");
        }

        gerDados.instancia.dados_.moedas += moedas_atuais;
        gerDados.instancia.dados_.dolares += dolares_atuais;

        gerDados.instancia.salvar();

        moedas_atuais = 0;
        dolares_atuais = 0;
        pontuacao_atual = 0;

        menu_restart.SetActive(false);

        reiniciado = true;
    }

    public void finalizarMiniGame(){

        uis_casa.SetActive(true);

        menu_restart.SetActive(false);

        cenario_0.SetActive(true);

        if(minigame_atual == 0){
            foreach(GameObject player_ in GameObject.FindGameObjectsWithTag("Player")){
                if(player_.GetComponent<movimento>().dentro_carro){
                    player_.GetComponent<movimento>().botao_sair_carro();
                }
            }
        }

        if(minigame_atual == 3){
            Screen.orientation = ScreenOrientation.Portrait;
        }

        foreach(GameObject cen_ in cenarios_minigames){
            cen_.SetActive(false);
        }

        if(gerDados.instancia.dados_.recordes[minigame_atual] < long.Parse(Mathf.FloorToInt(pontuacao_atual).ToString())){
            gerDados.instancia.dados_.recordes[minigame_atual] = long.Parse(Mathf.FloorToInt(pontuacao_atual).ToString());

            Debug.Log("NOVO RECORD");
        }
        
        iniciado = false;

        menu_pontuacao.SetActive(false);

        slider_gasolina.gameObject.SetActive(false);

        minigame_atual = -1;

        gerDados.instancia.dados_.moedas += moedas_atuais;
        gerDados.instancia.dados_.dolares += dolares_atuais;

        gerDados.instancia.salvar();

    }

    public void recompensaVideo(){
        chances_extras -= 1;
        gameOver = false;

        tempo_restante = 40;
        iniciado = false;
    }

    public void videoFechado(){
        iniciado = true;
        menu_game_over.GetComponent<animar_UI>().mostrar_ocultar(false);
    }

    public IEnumerator checharGameOver(){
        yield return new WaitForSeconds(3);

        if(tempo_restante == 0){
            if(chances_extras > 0){
                menu_game_over.GetComponent<animar_UI>().mostrar_ocultar();
            }
        }
    }
    
}

public enum TipoDeColetavel {
    moeda = 1,
    dolar,
    gasolina
}