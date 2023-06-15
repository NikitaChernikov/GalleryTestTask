using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

public class ImageLoader : MonoBehaviour
{
    private static string IMAGE_URL = "http://data.ikppbb.com/test-task-unity-data/pics/";

    public event EventHandler<OnInsertImageEventArgs> OnInsertImage;

    private static string PLAYER_PREFS_KEY = "ImageToLoad"; 

    private void Start()
    {
        int imageNumber = PlayerPrefs.GetInt(PLAYER_PREFS_KEY);
        StartCoroutine(IELoadImageFromServer(imageNumber));
    }

    public void LoadImageFromServer(int imageNumber)
    {
        StartCoroutine(IELoadImageFromServer(imageNumber));
    }

    private IEnumerator IELoadImageFromServer(int imageNumber)
    {
        string imageUrl = IMAGE_URL + imageNumber + ".jpg";
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ProtocolError)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            OnInsertImage?.Invoke(this, new OnInsertImageEventArgs { texture = texture });
        }
        else
        {
            Debug.Log("Ошибка загрузки изображения: " + request.error);
        }
    }
}