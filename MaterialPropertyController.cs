using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPropertyController : MonoBehaviour
{
    public enum PropertyType{
        Float, Integer, Vector, Color, Texture
    }
    [System.Serializable]
    public struct DefProperty{
        public string name;
        public PropertyType propertyType;
        public float fValue;
        public int iValue;
        public Vector4 vValue;
        public Color cValue;
        public Texture tValue;
    }
    public Renderer _renderer;
    public DefProperty[] defaultProperties;

    MaterialPropertyBlock mpb;

    private void Reset() {
        _renderer = GetComponent<Renderer>();
        defaultProperties = new DefProperty[1];
        defaultProperties[0].propertyType = PropertyType.Color;
        defaultProperties[0].name = "_BaseColor";
        defaultProperties[0].cValue = Color.white;
        UpdateProps();
    }

    private void OnValidate() {
        UpdateProps();
    }

    void Start(){
        UpdateProps();
    }

    void UpdateProps(){
        mpb = new MaterialPropertyBlock();

        foreach (var item in defaultProperties)
        {
            switch(item.propertyType){
                case PropertyType.Float:
                    mpb.SetFloat(item.name, item.fValue);
                    break;
                case PropertyType.Integer:
                    mpb.SetInt(item.name, item.iValue);
                    break;
                case PropertyType.Vector:
                    mpb.SetVector(item.name, item.vValue);
                    break;
                case PropertyType.Color:
                    mpb.SetColor(item.name, item.cValue);
                    break;
                case PropertyType.Texture:
                    mpb.SetTexture(item.name, item.tValue);
                    break;
            }
        }
        _renderer.SetPropertyBlock(mpb);
    }
}
