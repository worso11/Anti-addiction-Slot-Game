using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    private TempoController _tempoController;
    private GratificationController _gratificationController;
    private GameObject _message;
    private TextMeshProUGUI _messageHeader;
    private TextMeshProUGUI _messageDescription;
    private TextMeshProUGUI _messageSpace;
    private GameObject _symbolsTable;
    private Scrollbar _scrollbar;

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
        _scrollbar = _message.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<Scrollbar>();
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
        
        _scrollbar.value = 1;
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
                                           "Potraktuj hazard jako formę zabawy i przyjemności. " +
                                           "Załóż możliwość wydania wszystkich pieniędzy wprowadzonych do systemu. " +
                                           "W ten sposób zmniejszasz szanse na poczucie rozczarowania lub straty, które często skutkują nadmierną grą oraz uzależnieniem. " +
                                           "Poprzez ustawianie maksymalnej kwoty przeznaczonej na grę, ograniczasz nie tylko swoje straty, ale również ilość spinów. " +
                                           "W rezultacie, zwiększasz w ten sposób własną kontrolę nad rozgrywką, a także zmniejszasz prawdopodobieństwo straty dużej kwoty pieniędzy.";
                readingTime = 20f;
                break;
            case "TimeLimit":
                _messageHeader.text = "Przekroczono limit czasu";
                _messageDescription.text = $"Twój czas gry: {limit} minut\n\n" +
                                           "Przed rozpoczęciem rozgrywki przemyśl czas, który chcesz nad nią spędzić. " +
                                           "Pamiętaj, że gry hazardowe pozwalają na dowolnie długą rozgrywkę, a w trakcie sesji na maszynie slotowej można bardzo łatwo stracić poczucie czasu. " +
                                           "Z założenia gry te mają dezorientować gracza, między innymi za pomocą nienaturalnego światła oraz głośnych dźwięków. " +
                                           "Zastanów się, czy podczas kolejnej wizyty w kasynie nie ustawić limitu czasowego np. na telefonie komórkowym.";
                readingTime = 20f;
                break;
            case "TimeAlert":
                _messageHeader.text = "Alert czasowy";
                _messageDescription.text = $"Minęło {limit} minut\n\n" +
                                           "Regularne kontrolowanie upływu czasu podczas gry ułatwia zachowanie kontaktu z rzeczywistością. " +
                                           "Podczas wizyt w kasynie rozważ ustawienie alertów czasowych w telefonie, które pozwolą zapobiec nadmiernemu zatraceniu się w rozgrywce.";
                readingTime = 10f;
                break;
            case "money":
                _messageHeader.text = "Za mało kredytów";
                _messageDescription.text = $"Twoje kredyty: {limit}$\n\n" +
                                           "Potraktuj hazard jako formę zabawy i przyjemności. " +
                                           "Załóż możliwość wydania wszystkich pieniędzy wprowadzonych do systemu. " +
                                           "W ten sposób zmniejszasz szanse na poczucie rozczarowania lub straty, które często skutkują nadmierna grą oraz uzależnieniem. " +
                                           "Posiadanie odliczonej kwoty pieniędzy oraz pozostawienie kart płatniczych w domu pozwoli w prosty sposób zapanować nad wydawanymi pieniędzmi. " +
                                           "Przemyśl posiadanie osobnego portfela przeznaczonego tylko i wyłącznie na gry slotowe, ułatwi to rozgraniczyć pieniądze i nie naruszy codziennego budżetu.";
                readingTime = 25f;
                break;
            case "GameDescription":
                _messageHeader.text = "Zasady gry";
                _messageDescription.text = "Witamy w edukacyjnej grze slotowej przeciwdziałającej uzależnieniu. " +
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
                _messageDescription.text = "Losses Disguised as Wins (LDWs) -  zjawisko wystepujące w grach pieniężnych, kiedy to gracz wygrywa kwotę niższą od tej którą postawił w danej partii. " +
                                           "W hazardzie, najczęściej można je zaobserwować w wieloliniowych grach slotowych, gdzie wraz ze wzrostem liczby linii maleje wartość pojedynczej wygranej. " +
                                           "Według wielu badań, LDWs powoduje u graczy fałszywe poczucie wygranej, a tym samym narusza obraz całej sesji. " +
                                           "Sama wiedza na temat istnienia tego zjawiska pozwala uczestnikom gry na bezpieczniejsze i bardziej świadome prowadzenie rozgrywki.";
                readingTime = 20f;
                break;
            case "TooFast":
                _messageHeader.text = "Uwaga";
                _messageDescription.text = "Zauważyliśmy u Ciebie bardzo szybkie tempo gry.\n" +
                                           "Rozważ spowolnienie rozgrywki.";
                break;
            case "TooMuchClicks":
                _messageHeader.text = "Uwaga";
                _messageDescription.text = "Podczas rozgrywki zanotowaliśmy zwiększone tempo wciskania lewego przycisku myszy lub klawisza spacji. " +
                                           "Być może wynika ono z frustracji związanej z rozgrywką.\n" +
                                           "Rozważ chwilę przerwy oraz spokojniejszą grę.";
                break;
            case "TooFastOnLose":
                _messageHeader.text = "Uwaga";
                _messageDescription.text = "W przypadku przegranych przez ciebie rund zauważyliśmy wzrost tempa gry.\n" +
                                           "Rozważ spowolnienie rozgrywki.";
                break;
            case "RewardForSlowerPlay":
                _messageHeader.text = "Gratulacje";
                _messageDescription.text =
                    $"W zamian za spokojną grę otrzymujesz dodatkowe {limit}$ do kredytu oraz limitu zakładu.";
                break;
            case "GettingCredit":
                _messageHeader.text = "Podziękowania";
                _messageDescription.text =
                    "Bardzo mnie cieszy, że w trakcie gry, poświęcasz swój czas na przeczytanie wyświetlanych informacji. " +
                    "W ramach podziękowania otrzymujesz dodatkowe kredyty.\n\n" +
                    $"{limit}$ zostało dodanych do kredytu oraz limitu zakładu.";
                break;
            case "GettingTime":
                _messageHeader.text = "Podziękowania";
                _messageDescription.text =
                    "Bardzo mnie cieszy, że w trakcie gry, poświęcasz swój czas na przeczytanie wyświetlanych informacji.\n\n" +
                    $"W ramach podziękowania zostaje Ci zwrócone {limit} sekund.";
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
