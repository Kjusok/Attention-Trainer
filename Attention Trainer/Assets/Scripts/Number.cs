using UnityEngine;
using UnityEngine.UI;
using System;

public class Number : MonoBehaviour
{
    [SerializeField] private Vector2 _position;

    private Action<int> _clickHandler;
    private int _number;

    public Text TextNumber;
    public void OnMouseDownOnNumber()
    {
        _clickHandler(_number);
        Destroy(gameObject);
    }
    public void Initialize(int number, Action<int> clickHandler)
    {
        _number = number;
        TextNumber.text = number.ToString();
        _clickHandler = clickHandler;
    }
}
