using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playBtn;

    private void Awake()
    {
        if (_playBtn != null)
        {
            _playBtn.onClick.AddListener(OnClickPlayBtn);
        }
    }

    private void OnClickPlayBtn()
    {
        SceneManager.LoadScene(1);
    }
}
