using UnityEngine;
using UnityEngine.SceneManagement;

public static class Bootstrapper
{
    const string sceneName = "BootstrapScene";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void LoadBootstrapScene()
    {
        for(int scene = 0; scene < SceneManager.sceneCount; scene++)
        {
            Scene sceneToCheck = SceneManager.GetSceneAt(scene);

            if (sceneToCheck.name == sceneName) return;
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }
}

public class Bootstrap : MonoBehaviour
{
    public static Bootstrap Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found Bootstrap Data on " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}