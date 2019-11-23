using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulo_infinito : MonoBehaviour
{
    public bool vivo = true, teclado = false;
    public Vector3 giro_;

    public float vel_esq = 0, vel_dir = 0, escala_tela;

    public float dificuldade = 1, distancia_percorrida = 0, ultima_intancia = 0,velocidade = 6, velocidade_queda = -2, velocidade_queda_maxima = 2, velocidade_queda_minima = -2,sensibilidade = 1, multiplicador_de_giro = 1, velocidade_lado = 0, multiplicador_velecidade_lado = 1, angulo_visao = 90;

    public GameObject camera_pivot, paredes, pivot_plataforma;
    public GameObject[] prefab_plataforma;
    public Rigidbody rb_;
    public List<GameObject> plataformas = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();

        float altura_tela = Screen.height, largura_tela = Screen.width;
        escala_tela = (16f / 9f) / (altura_tela / largura_tela);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            restartPuloInfinito();
        }

        dificuldade += Time.deltaTime / (100 * dificuldade);
        float altura_tela = Screen.height, largura_tela = Screen.width;
        escala_tela = (16f / 9f) / (altura_tela / largura_tela);
        
        paredes.transform.localScale = new Vector3(1,1,escala_tela);
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)){
            teclado = true;
        }
        if(vivo){
            rb_.isKinematic = false;

            giro_ = Input.acceleration;

            if(teclado){
                
                if(Input.GetKey(KeyCode.A)){
                    vel_esq -= Time.deltaTime * 2;
                }else
                {
                     vel_esq += Time.deltaTime * 2;
                }

                if(Input.GetKey(KeyCode.D)){
                    vel_dir += Time.deltaTime * 2;
                }else
                {
                     vel_dir -= Time.deltaTime * 2;
                }

                vel_esq = Mathf.Clamp(vel_esq,-1,0);
                vel_dir = Mathf.Clamp(vel_dir,0,1);

                velocidade_lado = (vel_esq + vel_dir) * multiplicador_velecidade_lado * escala_tela;
                
            }else
            {
                velocidade_lado = Input.acceleration.x * multiplicador_velecidade_lado * escala_tela;
            }


            angulo_visao += 120 * velocidade_lado;

            angulo_visao = Mathf.Clamp(angulo_visao, 0, 90);

            
            transform.GetChild(0).rotation = Quaternion.Euler(0, 90 - angulo_visao + 45,0);
            

            camera_pivot.transform.position = Vector3.Lerp(camera_pivot.transform.position,new Vector3(0,distancia_percorrida + 12,0),Time.deltaTime * 8);

            velocidade_queda -= sensibilidade * Time.deltaTime;

            velocidade_queda = Mathf.Clamp(velocidade_queda, velocidade_queda_minima, velocidade_queda_maxima);
            rb_.MovePosition(transform.position + new Vector3(0,Time.deltaTime * velocidade_queda,velocidade_lado));
        }else
        {
            rb_.isKinematic = true;
        }
    }
    
    private void OnCollisionEnter(Collision col_) {
        float altura_tela = Screen.height, largura_tela = Screen.width;

        if(velocidade_queda <= 0){
            if(col_.transform.tag == "plataforma"){
                velocidade_queda = velocidade_queda_maxima;

                if(transform.position.y > distancia_percorrida + 5){
                distancia_percorrida += 5;

                GameObject inst_ = Instantiate(prefab_plataforma[(distancia_percorrida + 12) / 5 < 194 ? 0 : (distancia_percorrida + 12) / 5 < 994 ?  1 : 2],new Vector3(0,distancia_percorrida + 29,Random.Range(-5f * escala_tela,5f * escala_tela)), Quaternion.Euler(0,0,0),pivot_plataforma.transform);
                inst_.transform.GetChild(0).localScale = new Vector3(0.4f,1,Random.Range(0.2f, 0.3f) * (escala_tela) * (1 / dificuldade));

                plataformas.Add(inst_);

                distancia_percorrida += 5;

                inst_ = Instantiate(prefab_plataforma[(distancia_percorrida + 12) / 5 < 194 ? 0 : (distancia_percorrida + 12) / 5 < 994 ?  1 : 2],new Vector3(0,distancia_percorrida + 29,Random.Range(-5f * escala_tela,5f * escala_tela)), Quaternion.Euler(0,0,0),pivot_plataforma.transform);
                inst_.transform.GetChild(0).localScale = new Vector3(0.4f,1,Random.Range(0.2f, 0.3f) * (escala_tela) * (1 / dificuldade));

                plataformas.Add(inst_);

                if(plataformas.Count >= 10){
                    Destroy(plataformas[0]);
                    plataformas.RemoveAt(0);
                    Destroy(plataformas[0]);
                    plataformas.RemoveAt(0);
                }

                gerGames.instancia.pontuar(2);
                }else
                {
                    if(transform.position.y > distancia_percorrida){
                        distancia_percorrida += 5;

                        GameObject inst_ = Instantiate(prefab_plataforma[(distancia_percorrida + 12) / 5 < 194 ? 0 : (distancia_percorrida + 12) / 5 < 994 ?  1 : 2],new Vector3(0,distancia_percorrida + 29,Random.Range(-5f * escala_tela,5f * escala_tela)), Quaternion.Euler(0,0,0),pivot_plataforma.transform);
                        inst_.transform.GetChild(0).localScale = new Vector3(0.4f,1,Random.Range(0.2f, 0.3f) * (escala_tela) * (1 / dificuldade));

                        plataformas.Add(inst_);

                        if(plataformas.Count >= 10){
                            Destroy(plataformas[0]);
                            plataformas.RemoveAt(0);
                        }

                        gerGames.instancia.pontuar();
                    }
                }
            }
        }
        

        if(col_.transform.tag == "obstaculo"){
            vivo = false;
            
        }

        if(col_.transform.tag == "teleporte_01"){
            transform.position = new Vector3(transform.position.x, transform.position.y,7.9f * (escala_tela));
        }

        if(col_.transform.tag == "teleporte_02"){
            transform.position = new Vector3(transform.position.x, transform.position.y,-7.9f * (escala_tela));
        }
    }

    public void restartPuloInfinito(){

        distancia_percorrida = -12;

        dificuldade = 1;

        velocidade_lado = 0;

        velocidade_queda = 0;


        float altura_tela = Screen.height, largura_tela = Screen.width;
        escala_tela = (16f / 9f) / (altura_tela / largura_tela);

        camera_pivot.transform.position = new Vector3(0,0,0);

        transform.position = new Vector3(0,-10.5f,0);
        angulo_visao = 90;

        for ( int i= plataformas.Count - 1; i>=0; --i )
        {
            GameObject child = plataformas[i];
            Destroy(child);
        }

        plataformas = new List<GameObject>();



        for(int i_ = 0; i_ < 6; i_++){
            GameObject inst_ = Instantiate(prefab_plataforma[0],new Vector3(0,-8 + 5 * i_,Random.Range(-5f * escala_tela,5f * escala_tela)), Quaternion.Euler(0,0,0),pivot_plataforma.transform);
            inst_.transform.GetChild(0).localScale = new Vector3(0.4f,1,Random.Range(0.2f, 0.3f) * (escala_tela) * (1 / dificuldade));

            plataformas.Add(inst_);
        }

        vivo = true;
    }
}
