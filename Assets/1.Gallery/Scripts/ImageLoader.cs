using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

public class ImageLoader : MonoBehaviour
{
    public event EventHandler<OnInsertImageEventArgs> OnInsertImage;

    private static string PLAYER_PREFS_KEY = "ImageToLoad"; 

    private string _imageUrl; 

    IEnumerator Start()
    {
        int imageNumber = PlayerPrefs.GetInt(PLAYER_PREFS_KEY);
        _imageUrl = "http://data.ikppbb.com/test-task-unity-data/pics/" + imageNumber + ".jpg";
        yield return LoadImageFromServer();
    }

    private IEnumerator LoadImageFromServer()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(_imageUrl);

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