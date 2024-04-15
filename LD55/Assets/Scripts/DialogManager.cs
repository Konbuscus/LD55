using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public class DialogLine
{
    public string spriteName;
    public string name;
    public string text;

    public DialogLine(string spriteName, string name, string text)
    {
        this.spriteName = spriteName;
        this.name = name;
        this.text = text;
    }
}

public class DialogManager : MonoBehaviour
{
    public List<MyObjectObject<string, Sprite>> pictures;

    private Dictionary<string, List<DialogLine>> dialogs = new Dictionary<string, List<DialogLine>>();

    private string currentDialog;
    private int dialogLineIndex;

    // Start is called before the first frame update
    void Start()
    {
        AddIntroDialog();
        StartDialog("Intro");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddIntroDialog()
    {
        dialogs.Add("Intro", new List<DialogLine>());
        dialogs["Intro"].Add(new DialogLine("player", "Me", "3 month have passed since the day I came here !\nAt first, they said that it will be a simple experiment about human and nature.\nBut now, they won't let me go out.\nI'm a prisoner ..."));
        dialogs["Intro"].Add(new DialogLine("player", "Me", "I need to find a way to escape ! But how ..."));
        dialogs["Intro"].Add(new DialogLine("player", "Me", "The main reason they give me to why they chose me, is that disabled people are more receiptive and have greater adaptation abilities.\nI think that the real reason is that I can't escape easily ..."));
        dialogs["Intro"].Add(new DialogLine("player", "Me", "Recently, thanks to those experiments, I manage to communicate with animals.\nI can only understand basic things, but it's better than nothing and I think I'm getting better day by day."));
        dialogs["Intro"].Add(new DialogLine("player", "Me", "I only there was ..."));
    }

    public void StartDialog(string dialogName)
    {
        currentDialog = dialogName;
        dialogLineIndex = -1;
        NextDialogLine();
    }

    public void NextDialogLine()
    {
        dialogLineIndex++;
        if (dialogLineIndex < dialogs[currentDialog].Count)
        {
            DialogLine tmp = dialogs[currentDialog][dialogLineIndex];
            Debug.Log(pictures.First(x => x.obj1 == tmp.spriteName).obj2);
            DialogPanel.instance.SetDialog(pictures.First(x => x.obj1 == tmp.spriteName).obj2, tmp.name, tmp.text);
            DialogPanel.instance.gameObject.SetActive(true);
        }
        else
        {
            DialogPanel.instance.gameObject.SetActive(false);
        }
    }

    void OnNextDialog(InputValue v)
    {
        NextDialogLine();
    }
}
