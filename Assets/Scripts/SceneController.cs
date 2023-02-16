using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static SceneController _instance = null;

    private int _currentSceneIndex;
    private int _timeBeforeLoad = 3;
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);

        Initialize();
    }

    private void Initialize()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        GlobalEventManager.OnLevelComplete.AddListener(() => Invoke("LoadNextLevel", _timeBeforeLoad));
        GlobalEventManager.OnObstacleCollision.AddListener(() => Invoke("RestartLevel", _timeBeforeLoad));
    }

    private void LoadNextLevel()
    {
        if (_currentSceneIndex + 1 > SceneManager.sceneCount)
        {
            RestartLevel();
            return;
        }

        SceneManager.LoadScene(++_currentSceneIndex);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }
}
