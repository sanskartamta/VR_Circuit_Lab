using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameStartManager : MonoBehaviour
{
    [Header("UI Pages")]
    public GameObject mainMenu;
    public GameObject options;
    public GameObject about;

    [Header("Main Menu Buttons")]
    public GameObject startButtonObject;
    public GameObject optionButtonObject;
    public GameObject aboutButtonObject;
    public GameObject quitButtonObject;

    public List<GameObject> returnButtonObjects;

    void Start()
    {
        // Assign actions using EventTrigger
        AddEventTrigger(startButtonObject, StartGame);
        AddEventTrigger(optionButtonObject, EnableOption);
        AddEventTrigger(aboutButtonObject, EnableAbout);
        AddEventTrigger(quitButtonObject, QuitGame);

        foreach (var returnButton in returnButtonObjects)
        {
            AddEventTrigger(returnButton, EnableMainMenu);
        }

        EnableMainMenu();
    }

    private void AddEventTrigger(GameObject buttonObject, System.Action action)
    {
        EventTrigger trigger = buttonObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = buttonObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((_) => action.Invoke());
        trigger.triggers.Add(entry);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        HideAll();
        SceneTransitionManager.singleton.GoToSceneAsync(1);
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(false);
    }

    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        about.SetActive(false);
    }

    public void EnableOption()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        about.SetActive(false);
    }

    public void EnableAbout()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        about.SetActive(true);
    }
}
