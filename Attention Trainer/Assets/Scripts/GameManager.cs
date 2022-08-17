using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _amount;
    [SerializeField] private Slider _defficulty;
    [SerializeField] private Text _maxOfAmount;
    [SerializeField] private Text _maxOfDefficulty;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _yourBestTime;
    [SerializeField] private Image _timeScale;
    [SerializeField] private RectTransform _gameBoard;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _saccessPanel;
    [SerializeField] private Button _startButton;

    [SerializeField] private GameObject _number;

    private Vector2 _minValue = new Vector2(x: -0.45f, y: -0.4f);
    private Vector2 _maxValue = new Vector2(x: 0.45f, y: 0.4f);
    private float _timer;
    private float _currentValueForTimer;
    private float _currentValueForAmount;
    private bool _stopTimer;
    private int[] _arrayOfTakenNumbers;

    private void Start()
    {
        _stopTimer = true;
    }
    public void PressStartButton()
    {
        _stopTimer = false;
        _timerText.text = _maxOfDefficulty.text;
        _timer = int.Parse(_timerText.text);
        _currentValueForTimer = _defficulty.value;
        _currentValueForAmount = _amount.value;
        _startButton.interactable = false;
        SpawnAmount();
    }
    private void SpawnAmount()
    {
        _arrayOfTakenNumbers = new int[Mathf.RoundToInt(_currentValueForAmount+2)];
        for (int i = 0; i <= _currentValueForAmount; i++)
        {
            var number = Instantiate(_number, new Vector2(Random.Range(_minValue.x * _gameBoard.sizeDelta.x, _maxValue.x * _gameBoard.sizeDelta.x),
                Random.Range(_minValue.y * _gameBoard.sizeDelta.y, _maxValue.y * _gameBoard.sizeDelta.y)), Quaternion.identity);
            number.transform.SetParent(_gameBoard.transform, false);
            number.GetComponent<Number>().TextNumber.text = i.ToString();
            _arrayOfTakenNumbers[i] = i;
        }
    }
    public void CheckCorectSubsequence(int _currentNameOFNumber)
    {
        if (_arrayOfTakenNumbers[0] == _currentNameOFNumber)
        {
            int[] newArray = new int[_arrayOfTakenNumbers.Length - 1];
            for (int i = 0; i < 0; i++)
            {
                newArray[i] = _arrayOfTakenNumbers[i];
            }
            for (int i = 1; i < _arrayOfTakenNumbers.Length; i++)
            {
                newArray[i - 1] = _arrayOfTakenNumbers[i];
            }
            _arrayOfTakenNumbers = newArray;
            Debug.Log(_arrayOfTakenNumbers[0]);
        }
        else
        {
            _stopTimer = true;
            _failPanel.SetActive(true);
            DestroyAllNumbers();
        }
        if (_arrayOfTakenNumbers.Length == 1)
        {
            _stopTimer = true;
            _saccessPanel.SetActive(true);
            _yourBestTime.text = (_currentValueForTimer - _timer).ToString("0.00") + "s";
        }
    }
    public void PressButtonNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    private void DestroyAllNumbers()
    {
        GameObject [] numbers = GameObject.FindGameObjectsWithTag("Number");
        for ( int i = 0; i < numbers.Length; i++)
        {
            Destroy(numbers[i]);
        }
    }
    private void Update()
    {
        _maxOfAmount.text = _amount.value.ToString();
        _maxOfDefficulty.text = _defficulty.value.ToString();
        if (!_stopTimer)
        {
            _timer -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(_timer / 60);
            float seconds = Mathf.FloorToInt(_timer - minutes * 60);
            string textTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            _timerText.text = textTime;

            _timeScale.fillAmount = _timer / _currentValueForTimer;
        }
        if (_timer < 0)
        {
            _timerText.text = "0:00";
            _stopTimer = true;
            _failPanel.SetActive(true);
            DestroyAllNumbers();
        }
    }
}
