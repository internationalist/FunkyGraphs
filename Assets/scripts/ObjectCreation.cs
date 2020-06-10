using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VectorDraw.Functional;

public class ObjectCreation : MonoBehaviour
{
    private Button _button;
    public GameManager.ObjectType type;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SelectTypeToBeCreated);
    }

    // Update is called once per frame
    void SelectTypeToBeCreated()
    {
        GameManager.S.currentObjectToBeCreated = type;
        GameManager.CreateFigure(type);
    }
}
