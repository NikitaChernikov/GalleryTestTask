using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class GalleryLoader : MonoBehaviour
{
    public RawImage image; // ������ �� ��������� RawImage, ������� ����� ���������� �����������

    private string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private int currentImageIndex = 1; // ��������� ������ �����������
    private int totalImages = 1; // ����� ���������� �����������

    IEnumerator Start()
    {
        // ��������� ������ ����������� ��� ������� �������
        yield return LoadImageFromServer();

        // ���������� �������� ����������� � ��������� � 2 ������� ����� ����
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

        // ���������, ��� �� ������ ��� �������� �����������
        if (www.result != UnityWebRequest.Result.ProtocolError)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            image.texture = texture;
            image.SetNativeSize(); // ������������� ������� RawImage ��� ������� ������������ �����������
        }
        else
        {
            Debug.Log("������ �������� �����������: " + www.error);
        }
    }
}