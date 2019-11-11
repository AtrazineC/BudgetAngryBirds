using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu_Manager : MonoBehaviour
{

    public GameObject MenuScreen;
    public GameObject LevelSelectScreen;

    public Button StartGameButton;
    public Button LevelSelectionButton;

    public Button[] LevelButtons = new Button[8];

    private bool SelectingLevel = false;

    // Start is called before the first frame update
    void Start()
    {
      FindObjectOfType<AudioManager>().Play("SelectScreen");
        // play music???

        //Button start = StartGameButton.GetComponent<Button>();
        //start.onClick.AddListener(delegate{StartGame(1);});

        //Button selectlevel = StartGameButton.GetComponent<Button>();
        //selectlevel.onClick.AddListener(SelectLevel);

        //for (int i = 0; i < 8; i++) {
        //    Button yeet = LevelButtons[i].GetComponent<Button>();
        //    yeet.onClick.AddListener(delegate{StartGame(i);});
        //}

    }

    // Start the game at level 1.
    void StartGame(int i) {

        MenuScreen.SetActive(false);
        LevelSelectScreen.SetActive(false);
        SceneManager.LoadScene(i);
        FindObjectOfType<AudioManager>().StopPlaying("SelectScreen");

    }

    // Go to the level selection menu.
    void SelectLevel() {

        SelectingLevel = true;
        MenuScreen.SetActive(false);
        LevelSelectScreen.SetActive(true);

    }

    bool Held() {

        if ( (Time.realtimeSinceStartup - tick) <= 0.5 ) {
            return false;
        } else {
            return true;
        }

    }

    private float tick = 0f;
    private int currentLevel = 0;
    private bool transitioned = false;

    void Update() {

        if (SelectingLevel == false) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                tick = Time.realtimeSinceStartup;
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                if ( Held() == false ) {
                    StartGame(1);
                } else {
                    SelectLevel();
                    transitioned = true;
                }
            }
        } else if (transitioned) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                tick = Time.realtimeSinceStartup;
            }

            if (Input.GetKeyUp(KeyCode.Space)) {
                if ( Held() == false ) {
                    currentLevel++;
                    if (currentLevel >= 8) {
                        currentLevel = 0;
                    }

                    for (int i = 0; i < 8; i++) {
                        var colors = LevelButtons[i].GetComponent<Button>().colors;
                        colors.normalColor = Color.white;

                        if (i == currentLevel) {
                            colors.normalColor = new Color(255f/255f, 126f/255f, 126f/255f);
                        }

                        LevelButtons[i].GetComponent<Button>().colors = colors;
                    }
                } else {
                    StartGame(currentLevel + 1);
                }
            }
        }

    }
}
