using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageClickListener : MonoBehaviour
{
    private static string PLAYER_PREFS_KEY = "ImageToLoad";

    [SerializeField] private int _sceneToLoad = 2;

    [SerializeField][HideInInspector] private int _imageNumber;

    public void Click()
    {
        PlayerPrefs.SetInt(PLAYER_PREFS_KEY, _imageNumber);
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void SetImageNumber(int imageNumber)
    {
        _imageNumber = imageNumber;
    }
}
