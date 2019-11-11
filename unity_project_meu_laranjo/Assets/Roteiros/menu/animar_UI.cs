using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animar_UI : MonoBehaviour
{
    public direcao_animacao direcao_;
    public bool aparecendo, escala;
    public GameObject scroll_vertical, scroll_horizontal;
    // Start is called before the first frame update
    void Start()
    {
        if(escala){
            if(!aparecendo){
                LeanTween.scale(gameObject,Vector3.zero,0).setEaseOutBack();
            }
        }else{
            if(direcao_ == direcao_animacao.cima){
                if(!aparecendo){
                    LeanTween.scaleX(gameObject,0,0).setEaseOutBack();
                    LeanTween.moveLocalY(gameObject,1280 * 2,0).setEaseOutCirc();
                }
            }

            if(direcao_ == direcao_animacao.baixo){
                if(!aparecendo){
                    LeanTween.scaleX(gameObject,0,0).setEaseOutBack();
                    LeanTween.moveLocalY(gameObject, - 1280 * 2,0).setEaseOutCirc();
                }
            }

            if(direcao_ == direcao_animacao.esquerda){
                if(!aparecendo){
                    LeanTween.scaleY(gameObject,0,0).setEaseOutBack();
                    LeanTween.moveLocalX(gameObject, - 1280 * 2,0).setEaseOutCirc();
                }
            }

            if(direcao_ == direcao_animacao.direita){
                if(!aparecendo){
                    LeanTween.scaleY(gameObject,0,0).setEaseOutBack();
                    LeanTween.moveLocalX(gameObject, 1280 * 2,0).setEaseOutCirc();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void mostrar_ocultar(){
        
        StopCoroutine(ajustarScrolls());
        if(scroll_vertical != null || scroll_horizontal != null){
            StartCoroutine(ajustarScrolls());
        }

        if(escala){
            if(aparecendo){
                LeanTween.scale(gameObject,Vector3.zero,0.4f).setEaseOutBack();
                aparecendo = false;
            }else{

                LeanTween.scale(gameObject,Vector3.one,0.4f).setEaseOutBack();
                aparecendo = true;
            }
        }else
        {
            if(aparecendo){
                if(direcao_ == direcao_animacao.cima){
                    LeanTween.scaleX(gameObject,0,0.4f).setEaseOutBack();
                    LeanTween.moveLocalY(gameObject, 1280 * 2,0.4f).setEaseOutCirc();
                    aparecendo = false;
                }

                if(direcao_ == direcao_animacao.baixo){
                    LeanTween.scaleX(gameObject,0,0.4f).setEaseOutBack();
                    LeanTween.moveLocalY(gameObject, - 1280 * 2,0.4f).setEaseOutCirc();
                    aparecendo = false;
                }

                if(direcao_ == direcao_animacao.esquerda){
                    LeanTween.scaleY(gameObject,0,0.4f).setEaseOutBack();
                    LeanTween.moveLocalX(gameObject, - 1280 * 2,0.4f).setEaseOutCirc();
                    aparecendo = false;
                }

                if(direcao_ == direcao_animacao.direita){
                    LeanTween.scaleY(gameObject,0,0.4f).setEaseOutBack();
                    LeanTween.moveLocalX(gameObject, 1280 * 2,0.4f).setEaseOutCirc();
                    aparecendo = false;
                }
            }else
            {

                if(direcao_ == direcao_animacao.cima || direcao_ == direcao_animacao.baixo){
                    LeanTween.scaleX(gameObject,1,0.4f).setEaseOutBack();
                    LeanTween.moveLocalY(gameObject,0,0.4f).setEaseOutBack();
                    aparecendo = true;
                }

                if(direcao_ == direcao_animacao.direita || direcao_ == direcao_animacao.esquerda){
                    LeanTween.scaleY(gameObject,1,0.4f).setEaseOutBack();
                    LeanTween.moveLocalX(gameObject,0,0.4f).setEaseOutBack();
                    aparecendo = true;
                }
            }
        }
    }

    public void mostrar_ocultar(bool bool_){

        StopCoroutine(ajustarScrolls());

        if(scroll_vertical != null || scroll_horizontal != null){
            StartCoroutine(ajustarScrolls());
        }
        

        if(bool_ == false){
            if(aparecendo){
                if(escala){
                    if(aparecendo){

                        LeanTween.scale(gameObject,Vector3.zero,0.4f).setEaseOutBack();
                        aparecendo = false;
                    }else{

                        LeanTween.scale(gameObject,Vector3.one,0.4f).setEaseOutBack();
                        aparecendo = true;
                    }
                }else
                {
                    if(aparecendo){

                        if(direcao_ == direcao_animacao.cima){
                            LeanTween.scaleX(gameObject,0,0.4f).setEaseOutBack();
                            LeanTween.moveLocalY(gameObject, 1280 * 2,0.4f).setEaseOutCirc();
                            aparecendo = false;
                        }

                        if(direcao_ == direcao_animacao.baixo){
                            LeanTween.scaleX(gameObject,0,0.4f).setEaseOutBack();
                            LeanTween.moveLocalY(gameObject, - 1280 * 2,0.4f).setEaseOutCirc();
                            aparecendo = false;
                        }

                        if(direcao_ == direcao_animacao.esquerda){
                            LeanTween.scaleY(gameObject,0,0.4f).setEaseOutBack();
                            LeanTween.moveLocalX(gameObject, - 1280 * 2,0.4f).setEaseOutCirc();
                            aparecendo = false;
                        }

                        if(direcao_ == direcao_animacao.direita){
                            LeanTween.scaleY(gameObject,0,0.4f).setEaseOutBack();
                            LeanTween.moveLocalX(gameObject, 1280 * 2,0.4f).setEaseOutCirc();
                            aparecendo = false;
                        }
                    }else
                    {

                        if(direcao_ == direcao_animacao.cima || direcao_ == direcao_animacao.baixo){
                            LeanTween.scaleX(gameObject,1,0.4f).setEaseOutBack();
                            LeanTween.moveLocalY(gameObject,0,0.4f).setEaseOutBack();
                            aparecendo = true;
                        }

                        if(direcao_ == direcao_animacao.direita || direcao_ == direcao_animacao.esquerda){
                            LeanTween.scaleY(gameObject,1,0.4f).setEaseOutBack();
                            LeanTween.moveLocalX(gameObject,0,0.4f).setEaseOutBack();
                            aparecendo = true;
                        }
                    }
                }
            }
        }
        
    }

    public IEnumerator ajustarScrolls(){

        

        yield return new WaitForSeconds(0.2f);

        if(scroll_vertical != null){
            scroll_vertical.GetComponent<RectTransform>().localPosition = new Vector3(0,-50,0);
        }

        if(scroll_horizontal != null){
            scroll_horizontal.GetComponent<RectTransform>().localPosition = new Vector3(50,0,0);
        }
    }
}

public enum direcao_animacao{
    cima,
    baixo,
    esquerda,
    direita
}
