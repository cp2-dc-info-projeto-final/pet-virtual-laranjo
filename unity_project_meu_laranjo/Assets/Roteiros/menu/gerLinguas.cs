using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gerLinguas : MonoBehaviour
{
    public static gerLinguas instancia;
    public TMP_Dropdown dropdown_liguas;
    public texto[] textos;
    // Start is called before the first frame update

    void Awake() {
        instancia = this;
    }
    void Start()
    {
        atualizarLingua();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void atualizarLingua(){
        foreach(texto txt_ in textos){
            txt_.txt.text = txt_.traducao[gerDados.instancia.dados_.lingua];
        }
    }

    public void mudarLingua(int i_){
        gerDados.instancia.dados_.lingua = i_;
        dropdown_liguas.value = i_;
        atualizarLingua();
        gerDados.instancia.salvar();
    }

    
}

[System.Serializable]
public class texto
{
    public TextMeshProUGUI txt;
    public string[] traducao = new string[5];
}