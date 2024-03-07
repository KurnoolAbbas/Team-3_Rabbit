using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    public void LoadMultScreenScene()
    {
        SceneManager.LoadScene("multScreen");
    }
}
