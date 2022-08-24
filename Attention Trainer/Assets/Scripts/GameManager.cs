using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//public delegate void NumberClick(int number);
public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider _amountSlider;
    [SerializeField] private Slider _defficultySlider;
    [SerializeField] private Text _maxOfAmount;
    [SerializeField] private Text _maxOfDefficulty;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _yourBestTime;
    [SerializeField] private Image _timeScale;
    [SerializeField] private RectTransform _gameBoard;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private GameObject _saccessPanel;
    [SerializeField] private Button _startButton;

    [SerializeField] private Number _number;

    private Vector2 _minValue = new Vector2(x: -0.45f, y: -0.4f);
    private Vector2 _maxValue = new Vector2(x: 0.45f, y: 0.4f);
    private float _timer;
    private float _currentValueForTimer;
    private float _currentValueForAmount;
    private bool _stopTimer;
    private int _counter;

    private void Start()
    {
        _stopTimer = true;
    }
    public void PressStartButton()
    {
        _stopTimer = false;
        _timerText.text = _maxOfDefficulty.text;
        _timer = int.Parse(_timerText.text);
        _currentValueForTimer = _defficultySlider.value;
        _currentValueForAmount = _amountSlider.value;
        _startButton.interactable = false;
        SpawnAmount();
    }
    private void SpawnAmount()
    {
        for (int i = 0; i <= _currentValueForAmount; i++)
        {
            var number = Instantiate(_number, new Vector2(
                Random.Range(_minValue.x * _gameBoard.sizeDelta.x, _maxValue.x * _gameBoard.sizeDelta.x),
                Random.Range(_minValue.y * _gameBoard.sizeDelta.y, _maxValue.y * _gameBoard.sizeDelta.y)),
                Quaternion.identity);
            number.transform.SetParent(_gameBoard.transform, false);

            number.Initialize(i, CheckNumber);
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
        _maxOfAmount.text = _amountSlider.value.ToString();
        _maxOfDefficulty.text = _defficultySlider.value.ToString();
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
    private void CheckNumber(int number)
    {
        if (_counter != number)
        {
            _stopTimer = true;
            _failPanel.SetActive(true);
            DestroyAllNumbers();
        }

        _counter++;

        if(_counter == (int) _amountSlider.value+1)
        {
            _stopTimer = true;
            _saccessPanel.SetActive(true);
            _yourBestTime.text = (_currentValueForTimer - _timer).ToString("0.00") + "s";
        }
    }
}
