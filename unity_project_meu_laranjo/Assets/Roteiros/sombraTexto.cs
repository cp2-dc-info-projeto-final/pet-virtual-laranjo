using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sombraTexto : MonoBehaviour
{

    public TextMeshProUGUI[] sombras;
    // Start is called before the first frame update

    void Update()
    {
        foreach(TextMeshProUGUI sombra in sombras){
            sombra.text = GetComponent<TextMeshProUGUI>().text;
        }
    }
}
