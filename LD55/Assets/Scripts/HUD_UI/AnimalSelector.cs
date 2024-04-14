using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AnimalSelector : MonoBehaviour
{
    public GameObject animalSelectorItemPrefab;

    private List<MyObjectObject<AnimalType, GameObject>> animalSelectorItem = new List<MyObjectObject<AnimalType, GameObject>>();

    public List<MyObjectObject<AnimalType, Sprite>> sprites;

    private int selectedIndex = 0;

    public static AnimalSelector instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAnimalSelector(AnimalType animalType)
    {
        GameObject tmp = GameObject.Instantiate(animalSelectorItemPrefab, transform);
        tmp.transform.Find("Selector/bg/Image").GetComponent<Image>().sprite = sprites.First(x => x.obj1 == animalType).obj2;
        animalSelectorItem.Add(new MyObjectObject<AnimalType, GameObject>(animalType, tmp));
        selectedIndex = -1;
        ChangeSelection(true);
    }

    public void ChangeSelection(bool down)
    {
        selectedIndex = (selectedIndex + (down ? 1 : -1)) % animalSelectorItem.Count;
        if(selectedIndex < 0) selectedIndex = animalSelectorItem.Count - 1;

        for(int i = 0; i < animalSelectorItem.Count; i++)
        {
            animalSelectorItem[i].obj2.transform.Find("Selector").GetComponent<Image>().enabled = (i == selectedIndex);
        }
    }
}
