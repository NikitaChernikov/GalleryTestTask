using UnityEngine;
using UnityEngine.UI;

public class ImageInserter : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ImageLoader _imageLoader;

    private void OnEnable()
    {
        _imageLoader.OnInsertImage += ImageLoader_OnInsertImage;
    }

    private void ImageLoader_OnInsertImage(object sender, OnInsertImageEventArgs e)
    {
        _image.sprite = Sprite.Create(e.texture, new Rect(0, 0, e.texture.width, e.texture.height), Vector2.one * 0.5f);
    }
}
