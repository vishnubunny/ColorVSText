using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{
    public Image image, blurImage, fill;
    float v1, v2;
    public Canvas can;
    public Camera cam;
    byte g, bl, text_r = 0, text_g = 255, text_b = 0;
    float start, end;
    public Slider slider;
    public GameObject skoreAnim;
    public Animator anim, skore, gameover_anim;
    int highscore = 0;
    public Animation an;
    public Text Final_score;

    public AudioSource correct, wrong;
    public Text text, score;
    public int flip, flag = 0, count = 0, scoreValue = 0, total_score = 0;
    public Button red, blue, green, purple, white, cyan, yellow, retry, exit;
    public Button colorButton, textButton;
    Color32[] colors = { new Color(0, 0, 255), new Color(255, 0, 0), new Color(0, 255, 0), new Color(255, 255, 0), new Color(255, 255, 255), new Color32(128, 0, 128, 255), Color.cyan };
    string[] colorNames = {"RED", "BLUE", "GREEN", "YELLOW", "CYAN", "WHITE", "PURPLE" };
    // Start is called before the first frame update
    void Start()
    {
        
        rand();
        v1 = 0;
        v2 = 0;
        g = 255;
        bl = 255;
        red.onClick.AddListener(delegate { fun(red); });
        blue.onClick.AddListener(delegate { fun(blue); });
        green.onClick.AddListener(delegate { fun(green); });
        yellow.onClick.AddListener(delegate { fun(yellow); });
        cyan.onClick.AddListener(delegate { fun(cyan); });
        white.onClick.AddListener(delegate { fun(white); });
        purple.onClick.AddListener(delegate { fun(purple); });
        retry.onClick.AddListener(retry_fun);
        exit.onClick.AddListener(exit_fun);
        

    }

    
    void retry_fun()
    {
        red.interactable = true;
        blue.interactable = true;
        green.interactable = true;
        yellow.interactable = true;
        cyan.interactable = true;
        white.interactable = true;
        purple.interactable = true;

        count = 0;
        gameover_anim.SetTrigger("game_over_reverse");
        gameover_anim.SetBool("game_over_bool", false);
        gameover_anim.SetBool("reset", true);

        v1 = 0;
        v2 = 0;
        total_score = 0;
        score.text = "score : 0";
        slider.value = 100;
        rand();
    }

    void exit_fun()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 0)
            slide();
        else if(flag == 1)
        {
            count = 0;
            slider.value = Mathf.Lerp(slider.value, 100, 0.4f);
            
        }
        if (slider.value >= 99.7f)
            flag = 0;
        if (slider.value <= 0.65f && count == 0)
        {
            wrong.Play();
            anim.CrossFade("wrong_anim", 0f, 0, 0f);
            count = 1;
            highscore_fun();
        }

        


    }

    void slide()
    {
        slider.value = slider.value - 0.55f;
        if (v1 <= 255)
            g = (byte)(255 - v1);
        if (v2 <= 255)
            bl = (byte)(255 - v2);
        fill.color = new Color32(255, g, bl, 255);
        if (g > 220)
        {
            v1 = v1 + 3f;
            v2 = v2 + 5f;
        }
        else
        {
            v1 = v1 + 1.5f;
            v2 = v2 + 2.4f;
        }
    }

    void highscore_fun()
    {
        red.interactable=false;
        blue.interactable = false;
        green.interactable = false;
        yellow.interactable = false;
        cyan.interactable = false;
        white.interactable = false;
        purple.interactable = false;


        if (total_score>highscore)
        {
            highscore = total_score;
            PlayerPrefs.SetInt("High Score", highscore);

        }
        gameover_anim.SetTrigger("game_over_bool");
        gameover_anim.SetBool("game_over_reverse", false);
        Final_score.text = "score : " + total_score.ToString();
    }

    void fun(Button b)
    {
        count = 1;
        if(flip == 0)
        {
            if(text.color == b.image.color)
            {
                correct_fun();
                anim.CrossFade("correct_anim", 0f, 0, 0f);
            }
            else
            {
                flag = 2;
                wrong.Play();
                anim.CrossFade("wrong_anim", 0f, 0, 0f);
                highscore_fun();
            }
        }
        else
        {
            if (text.text == b.tag)
            {
                correct_fun();
                anim.CrossFade("correct_anim", 0f, 0, 0f);

                //anim.SetBool("correct_reverse", true);
            }
            else
            {
                flag = 2;
                wrong.Play();

                anim.CrossFade("wrong_anim", 0f, 0, 0f);

                //anim.SetBool("play", true);
                //anim.SetBool("play_reverse", false);
                highscore_fun();
            }
        }
    }
    void correct_fun()
    {
        correct.Play();
        flag = 1;
        end = Time.time;

        if (end - start > 1.5)
        {
            scoreValue = 1;
            text_r = 255;
            text_g = 255;
            text_b = 255;
        }
        else if (end - start >= 1.2 && end - start - start <= 1.5)
        {
            scoreValue = 2;
            text_r = 128;
            text_g = 255;
            text_b = 128;
        }
        else if (end - start >= 0.9 && end - start <= 1.2)
        {
            scoreValue = 3;
            text_r = 96;
            text_g = 255;
            text_b = 96;
        }
        else if (end - start >= 0.6 && end - start <= 0.9)
        {
            scoreValue = 4;
            text_r = 48;
            text_g = 255;
            text_b = 48;
        }
        else if (end - start >= 0.5 && end - start <= 0.6)
        {
            scoreValue = 5;
            text_r = 0;
            text_g = 255;
            text_b = 0;
        }
        else if (end - start < 0.5)
        {
            scoreValue = 10;
            text_r = 255;
            text_b = 0;
            text_g = 0;
        }
        total_score = total_score + scoreValue;
        score.text = "score : " + (total_score).ToString();
    


        GameObject gog = Instantiate(skoreAnim, new Vector3(281, 100, 0), Quaternion.identity) as GameObject ;
        gog.transform.SetParent(can.transform);
        //gg.transform.parent = skoreAnim.transform;
        gog.GetComponent<Text>().text = "+"+scoreValue.ToString();
        gog.GetComponent<Text>().color = new Color32(text_r, text_g, text_b, 255);

        gog.GetComponent<Animator>().SetBool("skore", true);
        fill.color = new Color32(255, 255, 255, 255);
        v1 = 0;
        v2 = 0;
        g = 255;
        bl = 255;
        rand();
    }
    
    void rand()
    {
        start = Time.time;
        text.text = colorNames[Random.Range(0, 7)];
        text.color = colors[Random.Range(0, 7)];
        flip = Random.Range(0, 2);
        if (flip == 0)
        {
            colorButton.image.color = Color.white;
            colorButton.GetComponentInChildren<Text>().color = Color.black;
            textButton.image.color = Color.black;
            textButton.GetComponentInChildren<Text>().color = Color.white;
        }
        else
        {
            textButton.image.color = Color.white;
            textButton.GetComponentInChildren<Text>().color = Color.black;
            colorButton.image.color = Color.black;
            colorButton.GetComponentInChildren<Text>().color = Color.white;
        }
    }

    
}
