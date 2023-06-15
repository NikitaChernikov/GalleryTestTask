using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public event EventHandler<OnProgressBarChangedEventArgs> OnStartLoading;
    public event EventHandler OnFinishLoading;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenScene(int index)
    {
        StartCoroutine(StartLoading(index));
    }

    private IEnumerator StartLoading(int index)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(index);
        loadScene.allowSceneActivation = false;
        while (!loadScene.isDone)
        {
            OnStartLoading?.Invoke(this, new OnProgressBarChangedEventArgs { Percent = loadScene.progress});
            if (loadScene.progress >= .9f)
            {
                OnFinishLoading?.Invoke(this, EventArgs.Empty);
                if (Input.anyKeyDown) loadScene.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

public class OnProgressBarChangedEventArgs : EventArgs
{
    public float Percent;
}
