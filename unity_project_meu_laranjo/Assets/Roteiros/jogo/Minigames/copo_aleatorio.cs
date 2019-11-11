using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copo_aleatorio : MonoBehaviour
{
    public bool vivo, iniciado = false,pode_escolher = false, embaralhando = false;
    public float tempo_total = 3, tempo_embaralhamento = 0;
    public int movimentos = 5;
    public List<copo> copos = new List<copo>();
    public GameObject prefab_copo, moeda;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(vivo){
            if(!iniciado){
                embaralhar(movimentos,tempo_total);
                iniciado = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            
            StartCoroutine(restartCopoAleatorio());
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            
            embaralhar(movimentos,tempo_total);
        }

        if(embaralhando){
            pode_escolher = false;
            tempo_embaralhamento += Time.deltaTime;

            if(tempo_embaralhamento >= tempo_total){
                embaralhando = false;
                pode_escolher = true;
            }
        }

        if(pode_escolher){
            Ray raio_camera = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Input.GetKeyDown(KeyCode.Mouse0)){
                if(Physics.Raycast(raio_camera, out RaycastHit hit_,Mathf.Infinity)){
                    if(hit_.transform.tag == "copo"){
                        hit_.transform.GetComponent<copo>().mostrar();

                        if(hit_.transform.GetComponent<copo>() != copos[0]){
                            copos[0].mostrar();

                            Debug.Log("ERROOOOOU");

                            vivo = false;
                        }else
                        {
                            Debug.Log("acerto, ahh mizeraaavih");

                            gerGames.instancia.pontuar();
                            movimentos++;

                            if(movimentos < 50){
                                int numero_aleatorio_ = Random.Range(1,4);

                                if(numero_aleatorio_ == 3){
                                    StartCoroutine(ganharMoeda());
                                }
                            }else
                            {
                                StartCoroutine(ganharMoeda());
                            }

                            if(movimentos >= 55 && copos.Count == 3){
                                GameObject inst_ = Instantiate(prefab_copo,new Vector3(0,0,10), Quaternion.Euler(0,0,0),transform);
                                copos.Add(inst_.GetComponent<copo>());
                            }

                            if(movimentos >= 205 && copos.Count == 4){
                                GameObject inst_ = Instantiate(prefab_copo,new Vector3(0,0,10), Quaternion.Euler(0,0,0),transform);
                                copos.Add(inst_.GetComponent<copo>());
                            }


                            StartCoroutine(reembaralhar());
                            
                            tempo_total += 0.3f;
                        }
                    }
                }
            }

            
        }
    }

    public void embaralhar(int movimentos_, float duracao_){
        tempo_embaralhamento = 0;
        embaralhando = true;
        for (int i_ = 0; i_ < movimentos_; i_++)
        {
            List<int> ordem = ordemAleatoria(copos.Count);

            for (int i__ = 0; i__ < copos.Count; i__++)
            {
                copos[i__].mudarPosicao(copos.Count % 2 == 0 ? float.Parse(ordem[i__].ToString())  - (float.Parse(copos.Count.ToString()) / 2) + 0.5f : float.Parse(ordem[i__].ToString()) - float.Parse(copos.Count.ToString()) / 2  + 0.5f,(duracao_ / movimentos_));
            }
        }

        if(copos.Count % 2 == 0){
            Debug.Log("PAR");
        }

        if(copos.Count % 2 == 1){
            Debug.Log("IMPAR");
        }
        
    }

    public List<int> ordemAleatoria(int tamanho_){
        List<int> lista_ = new List<int>();

        while(lista_.Count < tamanho_){
            int numero_aleatorio = 0;

            do
            {
                numero_aleatorio = Random.Range(0,tamanho_);
            } while (lista_.Contains(numero_aleatorio));

            lista_.Add(numero_aleatorio);
        }

        return lista_;
    }

    public IEnumerator restartCopoAleatorio(){
        vivo = true;

        iniciado = true;

        movimentos = 5;

        for ( int i= copos.Count - 1; i > 3; --i )
        {
            GameObject child = copos[i].gameObject;
            Destroy(child);
            copos.RemoveAt(i);
        }

        copos[0].mudarPosicao(0,0.5f);
        copos[1].mudarPosicao(1,0.5f);
        copos[2].mudarPosicao(-1,0.5f);

        copos[0].mostrar();

        for ( int i= copos.Count - 1; i >= 1; --i )
        {
            copos[i].esconder();
        }

        yield return new WaitForSeconds(0.6f);

        copos[0].esconder();

        yield return new WaitForSeconds(0.6f);

        iniciado = false;
    }

    public IEnumerator reembaralhar(){
        yield return new WaitForSeconds(0.8f);

        for ( int i= copos.Count - 1; i >= 0; --i )
        {
            copos[i].esconder();
        }

        yield return new WaitForSeconds(0.6f);

        embaralhar(movimentos,tempo_total);

    }

    public IEnumerator ganharMoeda(){

        gerGames.instancia.coletar(TipoDeColetavel.moeda);

        moeda.SetActive(true);

        yield return new WaitForSeconds(1f);

        moeda.SetActive(false);

    }
}
