using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    [SerializeField] private Vector2 _position;

    public Text TextNumber;
    private int _currentNameOFNumber;

    private void OnMouseDown()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gameManager.gameObject.GetComponent<GameManager>().CheckCorectSubsequence(_currentNameOFNumber);
        Destroy(gameObject);
    }
    private void Start()
    {
        _currentNameOFNumber = int.Parse(TextNumber.text);
    }
}
