using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fixar_ui_quadrada : MonoBehaviour
{
    public RectTransform rect_;

    public float width_, height_;
    // Start is called before the first frame update
    void Start()
    {
        rect_ = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Screen.height > Screen.width){
            rect_.localScale = Vector3.one;
        }else
        {
            width_ = Screen.width;
            height_ = Screen.height;

            rect_.localScale = new Vector3(height_ / width_,height_ / width_,1);
        }
    }
}
