using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatUpText : MonoBehaviour
{
    Text text;
    float alpha = 1;
    
    void Start()
    {
        text = this.GetComponent<Text>();
        text.color = Color.white;
    }

    
    void Update()
    {
        this.transform.Translate(0, 20, 0);
        alpha -= 0.05f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

        if (alpha < 0) Destroy(this.gameObject);
    }
}
