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
    public EventName eventName;

    public DialogLine(string spriteName, string name, string text)
    {
        this.spriteName = spriteName;
        this.name = name;
        this.text = text;
    }

    public DialogLine(EventName eventName)
    {
        this.eventName = eventName;
    }
}

public class DialogManager : MonoBehaviour
{
    public List<MyObjectObject<string, Sprite>> pictures;

    private Dictionary<string, List<DialogLine>> dialogs = new Dictionary<string, List<DialogLine>>();

    private string currentDialog;
    private int dialogLineIndex;

    public static DialogManager instance;

    private float refTime;
    private bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        refTime = Time.time;

        AddIntroDialog();
        AddRoomOutDialog();
        AddBeforeFalseExit();
        AddFalseExit();
        AddClosedDoors();
        AddSawElevator();
        AddInFrontOfElevator();
        AddInsideElevator();
        AddRoomIn();
        //StartDialog("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && refTime + 0.1f < Time.time)
        {
            hasStarted = true;
            StartDialog("Intro");
        }
    }

    void AddIntroDialog()
    {
        dialogs.Add("Intro", new List<DialogLine>());
        dialogs["Intro"].Add(new DialogLine("player", "Me", "3 month have passed since the day I came here !\nAt first, they said that it will be a simple experiment about human and nature.\nBut now, they won't let me go out.\nI'm a prisoner ..."));
        dialogs["Intro"].Add(new DialogLine("player", "Me", "I need to find a way to escape ! But how ..."));
        dialogs["Intro"].Add(new DialogLine("player", "Me", "The main reason they give me to why they chose me, is that disabled people are more receiptive and have greater adaptation abilities.\nI think that the real reason is that I can't escape easily ..."));
        dialogs["Intro"].Add(new DialogLine("player", "Me", "Recently, thanks to those experiments, I manage to communicate with animals.\nI can only understand basic things, but it's better than nothing and I think I'm getting better day by day."));
        dialogs["Intro"].Add(new DialogLine("player", "Me", "Wait !\nMaybe that i can try to invoke a bird to help me !"));
        dialogs["Intro"].Add(new DialogLine("none", "hint", "<Press the 'F' key to invoke a bird>"));
    }

    void AddRoomOutDialog()
    {
        dialogs.Add("RoomOut", new List<DialogLine>());
        dialogs["RoomOut"].Add(new DialogLine("bird", "Bird", "Folow me !!!"));
        //dialogs["RoomOut"].Add(new DialogLine(EventName.RoomOut3));
    }

    void AddBeforeFalseExit()
    {
        dialogs.Add("BeforeFalseExit", new List<DialogLine>());
        dialogs["BeforeFalseExit"].Add(new DialogLine("bird", "bird", "Here !\nA fire exit !!!\nAnd there is no one arround !"));
    }

    void AddFalseExit()
    {
        dialogs.Add("FalseExit", new List<DialogLine>());
        dialogs["FalseExit"].Add(new DialogLine("bird", "bird", "The fire exit is rigth here !"));
        dialogs["FalseExit"].Add(new DialogLine("bird", "bird", "Come upstair !"));
        dialogs["FalseExit"].Add(new DialogLine("bird", "bird", "I can already feel the fresh air !"));
        dialogs["FalseExit"].Add(new DialogLine("bird", "bird", "Wait... I forgot that you ..."));
        dialogs["FalseExit"].Add(new DialogLine("player", "me", "..."));
        dialogs["FalseExit"].Add(new DialogLine("bird", "bird", "..."));
        dialogs["FalseExit"].Add(new DialogLine("player", "me", "..."));
        dialogs["FalseExit"].Add(new DialogLine("bird", "bird", "It's ok.\nIf you manage to get here, there must be an elevator somewhere."));
        dialogs["FalseExit"].Add(new DialogLine("bird", "bird", "Let's search elsewhere !"));
    }

    void AddClosedDoors()
    {
        dialogs.Add("ClosedDoors", new List<DialogLine>());
        dialogs["ClosedDoors"].Add(new DialogLine("bird", "bird", "Damn !\nAll these doors are in locked in security mode since the server system failure."));
        dialogs["ClosedDoors"].Add(new DialogLine("bird", "bird", "I hope there is one that is not locked !"));
    }

    void AddSawElevator()
    {
        dialogs.Add("SawElevator", new List<DialogLine>());
        dialogs["SawElevator"].Add(new DialogLine("bird", "bird", "I can see the elevator !"));
        dialogs["SawElevator"].Add(new DialogLine("bird", "bird", "Hurry up before the system restarts !!!"));
    }

    void AddInFrontOfElevator()
    {
        dialogs.Add("InFrontOfElevator", new List<DialogLine>());
        dialogs["InFrontOfElevator"].Add(new DialogLine("bird", "bird", "Use the key card !"));
    }

    void AddInsideElevator()
    {
        dialogs.Add("InsideElevator", new List<DialogLine>());
        dialogs["InsideElevator"].Add(new DialogLine("bird", "bird", "We made it !\nLet's go to the surface !!!"));
    }

    void AddRoomIn()
    {
        dialogs.Add("RoomIn", new List<DialogLine>());
        dialogs["RoomIn"].Add(new DialogLine("player", "me", "IT WORKED !!!"));
        dialogs["RoomIn"].Add(new DialogLine("player", "me", "Now help me little bird !"));
        dialogs["RoomIn"].Add(new DialogLine("player", "me", "<Select the bird with 'E' then select the card with 'E'>"));
        dialogs["RoomIn"].Add(new DialogLine("player", "me", "<When the bird come back, press 'E' to take the card>"));
    }

    public void StartDialog(string dialogName)
    {
        currentDialog = dialogName;
        dialogLineIndex = -1;
        NextDialogLine();
    }

    public void AddNextEventToCurrentDialog(EventName enventName)
    {
        dialogs[currentDialog].Add(new DialogLine(enventName));
    }

    public void NextDialogLine()
    {
        dialogLineIndex++;
        if (dialogLineIndex < dialogs[currentDialog].Count)
        {
            DialogLine tmp = dialogs[currentDialog][dialogLineIndex];
            if (tmp.text != null)
            {
                DialogPanel.instance.SetDialog(pictures.First(x => x.obj1 == tmp.spriteName).obj2, tmp.name, tmp.text);
                DialogPanel.instance.gameObject.SetActive(true);
            }
            else if(tmp.eventName != EventName.None)
            {
                DialogPanel.instance.gameObject.SetActive(false);
                EventManager_.instance.TrigerEvent(tmp.eventName);
                NextDialogLine();
            }
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
