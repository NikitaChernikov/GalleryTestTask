using UnityEngine;

public class AutoOrientation : MonoBehaviour
{
    [SerializeField] private bool _isPortrait = true;

    private void Start()
    {
        if (_isPortrait) Screen.orientation = ScreenOrientation.Portrait;
        else Screen.orientation = ScreenOrientation.AutoRotation;
    }
}