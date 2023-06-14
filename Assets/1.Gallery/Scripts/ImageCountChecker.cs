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
            Debug.Log("���������� ����������� �� �������: " + imageCount);
        }
        else
        {
            Debug.Log("������ ��� ���������� �������: " + request.error);
        }
    }

    int CountImagesInHtml(string htmlCode)
    {
        // ���������� ��� ��� �������� ���������� ����������� � HTML-����
        // ��������, ����� ������������ ���������� ��������� ��� ������ ������ ��������� ������
        // ����������� ���������� ���������� �����������

        // ������ ���������� � �������������� ����������� ���������:
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("<img[^>]+>");
        int imageCount = regex.Matches(htmlCode).Count;
        return imageCount;
    }
}