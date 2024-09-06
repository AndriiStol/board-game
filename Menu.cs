using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu instance { get; set; }

    public void Start()
    {
        Menu.instance = this;
        this.MainMenuActive();
        
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void StartButton()
    {
        this.mainMenu.SetActive(false);
        this.startMenu.SetActive(true);
    }

    public void RulesButton()
    {
        this.mainMenu.SetActive(false);
        this.rulesMenu.SetActive(true);
    }

    public void OptionsButton()
    {
        this.mainMenu.SetActive(false);
        this.optionsMenu.SetActive(true);
    }

    public void AuthorsButton()
    {
        this.mainMenu.SetActive(false);
        this.authorsMenu.SetActive(true);
    }

    public void ExitButton()
    {
        this.exitMenu.SetActive(true);
    }

    public void YesExitButton()
    {
        Application.Quit();
    }

    public void NoExitButton()
    {
        this.exitMenu.SetActive(false);
    }

    public void LocalGameButton()
    {
        this.StartGame();
    }

    public void NetworkGameButton()
    {
        this.networkMenu.SetActive(true);
        this.startMenu.SetActive(false);
    }

    public void InternetGameButton()
    {
        this.startMenu.SetActive(false);
        if (this.isLogin)
        {
            this.internetPanel.SetActive(true);
        }
        else
        {
            this.loginPanel.SetActive(true);
        }
    }

    public void ConnectButton()
    {
       
    }

    public void HostButton()
    {
     
    }

    public void LoginOfLoginPanel()
    {
      
    }

    public void RegisterOfLoginPanel()
    {
        this.loginPanel.SetActive(false);
        this.registerPanel.SetActive(true);
    }

    public void GuestGameButton()
    {
      
    }

    public void BackLoginPanel()
    {
        this.startMenu.SetActive(true);
        this.loginPanel.SetActive(false);
    }

    public void RegisterOfRegisterPanel()
    {
        
        
    }

    public void BackRegisterPanel()
    {
        this.loginPanel.SetActive(true);
        this.registerPanel.SetActive(false);
    }

    public void ConnectRoomOfInternetPanel()
    {
        this.infoLabelOfInternetPanel.text = "В разработке";
    }

    public void CreateRoomOfInternetPanel()
    {
        this.infoLabelOfInternetPanel.text = "В разработке";
    }

    public void SearchRoomOfInternetPanel()
    {
    }

    public void BackInternetPanel()
    {
        this.internetPanel.SetActive(false);
        if (this.isLogin)
        {
            this.startMenu.SetActive(true);
        }
        else
        {
            this.loginPanel.SetActive(true);
        }
    }

    public void LogoutInternetButton()
    {
    }

    public void ConnectOfEnterPassPanel()
    {
    }

    public void BackEnterPassPanel()
    {
    }

    public void BackButton()
    {
        this.MainMenuActive();
    }

    public void BackNetworkButton()
    {
        this.networkMenu.SetActive(false);
        this.startMenu.SetActive(true);
    }

    public void BackWaitingMenu()
    {
       
    }

    

    public void EnemyTurnsButton()
    {
        this.enemyTurns = !this.enemyTurns;
       
    }

   

    public void SetOnline(string i)
    {
        this.online.text = "online: " + i;
    }

    public bool CheckChar(string message)
    {
        char[] source = "abcdefghijklmnopqrstuvwxyz_1234567890@.".ToCharArray();
        char[] array = message.ToLower().ToCharArray();
        foreach (char value in array)
        {
            if (!source.Contains(value))
            {
                return false;
            }
        }
        return true;
    }

    public void SetLoginWinLos()
    {
       
    }

    public void AutAccept()
    {
       
    }

    public void AutFailed()
    {
        this.infoLabelOfLoginPanel.text = "Неверный логин или пароль";
    }

    public void RegisterAccept()
    {
        this.loginPanel.SetActive(true);
        this.registerPanel.SetActive(false);
        this.infoLabelOfLoginPanel.text = "Регистрация успешна, авторизуйтесь.";
    }

    public void RegisterFail()
    {
        this.infoLabelOfRegisterPanel.text = "Регистрация не прошла, ошибка не сервере";
    }

    public void RegisterFailLogin()
    {
        this.infoLabelOfRegisterPanel.text = "Регистрация не прошла, логин занят";
    }

    private void SearchAndDestroyClient()
    {
       
    }

    private void SearchAndDestroyServer()
    {
       
    }

    public void WaitingMenuInternet()
    {
        this.internetPanel.SetActive(false);
        this.loginPanel.SetActive(false);
        this.waitingMenu.SetActive(true);
    }

    public void ConnectInternetButton()
    {
     
    }

    private void MainMenuActive()
    {
        this.mainMenu.SetActive(true);
        this.startMenu.SetActive(false);
        this.networkMenu.SetActive(false);
        this.waitingMenu.SetActive(false);
        this.rulesMenu.SetActive(false);
        this.optionsMenu.SetActive(false);
        this.authorsMenu.SetActive(false);
        this.exitMenu.SetActive(false);
        this.loginPanel.SetActive(false);
        this.registerPanel.SetActive(false);
        this.internetPanel.SetActive(false);
        this.enterPassPanel.SetActive(false);
    }

  



    

    

    

    

    



    public Text online;


    private string version = Setting.version;

    private int port = Setting.port;

    private bool enemyTurns;



    private string language;

    public GameObject mainMenu;

    public GameObject startMenu;

    public GameObject rulesMenu;

    public GameObject optionsMenu;

    public GameObject authorsMenu;

    public GameObject exitMenu;

    public GameObject infoPanel;

    public Button connectInternetButton;

    public GameObject networkMenu;

    public GameObject waitingMenu;

    private bool isOnline;

    private bool isLogin;

    public GameObject loginPanel;

    public GameObject registerPanel;

    public GameObject internetPanel;

    public GameObject enterPassPanel;

    private string speedCubsText;

    public Text loginText;

    public Text winLosText;

    public Text infoLabelOfLoginPanel;

    public InputField inputLoginOfLoginPanel;

    public InputField inputPassOfLoginPanel;

    public Text infoLabelOfRegisterPanel;

    public InputField inputLoginOfRegisterPanel;

    public InputField inputEmail;

    public InputField inputPassOfRegisterPanlel;

    public InputField InputRePass;

    public Text infoLabelOfInternetPanel;

    public InputField inputRoom;

    public InputField inputCreateRoom;

   
    public InputField inputEnterPass;

  
    public GameObject serverPrefab;

    public GameObject clientPrefab;

    public InputField nameInput;

    private string startButtonText;

    private string rulesButtonText;

    private string optionsButtonText;

    private string exitButtonText;

    private string authorsButtonText;

    private string localButtonText;

    private string networkButtonText;

    private string internetButtonText;

    private string connectButtontext;

    private string hostButtonText;

    private string nameText;

    private string backButtonText;

    private string yesButtonText;

    private string noButtonText;

    private string languageButtonText;



    private string enemyTurnsYesText;

    private string enemyTurnsNoText;

    private string authorsText;

    private string rulesText;

    private string quitText;
}
