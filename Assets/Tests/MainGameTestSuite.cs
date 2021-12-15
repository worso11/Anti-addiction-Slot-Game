using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class MainGameTestSuite
{
    private LimitController _limitController;
    private GameObject _messagePanel;

    [UnitySetUp]
    public IEnumerator UnitySetUp()
    {
        SceneManager.LoadScene("LimitSelector");

        yield return new WaitForSeconds(1f);
        
        _limitController = GameObject.FindGameObjectWithTag("LimitController").GetComponent<LimitController>();
        _messagePanel = GameObject.FindGameObjectWithTag("MessagePanel");
        
        _limitController.MoneyDeposit = 100;
        _limitController.WageringLimit = 100;
        _limitController.TimeLimit = 15;
        _limitController.TimeAlert = 5;

        _limitController.OnButtonClick();
    }

    [UnityTearDown]
    public IEnumerator UnityTearDown()
    {
        Object.Destroy(_limitController.gameObject);
        Object.Destroy(_messagePanel);
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckWinningLineNumberColorChange()
    {
        var winningLine = GameObject.Find("WinningLine");
        var rightNumber = winningLine.transform.GetChild(0).gameObject;

        var rightNumberColorBefore = rightNumber.GetComponent<SpriteRenderer>().color;
        
        rightNumber.GetComponent<Number>().OnMouseOver();
        
        var rightNumberColorAfter = rightNumber.GetComponent<SpriteRenderer>().color;
        
        Assert.AreNotEqual(rightNumberColorBefore, rightNumberColorAfter, "Colors are the same");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckWinningLineNumberColorChangeBack()
    {
        var winningLine = GameObject.Find("WinningLine");
        var rightNumber = winningLine.transform.GetChild(0).gameObject;

        var rightNumberColorBefore = rightNumber.GetComponent<SpriteRenderer>().color;
        
        rightNumber.GetComponent<Number>().OnMouseOver();

        rightNumber.GetComponent<Number>().OnMouseExit();
        
        var rightNumberColorAfter = rightNumber.GetComponent<SpriteRenderer>().color;
        
        Assert.AreEqual(rightNumberColorBefore, rightNumberColorAfter, "Colors are not the same");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckWinningLineLineActivation()
    {
        var winningLine = GameObject.Find("WinningLine");
        var rightNumber = winningLine.transform.GetChild(0).gameObject;
        var line = winningLine.transform.GetChild(2).gameObject;

        rightNumber.GetComponent<Number>().OnMouseOver();

        Assert.IsTrue(line.activeSelf, "Line is not active");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckWinningLineLineDeactivation()
    {
        var winningLine = GameObject.Find("WinningLine");
        var rightNumber = winningLine.transform.GetChild(0).gameObject;
        var line = winningLine.transform.GetChild(2).gameObject;

        rightNumber.GetComponent<Number>().OnMouseOver();

        rightNumber.GetComponent<Number>().OnMouseExit();

        Assert.IsFalse(line.activeSelf, "Line is active");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckGameDescriptionMessagePanel()
    {
        var descriptionButton = GameObject.Find("GameDescription").GetComponent<DescriptionButton>();
        var message = _messagePanel.transform.GetChild(0).gameObject;

        descriptionButton.OnMouseDown();
        
        Assert.IsTrue(message.activeSelf, "Message panel is not active");

        Time.timeScale = 1;

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckPlayButtonGameStarting()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
        var symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
        
        playButton.OnMouseDown();
        
        Assert.IsTrue(symbolController.GetGameStatus() && symbolController.GetSymbolsMoving(), "Game is not starting");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckPlayButtonGameStarted()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
        var symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
        
        playButton.OnMouseDown();

        yield return new WaitForSeconds(0.1f);
        
        Assert.IsFalse(symbolController.GetGameStatus(), "Game is not started");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckPlayButtonIsPressed()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();

        playButton.OnMouseDown();

        Assert.AreEqual("Pressed", playButton.GetPressedStatus(), "Button is not pressed");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckPlayButtonIsPressedForStopping()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();

        playButton.OnMouseDown();
        playButton.OnMouseDown();

        Assert.AreEqual("StopPressed", playButton.GetPressedStatus(), "Button is not pressed for stopping");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckAutoPlayButtonGameStarting()
    {
        var autoPlayButton = GameObject.FindGameObjectWithTag("AutoPlayButton").GetComponent<AutoPlayButton>();
        var symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();

        autoPlayButton.OnMouseDown();

        yield return null;
        
        Assert.IsTrue(symbolController.GetGameStatus() && symbolController.GetSymbolsMoving(), "Autoplay game is not starting");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckAutoPlayButtonGameStarted()
    {
        var autoPlayButton = GameObject.FindGameObjectWithTag("AutoPlayButton").GetComponent<AutoPlayButton>();
        var symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();

        autoPlayButton.OnMouseDown();

        yield return new WaitForSeconds(0.1f);
        
        Assert.IsFalse(symbolController.GetGameStatus(), "Auto game is not started");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckAutoPlayButtonIsPressed()
    {
        var autoPlayButton = GameObject.FindGameObjectWithTag("AutoPlayButton").GetComponent<AutoPlayButton>();

        autoPlayButton.OnMouseDown();

        Assert.AreEqual("Pressed", autoPlayButton.GetPressedStatus(), "Button is not pressed");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckAutoPlayButtonIsUnpressed()
    {
        var autoPlayButton = GameObject.FindGameObjectWithTag("AutoPlayButton").GetComponent<AutoPlayButton>();

        autoPlayButton.OnMouseDown();
        autoPlayButton.OnMouseDown();

        Assert.AreEqual("Unpressed", autoPlayButton.GetPressedStatus(), "Button is still pressed");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckLinesButtonChangeValue()
    {
        var linesButton = GameObject.Find("LinesButton").transform.GetChild(3).GetComponent<ChangeValueButton>();
        var linesValue = GameObject.FindGameObjectWithTag("LinesValue").GetComponent<TextMeshProUGUI>();
        var linesValueBefore = linesValue.text;
        
        linesButton.OnMouseDown();

        var linesValueAfter = linesValue.text;

        Assert.AreNotEqual(linesValueBefore, linesValueAfter, "Lines value didn't change");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckBetButtonChangeValue()
    {
        var betButton = GameObject.Find("BetButton").transform.GetChild(2).GetComponent<ChangeValueButton>();
        var betValue = GameObject.FindGameObjectWithTag("BetValue").GetComponent<TextMeshProUGUI>();
        var betValueBefore = betValue.text;
        
        betButton.OnMouseDown();

        var betValueAfter = betValue.text;

        Assert.AreNotEqual(betValueBefore, betValueAfter, "Bet value didn't change");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckMoneyPanelChangeValue()
    {
        var moneyPanel = GameObject.FindGameObjectWithTag("MoneyPanel").GetComponent<MoneyPanel>();
        var moneyPanelText = GameObject.FindGameObjectWithTag("MoneyPanel").GetComponent<TextMeshProUGUI>();
        var moneyPanelTextBefore = moneyPanelText.text;
        
        yield return moneyPanel.UpdateMoneyPanelText(-1f);

        var moneyPanelTextAfter = moneyPanelText.text;

        Assert.AreNotEqual(moneyPanelTextBefore, moneyPanelTextAfter, "Money panel value didn't change");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckMoneyPanelChangeColorBack()
    {
        var moneyPanel = GameObject.FindGameObjectWithTag("MoneyPanel").GetComponent<MoneyPanel>();
        var moneyPanelText = GameObject.FindGameObjectWithTag("MoneyPanel").GetComponent<TextMeshProUGUI>();
        var moneyPanelTextColorBefore = moneyPanelText.color;

        yield return moneyPanel.UpdateMoneyPanelText(1f);

        var moneyPanelTextColorAfter = moneyPanelText.color;

        Assert.AreEqual(moneyPanelTextColorBefore, moneyPanelTextColorAfter, "Money panel color changed");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckMoneyPanelShowMessage()
    {
        var moneyPanel = GameObject.FindGameObjectWithTag("MoneyPanel").GetComponent<MoneyPanel>();
        var message = _messagePanel.transform.GetChild(0).gameObject;

        yield return moneyPanel.UpdateMoneyPanelText(_limitController.WageringLimit - 0.01f);

        Assert.IsTrue(message.activeSelf, "Message panel is not active");

        Time.timeScale = 1;

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckLimitControllerMoneyDepositControl()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
        var financialController = GameObject.FindGameObjectWithTag("Board").GetComponent<FinancialController>();
        var message = _messagePanel.transform.GetChild(0).gameObject;

        financialController.IncreaseWinSum(-100f);
        playButton.OnMouseDown();

        Assert.IsTrue(message.activeSelf, "Message panel is not active");

        Time.timeScale = 1;

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckLimitControllerWageringLimitControl()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
        var message = _messagePanel.transform.GetChild(0).gameObject;

        _limitController.WageringLimit = 0f;
        playButton.OnMouseDown();

        Assert.IsTrue(message.activeSelf, "Message panel is not active");

        Time.timeScale = 1;

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckLimitControllerTimeLimitControl()
    {
        var message = _messagePanel.transform.GetChild(0).gameObject;

        _limitController.IncreaseTimeLimit(-900f);

        yield return null;

        Assert.IsTrue(message.activeSelf, "Message panel is not active");
        
        _limitController.IncreaseTimeLimit(900f);
        Time.timeScale = 1;

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckLimitControllerTimeAlertControl()
    {
        var message = _messagePanel.transform.GetChild(0).gameObject;

        _limitController.IncreaseTimeAlert(-300f);

        yield return null;

        Assert.IsTrue(message.activeSelf, "Message panel is not active");

        Time.timeScale = 1;

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckCoinsStartMoving()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
        var coin = GameObject.Find("Coin").GetComponent<Coin>();
        
        playButton.OnMouseDown();

        yield return null;

        Assert.AreEqual("Moving", coin.GetSymbolStatus(), "Coins are not moving");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckCoinsStartSlowing()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
        var symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
        var coin = GameObject.Find("Coin").GetComponent<Coin>();
        
        playButton.OnMouseDown();

        yield return new WaitForSecondsRealtime(symbolController.GetSpinTime() + symbolController.GetExtraSpinTime(coin.GetSymbolRow()) + 1.1f);

        Assert.AreEqual("Slowing", coin.GetSymbolStatus(), "Coins are not slowing");

        yield return null;
    }
    
    [UnityTest]
    public IEnumerator CheckCoinsStartStoppingOrStopped()
    {
        var playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<PlayButton>();
        var symbolController = GameObject.FindGameObjectWithTag("Board").GetComponent<SymbolController>();
        var coin = GameObject.Find("Coin").GetComponent<Coin>();
        
        playButton.OnMouseDown();

        yield return new WaitForSecondsRealtime(symbolController.GetSpinTime() + symbolController.GetExtraSpinTime(coin.GetSymbolRow()) + 4.1f);

        Assert.IsTrue(coin.GetSymbolStatus() == "Stopping" || coin.GetSymbolStatus() == "Stopped", "Coins are not stopping");
        
        Time.timeScale = 1;

        yield return null;
    }
}
