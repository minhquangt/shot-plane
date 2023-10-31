using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetActiveGameobject : MonoBehaviour
{
    public GameObject otherGameObject;
    public GameObject currentGameObject;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetGameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetGameObject()
    {
        currentGameObject.SetActive(false);
        otherGameObject.SetActive(true);
    }
}
