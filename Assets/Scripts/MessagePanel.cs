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
                _messageHeader.text = "Przekroczono limit zak??ad??w";
                _messageDescription.text = $"Pozosta??y limit: {limit}$\n\n" +
                                           "Potraktuj hazard jako form?? zabawy i przyjemno??ci. " +
                                           "Za?????? mo??liwo???? wydania wszystkich pieni??dzy wprowadzonych do systemu. " +
                                           "W ten spos??b zmniejszasz szanse na poczucie rozczarowania lub straty, kt??re cz??sto skutkuj?? nadmiern?? gr?? oraz uzale??nieniem. " +
                                           "Poprzez ustawianie maksymalnej kwoty przeznaczonej na gr??, ograniczasz nie tylko swoje straty, ale r??wnie?? ilo???? spin??w. " +
                                           "W rezultacie, zwi??kszasz w ten spos??b w??asn?? kontrol?? nad rozgrywk??, a tak??e zmniejszasz prawdopodobie??stwo straty du??ej kwoty pieni??dzy.";
                readingTime = 20f;
                break;
            case "TimeLimit":
                _messageHeader.text = "Przekroczono limit czasu";
                _messageDescription.text = $"Tw??j czas gry: {limit} minut\n\n" +
                                           "Przed rozpocz??ciem rozgrywki przemy??l czas, kt??ry chcesz nad ni?? sp??dzi??. " +
                                           "Pami??taj, ??e gry hazardowe pozwalaj?? na dowolnie d??ug?? rozgrywk??, a w trakcie sesji na maszynie slotowej mo??na bardzo ??atwo straci?? poczucie czasu. " +
                                           "Z za??o??enia gry te maj?? dezorientowa?? gracza, mi??dzy innymi za pomoc?? nienaturalnego ??wiat??a oraz g??o??nych d??wi??k??w. " +
                                           "Zastan??w si??, czy podczas kolejnej wizyty w kasynie nie ustawi?? limitu czasowego np. na telefonie kom??rkowym.";
                readingTime = 20f;
                break;
            case "TimeAlert":
                _messageHeader.text = "Alert czasowy";
                _messageDescription.text = $"Min????o {limit} minut\n\n" +
                                           "Regularne kontrolowanie up??ywu czasu podczas gry u??atwia zachowanie kontaktu z rzeczywisto??ci??. " +
                                           "Podczas wizyt w kasynie rozwa?? ustawienie alert??w czasowych w telefonie, kt??re pozwol?? zapobiec nadmiernemu zatraceniu si?? w rozgrywce.";
                readingTime = 10f;
                break;
            case "money":
                _messageHeader.text = "Za ma??o kredyt??w";
                _messageDescription.text = $"Twoje kredyty: {limit}$\n\n" +
                                           "Potraktuj hazard jako form?? zabawy i przyjemno??ci. " +
                                           "Za?????? mo??liwo???? wydania wszystkich pieni??dzy wprowadzonych do systemu. " +
                                           "W ten spos??b zmniejszasz szanse na poczucie rozczarowania lub straty, kt??re cz??sto skutkuj?? nadmierna gr?? oraz uzale??nieniem. " +
                                           "Posiadanie odliczonej kwoty pieni??dzy oraz pozostawienie kart p??atniczych w domu pozwoli w prosty spos??b zapanowa?? nad wydawanymi pieni??dzmi. " +
                                           "Przemy??l posiadanie osobnego portfela przeznaczonego tylko i wy????cznie na gry slotowe, u??atwi to rozgraniczy?? pieni??dze i nie naruszy codziennego bud??etu.";
                readingTime = 25f;
                break;
            case "GameDescription":
                _messageHeader.text = "Zasady gry";
                _messageDescription.text = "Witamy w edukacyjnej grze slotowej przeciwdzia??aj??cej uzale??nieniu. " +
                                           "Celem gry jest klasyczna rozgrywka na maszynie slotowej z elementami Odpowiedzialnego Hazardu. " +
                                           "Ka??d?? kolejn?? rund?? rozpocz???? mo??esz za pomoc?? klawisza spacji lub przycisku SPIN.\n" +
                                           "<b>Powodzenia!</b>\n\n" +
                                           "<size=65>RTP: <color=#B44400>100%</color></size>\n" +
                                           "<size=30><i>RTP (Return To Player) - oczekiwana warto???? zwrotu dla gracza</i></size>\n\n" +
                                           "Wygrane dla poszczeg??lnych symboli:\n" +
                                           "<size=30><i>Podane warto??ci odpowiadaj?? wygranym przy grze na jednej linii</i></size>\n";
                _symbolsTable.SetActive(true);
                _messageSpace.enabled = true;
                waitTime = 0.1f;
                readingTime = 20f;
                break;
            case "LDWsDescription":
                _messageHeader.text = "Losses Disguised as Wins";
                _messageDescription.text = "Losses Disguised as Wins (LDWs) -  zjawisko wystepuj??ce w grach pieni????nych, kiedy to gracz wygrywa kwot?? ni??sz?? od tej kt??r?? postawi?? w danej partii. " +
                                           "W hazardzie, najcz????ciej mo??na je zaobserwowa?? w wieloliniowych grach slotowych, gdzie wraz ze wzrostem liczby linii maleje warto???? pojedynczej wygranej. " +
                                           "Wed??ug wielu bada??, LDWs powoduje u graczy fa??szywe poczucie wygranej, a tym samym narusza obraz ca??ej sesji. " +
                                           "Sama wiedza na temat istnienia tego zjawiska pozwala uczestnikom gry na bezpieczniejsze i bardziej ??wiadome prowadzenie rozgrywki.";
                readingTime = 20f;
                break;
            case "TooFast":
                _messageHeader.text = "Uwaga";
                _messageDescription.text = "Zauwa??yli??my u Ciebie bardzo szybkie tempo gry.\n" +
                                           "Rozwa?? spowolnienie rozgrywki.";
                break;
            case "TooMuchClicks":
                _messageHeader.text = "Uwaga";
                _messageDescription.text = "Podczas rozgrywki zanotowali??my zwi??kszone tempo wciskania lewego przycisku myszy lub klawisza spacji. " +
                                           "By?? mo??e wynika ono z frustracji zwi??zanej z rozgrywk??.\n" +
                                           "Rozwa?? chwil?? przerwy oraz spokojniejsz?? gr??.";
                break;
            case "TooFastOnLose":
                _messageHeader.text = "Uwaga";
                _messageDescription.text = "W przypadku przegranych przez ciebie rund zauwa??yli??my wzrost tempa gry.\n" +
                                           "Rozwa?? spowolnienie rozgrywki.";
                break;
            case "RewardForSlowerPlay":
                _messageHeader.text = "Gratulacje";
                _messageDescription.text =
                    $"W zamian za spokojn?? gr?? otrzymujesz dodatkowe {limit}$ do kredytu oraz limitu zak??adu.";
                break;
            case "GettingCredit":
                _messageHeader.text = "Podzi??kowania";
                _messageDescription.text =
                    "Bardzo mnie cieszy, ??e w trakcie gry, po??wi??casz sw??j czas na przeczytanie wy??wietlanych informacji. " +
                    "W ramach podzi??kowania otrzymujesz dodatkowe kredyty.\n\n" +
                    $"{limit}$ zosta??o dodanych do kredytu oraz limitu zak??adu.";
                break;
            case "GettingTime":
                _messageHeader.text = "Podzi??kowania";
                _messageDescription.text =
                    "Bardzo mnie cieszy, ??e w trakcie gry, po??wi??casz sw??j czas na przeczytanie wy??wietlanych informacji.\n\n" +
                    $"W ramach podzi??kowania zostaje Ci zwr??cone {limit} sekund.";
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
