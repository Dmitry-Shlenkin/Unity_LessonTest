using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void ChangeLevel(int num)
    {
        SceneManager.LoadScene(num);
    }
}
