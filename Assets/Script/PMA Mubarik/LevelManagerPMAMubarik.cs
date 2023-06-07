using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
//liberary that uses await.task 
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerPMAMubarik : MonoBehaviour
{
    //Tarodev - https://www.youtube.com/watch?v=OmobsXZSRKo&ab_channel=Tarodev
    public static LevelManagerPMAMubarik instance;
    // by making it a static variable it becomes a singleton pattern since it ensures that only 1instance exists throughout the application
    //references in inspector
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;

    private void Awake()
    { //If the scene we currently are in gets destroyed,this wont be destroyed inbetween scenes.
        // if it is already assigned then destroy this object.
        if(instance == null)
        {
            //This keyword refers to the current instance of the class, it means that the curretn instance of the script is being assigned to the instance variable.
            //By assigning the current instance to a static variable it allows other scripts to access the functionality without needing a direct reference to the gameobject - Can be access by its class name - LevelManagerPMaMubarik.instance.
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //async function (scenename as parameter)
    // Async marks a method as asynchronous meaning it can be run in the background while another code executes.
    //the await keyword indicates that the method should wait for the result of a asynchronouys operation before continuing https://www.c-sharpcorner.com/article/async-and-await-in-c-sharp/
    public async void loadScene(string sceneName)
    {
        // var keyword is used to declare a var type variable, the varr type variable can be used to store a simple/complex data type - is more work to determine the type for the complier(not cost effective)
        //LSA is used since LS forces all previous AsyncOperations to complete (Loading set to completet in the next rendered frame)
        var scene = SceneManager.LoadSceneAsync(sceneName);
        //wont allow the scene to activate straight away.
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);

        //do-while loop is the same a while loop, the difference is that the logic will runs once.
        // then once it getst down to the while condition it will run like a normal while loop( scene)
        //Unity has implemented that 0 to 0.9 is for loading the data - 0.9-1 is for setting up scene/gameobj. https://docs.unity3d.com/ScriptReference/AsyncOperation-progress.html
        // await task.delay is a artifical delay to avoid instant scene change to view the progressbar
        do
        {
            await Task.Delay(100);
            _progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9f);



        scene.allowSceneActivation = true;
        //Disables loadingCanvas since the new scene is loaded
        _loaderCanvas.SetActive(false);

    }

}
