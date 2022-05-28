using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class CustomSlider : Slider
{
    [SerializeField] public int MyInt;
    [SerializeField] public string MyString;
    [SerializeField] public Sprite MySprite;
    [SerializeField] public Color MyColor;
    [SerializeField] public List<int> MyList;

    Action<PointerEventData> _onPointerDownEvent;
    Action<PointerEventData> _onPointerUpEvent;
    bool _isPointerDown;
    public bool IsPointerDown => _isPointerDown;

    public void SetOnPointerDownEvent(Action<PointerEventData> callback)
    {
        _onPointerDownEvent = callback;
    }

    public void SetOnPointerUpEvent(Action<PointerEventData> callback)
    {
        _onPointerUpEvent = callback;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPinterDown");
        base.OnPointerDown(eventData);

        _isPointerDown = true;
        _onPointerDownEvent?.Invoke(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        base.OnPointerUp(eventData);

        _isPointerDown = false;
        _onPointerUpEvent?.Invoke(eventData);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CustomSlider))]
public class CustomSliderEditor : UnityEditor.UI.SliderEditor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var component = (CustomSlider)target;

        //    component.MyInt = EditorGUILayout.IntField("My Int", component.MyInt);
        //    component.MyString = EditorGUILayout.TextField("My String", component.MyString);
        //    component.MySprite = (Sprite) EditorGUILayout.ObjectField("My Sprite", component.MySprite, typeof(Sprite), true);
        //    component.MyColor = EditorGUILayout.ColorField("My Color", component.MyColor);
        //    component.MyWeek = (WEEK) EditorGUILayout.EnumPopup("My Week", component.MyWeek);

        //    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(component.MyClass)), new GUIContent("My Class"));
        //    EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(component.MyList)), new GUIContent("My List"));

        serializedObject.ApplyModifiedProperties();

    }
}
#endif