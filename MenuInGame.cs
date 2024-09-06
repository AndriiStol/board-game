using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInGame : MonoBehaviour
{
    public static MenuInGame Instance { get; set; }

    public void Start()
    {
        this.chatText.text = "Start Game";
        MenuInGame.Instance = this;
        this.SetSpeedCubs();
        this.SetTextMenu();
        this.SetActiveMenu();
        this.chatPanel.SetActive(false);
        base.gameObject.GetComponent<AudioSource>().clip = this.chatSound;
        this.SetTurnPanel(true);
        this.SetTurnNumber(0);
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            this.chatInput.ActivateInputField();
        }
        if (Input.GetKey(KeyCode.Return) && this.chatInput.text != "")
        {
            this.sendChatButton();
        }
    }

    

    private void SetSpeedCubs()
    {
        if (this.speedCubs == 1f)
        {
            this.rotationSpeed = 750;
            this.positionSpeed = 15;
        }
        else if (this.speedCubs == 2f)
        {
            this.rotationSpeed = 1000;
            this.positionSpeed = 20;
        }
        else if (this.speedCubs == 3f)
        {
            this.rotationSpeed = 2000;
            this.positionSpeed = 40;
        }
    }

    private void SetEnemyTurns()
    {
        BoardManager.Instance.viewHelpTurns = this.enemyTurns;
    }

    

   

    public void MenuButton()
    {
        this.optionsMenu.SetActive(!this.optionsMenu.activeSelf);
    }

    public void SpeedCubsButton()
    {

        this.SetSpeedCubs();
    }

    public void EnemyTurnsButton()
    {
        this.enemyTurns = !this.enemyTurns;
        this.SetEnemyTurns();
    }

   

    public void ExitButton()
    {
        this.exitMenu.SetActive(true);
    }

    public void YesButton()
    {
        if (BoardManager.Instance.isNetworkGame)
        {
        }
        Application.Quit();
    }

    public void NoButton()
    {
        this.exitMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        if (BoardManager.Instance.isNetworkGame)
        {
        }
        SceneManager.LoadScene("Menu");
    }

    public void RestartButton()
    {
        BoardManager.Instance.RestartGame();
        this.SetActiveMenu();
    }

    public void QuitButton()
    {
        this.YesButton();
    }

    public void chatActivatedButton()
    {
        this.chatPanel.SetActive(!this.chatPanel.activeSelf);
    }

    public void sendChatButton()
    {
        this.chatInput.text = "";
        this.isMyMsg = true;
    }

    public void onIncomingMsgChat(string msg)
    {
        string[] separator = new string[]
        {
            Environment.NewLine
        };
        string[] array = this.chatText.text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        string[] array2 = new string[array.Length - 1];
        if (array.Length == 8)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i != 0)
                {
                    array2[i - 1] = array[i];
                }
            }
            string text = string.Join(Environment.NewLine, array2);
            this.chatText.text = text;
        }
        Text text2 = this.chatText;
        text2.text = text2.text + Environment.NewLine + msg;
        if (!this.isMyMsg)
        {
            base.gameObject.GetComponent<AudioSource>().Play();
        }
        this.isMyMsg = false;
    }

    private void SetTextMenu()
    {
        this.menuActive.transform.Find("MenuButton").transform.Find("Text").GetComponent<Text>().text = this.menuButtonText;
        this.optionsMenu.transform.Find("SpeedCubs").transform.Find("Text").GetComponent<Text>().text = this.speedCubsText;
        this.optionsMenu.transform.Find("EnemyTurns").transform.Find("Text").GetComponent<Text>().text = ((!this.enemyTurns) ? this.enemyTurnsNoText : this.enemyTurnsYesText);
        this.optionsMenu.transform.Find("LanguageButton").transform.Find("Text").GetComponent<Text>().text = this.languageText;
        this.optionsMenu.transform.Find("ExitButton").transform.Find("Text").GetComponent<Text>().text = this.exitButtonText;
        this.exitMenu.transform.Find("Yes").transform.Find("Text").GetComponent<Text>().text = this.yesButtonText;
        this.exitMenu.transform.Find("No").transform.Find("Text").GetComponent<Text>().text = this.noButtonText;
        this.exitMenu.transform.Find("Quit").GetComponent<Text>().text = this.quitText;
        this.exitMenu.transform.Find("MainMenuButton").transform.Find("Text").GetComponent<Text>().text = this.mainMenuButtonText;
        this.winMenu.transform.Find("MainMenuWinButton").transform.Find("Text").GetComponent<Text>().text = this.mainMenuButtonText;
        this.winMenu.transform.Find("QuitWinButton").transform.Find("Text").GetComponent<Text>().text = this.quitButtonText;
        this.winMenu.transform.Find("RestartWinButton").transform.Find("Text").GetComponent<Text>().text = this.restartButtonText;
    }

    private void SetActiveMenu()
    {
        this.menuActive.SetActive(true);
        this.optionsMenu.SetActive(false);
        this.exitMenu.SetActive(false);
        this.winMenu.SetActive(false);
        this.ClientCloseMenu.SetActive(false);
    }

    private void SetChatMenu(bool network)
    {
        this.chatPanel.SetActive(network);
        this.chatButton.SetActive(network);
    }

    public void SetTurnPanel(bool isWhiteTurn)
    {
        if (isWhiteTurn)
        {
            this.turnPanel.GetComponent<Image>().color = Color.white;
            this.turnPanel.transform.Find("TurnText").GetComponent<Text>().text = "Ход Белых";
            this.turnPanel.transform.Find("TurnText").GetComponent<Text>().color = Color.black;
        }
        else
        {
            this.turnPanel.GetComponent<Image>().color = Color.black;
            this.turnPanel.transform.Find("TurnText").GetComponent<Text>().text = "Ход Черных";
            this.turnPanel.transform.Find("TurnText").GetComponent<Text>().color = Color.white;
        }
    }

    public void SetTurnNumber(int number)
    {
        this.turnNumberPanel.transform.Find("TurnNumberText").GetComponent<Text>().text = "Сделано ходов: " + number;
    }

    public void ActiveWinMenu(bool winIsWhite)
    {
        this.winMenu.transform.Find("Text").GetComponent<Text>().text = ((!winIsWhite) ? this.blackWinsText : this.whiteWinsText);
        this.winMenu.SetActive(true);
        Time.timeScale = 0;

    }

    public float speedCubs;

    private bool enemyTurns;

    private string language;

    public int rotationSpeed;

    public int positionSpeed;

    public AudioClip chatSound;

    private bool isMyMsg = false;

    public GameObject menuActive;

    public GameObject optionsMenu;

    public GameObject exitMenu;

    public GameObject winMenu;

    public GameObject turnPanel;

    public GameObject turnNumberPanel;

    public GameObject ClientCloseMenu;

    public GameObject chatButton;

    public GameObject chatPanel;

    public InputField chatInput;

    public Text chatText;

    private string menuButtonText;

    private string speedCubsText;

    private string enemyTurnsYesText;

    private string enemyTurnsNoText;

    private string languageText;

    private string exitButtonText;

    private string quitText;

    private string yesButtonText;

    private string noButtonText;

    public string mainMenuButtonText;

    public string blackWinsText;

    public string whiteWinsText;

    public string restartButtonText;

    public string quitButtonText;
}