using System.Collections;
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
    [SerializeField] private GameObject[] managersToInstantiate;

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

        for(int i = 0; i < managersToInstantiate.Length; i++)
        {
            GameObject prefab = managersToInstantiate[i];
            if(prefab != null)
            {
                DontDestroyOnLoad(prefab);
            }
        }

        StartCoroutine(LoadGameplayScene());
    }

    private IEnumerator LoadGameplayScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}