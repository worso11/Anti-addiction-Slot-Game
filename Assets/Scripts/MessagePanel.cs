using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagePanel : MonoBehaviour
{
    private TempoController _tempoController;
    private GratificationController _gratificationController;
    private GameObject _message;
    private TextMeshProUGUI _messageHeader;
    private TextMeshProUGUI _messageDescription;
    private TextMeshProUGUI _messageSpace;
    private GameObject _symbolsTable;

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

    private IEnumerator DeactiveMessage(string limitName, float waitTime, float readingTime)
    {
        var endTime = DateTime.Now.AddSeconds(readingTime);
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

        if (readingTime != 0f && endTime < DateTime.Now)
        {
            if (limitName == "WageringLimit" || limitName == "money")
            {
                _gratificationController.GratificatePlayerWithCredits(limitName, 0.1f);
            }
            else
            {
                _gratificationController.GratificatePlayerWithTime(limitName, readingTime+10);
            }
            
        }
    }
    
    public void SetMessage(string limitName, float limit = 0f)
    {
        _symbolsTable.SetActive(false);
        Time.timeScale = 0;

        var waitTime = 2f;
        var readingTime = 0f;
        
        switch (limitName)
        {
            case "WageringLimit":
                _messageHeader.text = "Przekroczono limit zakładów";
                _messageDescription.text = $"Pozostały limit: {limit}$\n\n" +
                                           "Zawsze traktuj hazard jako formę zabawy i przyjemności, z założeniem wydania wszystkich pieniędzy jakie wprowadzisz do systemu. " +
                                           "W ten sposób zmniejszasz szanse na poczucie rozczarowania lub straty, które często skutkują nadmierną grą.\n" +
                                           "Poprzez ustawianie maksymalnej kwoty jaką chcesz przeznaczyć na grę, ograniczasz nie tylko swoje straty, ale również ilość spinów. " +
                                           "W rezultacie, zwiększasz własną kontrolę nad rozgrywką, a także zmniejszasz prawdopodobieństwo straty dużej kwoty pieniędzy.";
                readingTime = 20f;
                break;
            case "TimeLimit":
                _messageHeader.text = "Przekroczono limit czasu";
                _messageDescription.text = $"Twój czas gry: {limit} minut\n\n" +
                                           "Gry hazardowe pozwalają na dowolnie długą rozgrywkę, warto jednak zaplanować jej czas jeszcze przed rozpoczęciem. " +
                                           "W trakcie sesji na maszynie slotowej można bardzo łatwo stracić poczucie czasu. " +
                                           "Powodem tego są same gry, które stworzono tak, aby dezorientowały gracza, między innymi za pomocą nienaturalnego światła oraz głośnych dźwięków. " +
                                           "Podczas pobytu w kasynie warto ustawić sobie alarm w telefonie, na godzinę o której chcemy zakończyć zabawę.";
                readingTime = 15f;
                break;
            case "TimeAlert":
                _messageHeader.text = "Alert czasowy";
                _messageDescription.text = $"Minęło {limit} minut!\n\n" +
                                           "Regularne kontrolowanie upływu czasu podczas gry ułatwia zachowanie kontaktu z rzeczywistością. " +
                                           "Podczas wizyt w kasynie rozważ ustawienie alertów czasowych w telefonie, które zapobiegną nadmiernemu zatraceniu się w rozgrywce.";
                readingTime = 10f;
                break;
            case "money":
                _messageHeader.text = "Za mało kredytów";
                _messageDescription.text = $"Twoje kredyty: {limit}$\n\n" +
                                           "Zawsze traktuj hazard jako formę zabawy i przyjemności, z założeniem wydania wszystkich pieniędzy jakie wprowadzisz do systemu. " +
                                           "W ten sposób zmniejszasz szanse na poczucie rozczarowania lub straty, które często skutkują nadmierną grą.\n" +
                                           "Poprzez pozostawienie kart płatniczych w domu oraz zabranie ze sobą jedynie konkretnej kwoty gotówki do kasyna, zapobiegasz możliwości wydania więcej niż planowałeś przed grą. " +
                                           "Rozważ również posiadanie osobnego portfela do uprawiania hazardu, w celu separacji wydatków na gry slotowe od codziennego budżetu.";
                readingTime = 20f;
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
                readingTime = 20f;
                break;
            case "LDWsDescription":
                _messageHeader.text = "Losses Disguised as Wins";
                _messageDescription.text = "Losses Disguised as Wins (LDWs) -  zjawisko wystepujące w grach pieniężnych kiedy to gracz wygrywa kwotę niższą od tej którą postawił w danej partii. " +
                                           "W hazardzie najczęściej można je zaobserwować w wieloliniowych grach slotowych, gdzie wraz ze wzrostem liczby linii maleje wartość pojedynczej wygranej. " +
                                           "Według wielu badań, LDWs powoduje u graczy fałszywe poczucie wygranej, a tym samym narusza obraz całej sesji. " +
                                           "Sama świadomość istnienia tego zjawiska pozwala uczestnikom gry na bezpieczniejsze i bardziej świadome prowadzenie rozgrywki.";
                readingTime = 20f;
                break;
            case "TooFast":
                _messageHeader.text = "Zwolnij :)";
                _messageDescription.text = "Zauważyliśmy u Ciebie bardzo szybkie tempo gry.\n" +
                                           "Rozważ spowolnienie rozgrywki.";
                break;
            case "TooMuchClicks":
                _messageHeader.text = "Odetchnij :)";
                _messageDescription.text = "Zauważyliśmy u Ciebie zwiększone tempo wciskania lewego przycisku myszy lub klawisza spacji, które wynikać może z frustracji rogrywką.\n" +
                                           "Rozważ chwilę przerwy oraz spokojniejszą grę.";
                break;
            case "TooFastOnLose":
                _messageHeader.text = "Zwolnij :)";
                _messageDescription.text = "Zauważyliśmy u Ciebie szybsze tempo gry w przypadku przegranych rund.\n" +
                                           "Rozważ spowolnienie rozgrywki.";
                break;
            case "RewardForSlowerPlay":
                _messageHeader.text = "Dobra robota!";
                _messageDescription.text =
                    $"W zamian za spokojniejszą rozgrywkę otrzymujesz dodatkowe {limit}$ do kredytu oraz limitu zakładu.";
                break;
            case "GettingCredit":
                _messageHeader.text = "Dobra robota!";
                _messageDescription.text =
                    "Bardzo nas cieszy, że poświęcasz swój czas gry na czytanie naszych informacji. W ramach podziękowań otrzymujesz dodatkowe kredyty!\n\n" +
                    $"{limit}$ zostało dodanych do kredytu oraz limitu zakładu.";
                break;
            case "GettingTime":
                _messageHeader.text = "Tak trzymaj!";
                _messageDescription.text =
                    "Bardzo nas cieszy, że poświęcasz swój czas gry na czytanie naszych informacji. W ramach podziękowań otrzymujesz czas z powrotem!\n\n" +
                    $"{limit} sekund zostało dodanych do limitu czasowego.";
                break;
        }
        
        _message.SetActive(true);
        StartCoroutine(DeactiveMessage(limitName, waitTime, readingTime));
    }

    public void SetControllers()
    {
        _tempoController = GameObject.FindGameObjectWithTag("TempoController").GetComponent<TempoController>();
        _gratificationController = GameObject.FindGameObjectWithTag("GratificationController")
            .GetComponent<GratificationController>();
    }
}
