using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    public GameObject contents;

    Button button;
    bool isContentsActive;

    void Awake() {
        button = GetComponent<Button>();
        isContentsActive = false;
        // slider = GetComponent<Slider>();
    }

    void Start() {
        button.onClick.AddListener(OnClickButton);
        contents.SetActive(false);
    }

    void OnClickButton() {
        isContentsActive = !isContentsActive;
        contents.SetActive(isContentsActive);
    }
}
