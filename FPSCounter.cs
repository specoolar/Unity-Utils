using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FPSCounter : MonoBehaviour {
    public int updateInFrames = 30;
    [Space]
    public TMP_Text fpsText;
    [Space]
    public bool showOnUI = true;
    public Rect rect = new Rect(10, 50, 500, 100);
    public int fontSize = 32;
    public Color fontColor = Color.white;
    
    GUIStyle style;
    string label = "";
    int frameCount = 0;
    float fpsSum = 0;

    void Start (){
        GUI.depth = 2;
        GUI.color = fontColor;
        style = new GUIStyle();
        style.fontSize = fontSize;
    }

    private void Update() {
        fpsSum += Time.deltaTime;
        frameCount++;
        if(frameCount >= updateInFrames){
            label = Mathf.RoundToInt(1f / (fpsSum / frameCount)).ToString();
            if(fpsText)
                fpsText.text = label;
            frameCount = 0;
            fpsSum = 0;
        }
    }

    void OnGUI (){
        if(showOnUI){
            GUI.Label (rect, label, style);
        }
    }
}
