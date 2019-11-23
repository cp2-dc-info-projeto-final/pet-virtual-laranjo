using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flepi_laranjo : MonoBehaviour
{
    public bool vivo = true;
    public float escala_tela;
    public float dificuldade = 1, distancia_percorrida = 0, ultima_intancia = 0,velocidade = 6, velocidade_queda = -2, velocidade_queda_maxima = 2, velocidade_queda_minima = -2,sensibilidade = 1, multiplicador_de_giro = 1;
    public GameObject jogador, prefab_cano, pivot_canos ,pivot_de_giro, camera_pivot;
    public List<GameObject> lista_canos = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        float altura_tela = Screen.height, largura_tela = Screen.width;
        escala_tela = (16f / 9f) / (altura_tela / largura_tela);
    }

    // Update is called once per frame
    void Update()
    {
        
        float altura_tela = Screen.height, largura_tela = Screen.width;
        escala_tela = (16f / 9f) / (altura_tela / largura_tela);

        camera_pivot.transform.position = Vector3.Lerp(camera_pivot.transform.position, new Vector3(0,0,jogador.transform.position.z + 2.5f),0.9f);
        if(vivo){
            dificuldade += Time.deltaTime / (100 * dificuldade);

            if(Input.GetKeyDown(KeyCode.Mouse0)){
                velocidade_queda = velocidade_queda_maxima;
                multiplicador_de_giro = 6;
            }

            multiplicador_de_giro -= Time.deltaTime * 14;

            multiplicador_de_giro = Mathf.Clamp(multiplicador_de_giro, 1, 6);

            velocidade_queda -= Time.deltaTime * sensibilidade;

            velocidade_queda = Mathf.Clamp(velocidade_queda, velocidade_queda_minima, velocidade_queda_maxima);

            jogador.transform.Translate(new Vector3(0,velocidade_queda * Time.deltaTime, Time.deltaTime * velocidade * dificuldade * escala_tela));

            distancia_percorrida += Time.deltaTime * velocidade * dificuldade * escala_tela;

            pivot_de_giro.transform.Rotate(new Vector3(360 * Time.deltaTime * multiplicador_de_giro, 0, 0));

            if(ultima_intancia <= distancia_percorrida){
                GameObject cano_ = Instantiate(prefab_cano,new Vector3(0, Random.Range(-7,7),ultima_intancia + 20), Quaternion.Euler(0,0,0),pivot_canos.transform);

                lista_canos.Add(cano_);

                if(lista_canos.Count > 4){
                    Destroy(lista_canos[0]);
                    lista_canos.RemoveAt(0);
                }

                ultima_intancia += 10;
            }
        }
        
    }

    public void restartFlepiLaranjo(){
        vivo = true;

        dificuldade = 1;

        velocidade_queda = velocidade_queda_maxima;
        
        jogador.transform.position = new Vector3(0,0,0);
        jogador.transform.rotation = Quaternion.Euler(0,0,0);

        distancia_percorrida = 0;
        ultima_intancia = 0;

        for ( int i= pivot_canos.transform.childCount - 1; i>=0; --i )
        {
            GameObject child = pivot_canos.transform.GetChild(i).gameObject;
            Destroy(child );
        }

        lista_canos = new List<GameObject>();
    }

    private void OnTriggerEnter(Collider col_) {
        if(col_.tag == "obstaculo"){
            vivo = false;
        }

        if(col_.tag == "marca"){
            gerGames.instancia.pontuar();
        }
    }
}
