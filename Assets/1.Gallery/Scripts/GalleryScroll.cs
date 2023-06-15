using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GalleryScroll : MonoBehaviour
{
    private static string baseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";

    [SerializeField] private GameObject _imagePrefab; 
    [SerializeField] private Transform _imageContainer; 
    [SerializeField] private ScrollRect _scrollRect; 
    [SerializeField] private GridLayoutGroup _gridLayout;
    
    private int _currentImageIndex = 1; 
    private int _totalImages; 
    private float _rowHeight; 

    private List<GameObject> _loadedImages = new List<GameObject>(); 

    private void Start()
    {
        StartCoroutine(LoadImages());
    }

    private IEnumerator LoadImages()
    {
        // Определить высоту строки на основе префаба изображения
        GameObject tempImageObj = Instantiate(_imagePrefab, _imageContainer);
        _rowHeight = tempImageObj.GetComponent<RectTransform>().rect.height;
        Destroy(tempImageObj);

        // Постепенно загружать изображения с сервера, пока не будет ошибка загрузки
        while (true)
        {
            string imageUrl = baseUrl + _currentImageIndex + ".jpg";
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.ProtocolError)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                GameObject imageObj = Instantiate(_imagePrefab, _imageContainer);
                Image imageComponent = imageObj.GetComponent<Image>();
                ImageClickListener imageClickListener = imageObj.GetComponent<ImageClickListener>();
                imageClickListener.SetImageNumber(_currentImageIndex);
                imageComponent.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                imageObj.GetComponent<RectTransform>().anchoredPosition = new Vector2((_currentImageIndex - 1) % 2 * _gridLayout.cellSize.x, -(_currentImageIndex - 1) / 2 * _rowHeight);
                _loadedImages.Add(imageObj);
                _currentImageIndex++;
            }
            else
            {
                Debug.Log("Ошибка загрузки изображения: " + request.error);
                break;
            }
        }

        _totalImages = _currentImageIndex - 1;
    }

    public void OnScrollValueChanged()
    {
        float contentHeight = _gridLayout.cellSize.y * Mathf.Ceil(_totalImages / 2f);
        float viewHeight = _scrollRect.content.rect.height;

        if (_scrollRect.normalizedPosition.y * contentHeight <= (viewHeight - contentHeight))
        {
            if (_currentImageIndex <= _totalImages)
            {
                StartCoroutine(LoadImages());
            }
        }
    }
}