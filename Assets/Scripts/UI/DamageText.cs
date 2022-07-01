using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public Color myColor;

    private void Awake()
    {
        myColor = this.transform.GetComponent<TextMesh>().color;
    }


    // Update is called once per frame
    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        myColor.a = Mathf.Lerp(myColor.a, 0.0f, Time.deltaTime * 5.0f);
        this.transform.GetComponent<TextMesh>().color = myColor;

    }

    
}
