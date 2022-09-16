using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuMenager : MonoBehaviour
{
    public int diamondAmount;

    public Text highScore;
    public Text diamondsText;

    public bool isbought = false;

    public GameObject buyButton;
    
    public Sprite[] sprites;
    public Image birdImage;

    public int birdIndex = 0;

    public Text buyText;
    public int[] prices;

    private void Start()
    {
        //amount of diamonds
        diamondsText.text = PlayerPrefs.GetInt("diamondAmount", 0).ToString();
        for (int i = 0; i < sprites.Length; i++)
        {
            if (PlayerPrefs.GetString(PlayerPrefs.GetInt("birdImageIndex", birdIndex).ToString()) != "Bought")
            {
                PlayerPrefs.SetString(PlayerPrefs.GetInt("birdImageIndex", birdIndex).ToString(), "NotBought");
            }
        }
        
        // highScore
        highScore.text = "HighScore : " + PlayerPrefs.GetInt("highScore", 0).ToString();
        //birdImage
        birdImage.sprite = sprites[birdIndex];
        PlayerPrefs.SetInt("birdImageIndex", birdIndex);
    }

    private void Update()
    {
        if (isbought)
        {
            PlayerPrefs.DeleteAll();
        }
        buyText.text = prices[PlayerPrefs.GetInt("birdImageIndex", birdIndex)].ToString();
        
        if(PlayerPrefs.GetString(PlayerPrefs.GetInt("birdImageIndex", birdIndex).ToString()) == "Bought")
        {
            buyButton.SetActive(false);
        }else
        {
            buyButton.SetActive(true);
        }

    }
    public void PlayTheGameButton()
    {
        SceneManager.LoadScene(1);
    }
    public void RightBird()
    {
        birdImage.sprite = sprites[RightSwitch()];
    }
    public void LeftBird()
    {
        birdImage.sprite = sprites[LeftSwitch()] ;
    }
    private int RightSwitch()
    {
        if (birdIndex + 1  == sprites.Length)
        {
            birdIndex = 0;
        }else
        {
            birdIndex++;
        }

        PlayerPrefs.SetInt("birdImageIndex", birdIndex);
        return birdIndex;
    }
    private int LeftSwitch()
    {
        if (birdIndex == 0)
        {
            birdIndex = sprites.Length -1;
        }
        else
        {
            birdIndex--;
        }
        PlayerPrefs.SetInt("birdImageIndex", birdIndex);

        return birdIndex;
    }

    public void BuyTheBird()
    {
        // decline number of diamonds
        int amountofDimonds = PlayerPrefs.GetInt("diamondAmount");
        if (amountofDimonds >= prices[PlayerPrefs.GetInt("birdImageIndex", birdIndex)] )
        {
            //set bird to active
            PlayerPrefs.SetString(PlayerPrefs.GetInt("birdImageIndex", birdIndex).ToString(), "Bought");

            amountofDimonds = amountofDimonds - prices[PlayerPrefs.GetInt("birdImageIndex", birdIndex)];
            PlayerPrefs.SetInt("diamondAmount", amountofDimonds);
        }
    }



}
