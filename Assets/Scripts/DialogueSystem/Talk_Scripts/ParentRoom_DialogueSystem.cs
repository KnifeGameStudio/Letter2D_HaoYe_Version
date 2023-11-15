using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ParentRoom_DialogueSystem : MonoBehaviour
{
    [Header("UI组件")] 
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")] 
    public float textSpeed;
    public TextAsset textFile;
    public int index;
    public bool textFinish;
    public bool cancelTyping;
    private List<string> textList = new List<string>();

    [Header("头像")] 
    public Sprite face01, face02;

    [Header("对话触发物")] 
    public GameObject dialogueTrigger;
    
    void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable()
    {
        textFinish = false;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if (index == textList.Count)
        {
            SceneManager.LoadScene("TempEnding");
            Destroy(gameObject);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (textFinish && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinish)
            {
                cancelTyping = !cancelTyping;
            }
        }
    }
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;
        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinish = false;
        textLabel.text = "";
        
        if (textList[index] == "A\r")
        {
            faceImage.sprite = face01;
            index++;
        }
        
        else if (textList[index] == "B\r")
        {
            faceImage.sprite = face02;
            index++;
        }
        
        int letter = 0;
        while (!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinish = true;
        index += 1;
    }
}
