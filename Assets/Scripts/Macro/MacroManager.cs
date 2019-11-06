using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MacroManager : MonoBehaviour
{
    static public PlayerInstance player;
    static public Persona opponent;

    static public MacroManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        InitiateMacroManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateMacroManager()
    {
        Instance = this;
    }

    public void InitiateFight(Persona _opponent)
    {
        opponent = _opponent;
        //GoToSceneFight
        LoadScene("Combat");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
