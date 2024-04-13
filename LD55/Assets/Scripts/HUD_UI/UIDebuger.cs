using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDebuger : MonoBehaviour
{
    private Dictionary<string, TextMeshProUGUI> elements;

    private static UIDebuger instance;

    public GameObject Line;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        elements = new Dictionary<string, TextMeshProUGUI>();
    }

    public static void DisplayValue(string key, string value)
    {
        if (!instance.elements.ContainsKey(key))
        {
            GameObject line = Instantiate(instance.Line, instance.transform);
            instance.elements.Add(key, line.GetComponent<TextMeshProUGUI>());
        }
        instance.elements[key].text = value;
    }
}
