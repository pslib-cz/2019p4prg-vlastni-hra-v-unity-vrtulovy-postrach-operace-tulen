using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public Canvas _Menu;
    public Button _NG;

    // Start is called before the first frame update
    void Start()
    {
        _Menu = GetComponent<Canvas>();
        _NG = _Menu.GetComponentInChildren<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        _NG.onClick.AddListener(ZapniHru);
    }

    void ZapniHru()
    {
        SceneManager.LoadScene(1);      
    }
}
