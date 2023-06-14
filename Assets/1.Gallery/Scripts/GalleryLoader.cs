using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GalleryLoader : MonoBehaviour
{
    public RawImage image; // Ссылка на компонент RawImage, который будет отображать изображение

    private string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private int currentImageIndex = 1; // Начальный индекс изображения
    private int totalImages = 1; // Общее количество изображений

    IEnumerator Start()
    {
        // Загружаем первое изображение при запуске скрипта
        yield return LoadImageFromServer();

        // Продолжаем загрузку изображений с задержкой в 2 секунды между ними
        while (currentImageIndex < totalImages)
        {
            yield return new WaitForSeconds(2);
            currentImageIndex++;
            yield return LoadImageFromServer();
        }
    }

    IEnumerator LoadImageFromServer()
    {
        string imageUrl = baseUrl + currentImageIndex + ".jpg";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);

        yield return www.SendWebRequest();

        // Проверяем, нет ли ошибок при загрузке изображения
        if (www.result != UnityWebRequest.Result.ProtocolError)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            image.texture = texture;
            image.SetNativeSize(); // Устанавливаем размеры RawImage под размеры загруженного изображения
        }
        else
        {
            Debug.Log("Ошибка загрузки изображения: " + www.error);
        }
    }
}