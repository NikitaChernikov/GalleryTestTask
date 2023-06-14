using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ImageCountChecker : MonoBehaviour
{
    private string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";

    void Start()
    {
        StartCoroutine(GetImageCount());
    }

    IEnumerator GetImageCount()
    {
        UnityWebRequest request = UnityWebRequest.Get(baseUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string htmlCode = request.downloadHandler.text;
            int imageCount = CountImagesInHtml(htmlCode);
            Debug.Log("Количество изображений на сервере: " + imageCount);
        }
        else
        {
            Debug.Log("Ошибка при выполнении запроса: " + request.error);
        }
    }

    int CountImagesInHtml(string htmlCode)
    {
        // Реализуйте код для подсчета количества изображений в HTML-коде
        // Например, можно использовать регулярные выражения или другие методы обработки текста
        // Возвращайте полученное количество изображений

        // Пример реализации с использованием регулярного выражения:
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("<img[^>]+>");
        int imageCount = regex.Matches(htmlCode).Count;
        return imageCount;
    }
}