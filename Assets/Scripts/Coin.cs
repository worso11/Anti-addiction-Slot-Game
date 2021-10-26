using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject nextCoin;

    private SpriteRenderer _spriteRenderer;
    private string _status;
    private float _speed;
    private float _xPosition;
    private int _symbolStage;
    private int _row;
    private Vector3 _stopPosition;
    private SymbolController _controller;
    private PlayButton _playButton;

    private void Start()
    {
        _controller = GameObject.FindWithTag("Board").GetComponent<SymbolController>();
        _playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        _xPosition = transform.position.x;
        _row = (int) _xPosition / 2 + 2;
        _stopPosition = new Vector3(_xPosition, -4f, 0);
        _speed = 25;
        _status = "Stopped";
        
        _spriteRenderer.sprite = _controller.SetRandomSprite();
    }

    private void PrepareToStart()
    {
        _speed = 25;
        _stopPosition.y = -4f;
        _symbolStage = 0;
        _status = "Moving";
        
        _controller.SetSymbolAsMoving(_row, (int) transform.position.y / 2 + 1);
        InvokeSpinningTime();
    }

    private void InvokeSpinningTime()
    {
        Invoke("InvokeExtraSpinningTime", 1);
    }
    
    private void InvokeExtraSpinningTime()
    {
        Invoke("InvokeSlowingStart", _controller.GetSpinTime());
    }
    
    public void InvokeSlowingStart()
    {
        Invoke("StartSlowing", _controller.GetExtraSpinTime(_row));
    }
    

    private void Update()
    {
        switch (_status)
        {
            case "Moving":
                MoveCoin();

                if (Input.GetKeyDown(KeyCode.Space) || _playButton.GetPressedStatus() == "StopPressed")
                {
                    InvokeSlowingStart();
                }

                break;

            case "Slowing":
                MoveCoin();
                SlowCoin();
                break;

            case "Stopping":
                StopCoin();
                break;

            case "Stopped":
                if (_controller.GetGameStatus())
                {
                    PrepareToStart();
                }

                break;
        }

        if (transform.position.y <= -4f)
        {
            TeleportCoinToTop();
        }
    }

    private void MoveCoin()
    {
        if (nextCoin.transform.position.y > transform.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_xPosition, -4f, 0),
                _speed * Time.deltaTime);
        }
        else
        {
            transform.position = nextCoin.transform.position + new Vector3(0, 2, 0);
        }
    }

    private void StartSlowing()
    {
        if (_status == "Moving")
        {
            _status = "Slowing";
            CancelInvoke("StartSlowing");
        }
    }
    
    private void SlowCoin()
    {
        _speed -= 0.05f;

        if (_speed <= 10f)
        {
            _status = "Stopping";
        }
    }

    private void StopCoin()
    {
        transform.position = Vector3.MoveTowards(transform.position, _stopPosition, _speed * Time.deltaTime);

        if (_speed > 1f)
        {
            _speed /= 1.004f;
        }


        if (transform.position == _stopPosition)
        {
            switch (_symbolStage)
            {
                case 1:
                    _stopPosition.y = Mathf.Ceil(_stopPosition.y);
                    _symbolStage = 2;
                    break;

                case 2:
                    _status = "Stopped";
                    _controller.SetSymbolAsStopped(_row, (int) _stopPosition.y / 2 + 1);
                    break;
            }
        }
    }

    private void TeleportCoinToTop()
    {
        transform.position = nextCoin.transform.position + new Vector3(0, 2, 0);

        switch (_status)
        {
            case "Stopping":
                _spriteRenderer.sprite = _controller.SetSprite(_row);
                _stopPosition.y = _controller.SetYPosition(_row);
                _symbolStage = 1;
                break;

            default:
                _spriteRenderer.sprite = _controller.SetRandomSprite();
                break;
        }
    }
}
