using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetActiveButton : MonoBehaviour
{
    public GameObject[] contents;
    public GameObject contentAppear;
    private Button btn;
    public GameObject[] buttons;
    public GameObject buttonAppear;
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(SetActive);
        btn.onClick.AddListener(ChangeColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = new Color(0f, 255f, 0f, 210f);
        }

        buttonAppear.GetComponent<Image>().color = color;
    }

    public void SetActive()
    {
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].SetActive(false);
        }

        contentAppear.SetActive(true);
    }
}
