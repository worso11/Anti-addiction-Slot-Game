using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagePanel : MonoBehaviour
{
    private GameObject _message;
    private TextMeshProUGUI _messageHeader;
    private TextMeshProUGUI _messageDescription;
    private TextMeshProUGUI _messageSpace;
    private GameObject _symbolsTable;
    private TempoController _tempoController;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _message = transform.GetChild(0).gameObject;
        _messageHeader = _message.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        _messageDescription = _message.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        _messageSpace = _message.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        _symbolsTable = _message.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(2)
            .gameObject;
    }

    private IEnumerator DeactiveMessage(float waitTime)
    {
        _tempoController.SetIsPlaying(false);
        
        yield return new WaitForSecondsRealtime(waitTime);

        _messageSpace.enabled = true;
        
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        
        _messageSpace.enabled = false;
        
        _tempoController.SetIsPlaying(true);
        _message.SetActive(false);
        
        Time.timeScale = 1;
    }
    
    public void SetMessage(string limitName, float limit = 0f)
    {
        _symbolsTable.SetActive(false);
        Time.timeScale = 0;
        var waitTime = 2f;
        
        switch (limitName)
        {
            case "WageringLimit":
                _messageHeader.text = "Przekroczono limit zakładów";
                _messageDescription.text = $"Pozostały limit: {limit}$\n\n" +
                                           "Wyjaśnienie czemu nie warto przekraczać";
                break;
            case "TimeLimit":
                _messageHeader.text = "Przekroczono limit czasu";
                _messageDescription.text = $"Twój czas gry: {limit} minut\n\n" +
                                           "Wyjaśnienie czemu nie warto przekraczać";
                break;
            case "TimeAlert":
                _messageHeader.text = "Alert czasowy";
                _messageDescription.text = $"Minęło {limit} minut!\n\n" +
                                           "Wyjaśnienie czemu nie warto przekraczać";
                break;
            case "money":
                _messageHeader.text = "Za mało kredytów";
                _messageDescription.text = $"Twoje kredyty: {limit}$\n\n" +
                                           "Wyjaśnienie czemu nie warto przekraczać";
                break;
            case "GameDescription":
                _messageHeader.text = "Zasady gry";
                _messageDescription.text = "Witamy w edukacyjnej grze slotowej przeciwdziałającej uzależnieniu! " +
                                           "Celem gry jest klasyczna rozgrywka na maszynie slotowej z elementami Odpowiedzialnego Hazardu. " +
                                           "Każdą kolejną rundę rozpocząć możesz za pomocą klawisza spacji lub przycisku SPIN.\n" +
                                           "<b>Powodzenia!</b>\n\n" +
                                           "<size=65>RTP: <color=#B44400>100%</color></size>\n" +
                                           "<size=30><i>RTP (Return To Player) - oczekiwana wartość zwrotu dla gracza</i></size>\n\n" +
                                           "Wygrane dla poszczególnych symboli:\n" +
                                           "<size=30><i>Podane wartości odpowiadają wygranym przy grze na jednej linii</i></size>\n";
                _symbolsTable.SetActive(true);
                _messageSpace.enabled = true;
                waitTime = 0.1f;
                break;
            case "LDWsDescription":
                _messageHeader.text = "Losses Disguised as Wins";
                _messageDescription.text = "Wyjaśnienie na temat LDWs";
                break;
            case "TooFast":
                _messageHeader.text = "Zwolnij :)";
                _messageDescription.text = "Zauważyliśmy u Ciebie bardzo szybkie tempo gry.\n" +
                                           "Rozważ spowolnienie rozgrywki";
                break;
            case "TooMuchClicks":
                _messageHeader.text = "Odetchnij :)";
                _messageDescription.text = "Zauważyliśmy u Ciebie zwiększone tempo wciskania lewego przycisku myszy lub klawisza spacji, które wynikać może z frustracji rogrywką.\n" +
                                           "Rozważ chwilę przerwy oraz spokojniejszą grę";
                break;
        }
        
        _message.SetActive(true);
        StartCoroutine(nameof(DeactiveMessage), waitTime);
    }

    public void SetTempoController()
    {
        _tempoController = GameObject.FindGameObjectWithTag("TempoController").GetComponent<TempoController>();
    }
}
