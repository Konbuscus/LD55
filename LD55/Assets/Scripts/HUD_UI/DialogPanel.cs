using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogPanel : MonoBehaviour
{
    public Image picture;
    public TextMeshProUGUI name; 
    public TextMeshProUGUI text;

    public static DialogPanel instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDialog(Sprite sprite, string name, string text)
    {
        picture.sprite = sprite;
        this.name.text = name;
        this.text.text = text;
    }
}
