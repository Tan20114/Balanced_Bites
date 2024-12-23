using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveData : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Delete);
    }

    void Delete()
    {
        VariableAcrossScene.DeleteData();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
