using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Menu))]
public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _loadingUI;
    [SerializeField] private Text _textField;
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private Text _percentText;
    
    private Menu _menu;

    private void Awake()
    {
        _menu = GetComponent<Menu>();
    }

    private void OnEnable()
    {
        _menu.OnStartLoading += Menu_OnStartLoading;
        _menu.OnFinishLoading += Menu_OnFinishLoading;
    }

    private void Menu_OnFinishLoading(object sender, System.EventArgs e)
    {
        _textField.text = "TAP TO CONTINUE";
        _loadingBar.value = 1f;
        _percentText.text = "100%";
    }

    private void OnDisable()
    {
        _menu.OnStartLoading -= Menu_OnStartLoading;
    }

    private void Menu_OnStartLoading(object sender, OnProgressBarChangedEventArgs e)
    {
        _loadingUI.SetActive(true);
        _loadingBar.value = e.Percent;
        _percentText.text = (e.Percent * 100) + "%";
    }
}
