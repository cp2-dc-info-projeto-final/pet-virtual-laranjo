using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_casa : MonoBehaviour
{
    public int lugar, destino, comodo = 0;

    public bool poderodar = true;
    public GameObject[] icones_sala_0, icones_sala_1, icones_sala_2, icones_sala_3;
    Animator ani_;
    // Start is called before the first frame update
    void Start()
    {
        ani_ = GetComponent<Animator>();

        foreach (GameObject icone_ in icones_sala_0)
        {
            if (icone_ != null)
            {
                icone_.SetActive(false);
            }
        }

        foreach (GameObject icone_ in icones_sala_1)
        {
            if (icone_ != null)
            {
                icone_.SetActive(false);
            }
        }

        foreach (GameObject icone_ in icones_sala_2)
        {
            if (icone_ != null)
            {
                icone_.SetActive(false);
            }
        }

        foreach (GameObject icone_ in icones_sala_3)
        {
            if (icone_ != null)
            {
                icone_.SetActive(false);
            }
        }

        if (comodo == 0)
        {
            foreach (GameObject icone_ in icones_sala_0)
            {
                if (icone_ != null)
                {
                    icone_.SetActive(true);
                }
            }
        }
        else if (comodo == 1)
        {
            foreach (GameObject icone_ in icones_sala_1)
            {
                if (icone_ != null)
                {
                    icone_.SetActive(true);
                }
            }
        }
        else if (comodo == 2)
        {
            foreach (GameObject icone_ in icones_sala_2)
            {
                if (icone_ != null)
                {
                    icone_.SetActive(true);
                }
            }
        }
        else if (comodo == 3)
        {
            foreach (GameObject icone_ in icones_sala_3)
            {
                if (icone_ != null)
                {
                    icone_.SetActive(true);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (poderodar)
        {
            if (destino > lugar)
            {
                ani_.SetTrigger("rodarmais");

                if (comodo + 1 >= 4)
                {
                    comodo = 0;
                }
                else
                {
                    comodo++;
                }


                foreach (GameObject icone_ in icones_sala_0)
                {
                    if (icone_ != null)
                    {
                        icone_.SetActive(false);
                    }
                }

                foreach (GameObject icone_ in icones_sala_1)
                {
                    if (icone_ != null)
                    {
                        icone_.SetActive(false);
                    }
                }

                foreach (GameObject icone_ in icones_sala_2)
                {
                    if (icone_ != null)
                    {
                        icone_.SetActive(false);
                    }
                }

                foreach (GameObject icone_ in icones_sala_3)
                {
                    if (icone_ != null)
                    {
                        icone_.SetActive(false);
                    }
                }

                /*
                if (comodo == 0)
                {
                    foreach (GameObject icone_ in icones_sala_0)
                    {
                        if (icone_ != null)
                        {
                            icone_.SetActive(true);
                        }
                    }
                }
                else if (comodo == 1)
                {
                    foreach (GameObject icone_ in icones_sala_1)
                    {
                        if (icone_ != null)
                        {
                            icone_.SetActive(true);
                        }
                    }
                }
                else if (comodo == 2)
                {
                    foreach (GameObject icone_ in icones_sala_2)
                    {
                        if (icone_ != null)
                        {
                            icone_.SetActive(true);
                        }
                    }
                }
                else if (comodo == 3)
                {
                    foreach (GameObject icone_ in icones_sala_3)
                    {
                        if (icone_ != null)
                        {
                            icone_.SetActive(true);
                        }
                    }
                }*/
                //destino--;
                poderodar = false;
            }
            if (destino < lugar)
            {
                ani_.SetTrigger("rodarmenos");

                if (comodo - 1 <= -1)
                {
                    comodo = 3;
                }
                else
                {
                    comodo--;
                }


                foreach (GameObject icone_ in icones_sala_0)
                {
                    if (icone_ != null)
                    {
                        icone_.SetActive(false);
                    }
                }

                foreach (GameObject icone_ in icones_sala_1)
                {
                    if (icone_ != null)
                    {
                        icone_.SetActive(false);
                    }
                }

                foreach (GameObject icone_ in icones_sala_2)
                {
                    if (icone_ != null)
                    {
                        icone_.SetActive(false);
                    }
                }

                foreach (GameObject icone_ in icones_sala_3)
                {
                    if (icone_ != null)
                    {
                        icone_.SetActive(false);
                    }
                }

                /*
                if (comodo == 0)
                {
                    foreach (GameObject icone_ in icones_sala_0)
                    {
                        if (icone_ != null)
                        {
                            icone_.SetActive(true);
                        }
                    }
                }
                else if (comodo == 1)
                {
                    foreach (GameObject icone_ in icones_sala_1)
                    {
                        if (icone_ != null)
                        {
                            icone_.SetActive(true);
                        }
                    }
                }
                else if (comodo == 2)
                {
                    foreach (GameObject icone_ in icones_sala_2)
                    {
                        if (icone_ != null)
                        {
                            icone_.SetActive(true);
                        }
                    }
                }
                else if (comodo == 3)
                {
                    foreach (GameObject icone_ in icones_sala_3)
                    {
                        if (icone_ != null)
                        {
                            icone_.SetActive(true);
                        }
                    }
                }*/

                //destino++;
                poderodar = false;
            }
        }
    }

    public void pode(int incremento)
    {
        destino += incremento;
        poderodar = true;

        foreach (GameObject icone_ in icones_sala_0)
        {
            if (icone_ != null)
            {
                icone_.SetActive(false);
            }
        }

        foreach (GameObject icone_ in icones_sala_1)
        {
            if (icone_ != null)
            {
                icone_.SetActive(false);
            }
        }

        foreach (GameObject icone_ in icones_sala_2)
        {
            if (icone_ != null)
            {
                icone_.SetActive(false);
            }
        }

        foreach (GameObject icone_ in icones_sala_3)
        {
            if (icone_ != null)
            {
                icone_.SetActive(false);
            }
        }

        if (comodo == 0)
        {
            foreach (GameObject icone_ in icones_sala_0)
            {
                if (icone_ != null)
                {
                    icone_.SetActive(true);
                }
            }
        }
        else if (comodo == 1)
        {
            foreach (GameObject icone_ in icones_sala_1)
            {
                if (icone_ != null)
                {
                    icone_.SetActive(true);
                }
            }
        }
        else if (comodo == 2)
        {
            foreach (GameObject icone_ in icones_sala_2)
            {
                if (icone_ != null)
                {
                    icone_.SetActive(true);
                }
            }
        }
        else if (comodo == 3)
        {
            foreach (GameObject icone_ in icones_sala_3)
            {
                if (icone_ != null)
                {
                    icone_.SetActive(true);
                }
            }
        }
    }

    public void viracamera(int incremento)
    {
        destino += incremento;
    }
}



