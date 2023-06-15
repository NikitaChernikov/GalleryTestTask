using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ImageLoader : MonoBehaviour
{
    private static string PLAYER_PREFS_KEY = "ImageToLoad";

    [SerializeField] private Image _image; // ������ �� ��������� Image, ������� ����� ���������� �����������

    private string _imageUrl; // URL �����������

    IEnumerator Start()
    {
        int imageNumber = PlayerPrefs.GetInt(PLAYER_PREFS_KEY);
        _imageUrl = "http://data.ikppbb.com/test-task-unity-data/pics/" + imageNumber + ".jpg";
        yield return LoadImageFromServer();
    }

    IEnumerator LoadImageFromServer()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(_imageUrl);

        yield return request.SendWebRequest();

        // ���������, ��� �� ������ ��� �������� �����������
        if (request.result != UnityWebRequest.Result.ProtocolError)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            _image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            _image.SetNativeSize(); // ������������� ������� Image ��� ������� ������������ �����������
        }
        else
        {
            Debug.Log("������ �������� �����������: " + request.error);
        }
    }
}