using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour
{
    public Button play, how, ok;
    public Animator how_anim;
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(load);
        how.onClick.AddListener(how_fun);
        ok.onClick.AddListener(ok_fun);
    }

    void load ()
    {
        SceneManager.LoadScene("Game");
    }

    void how_fun()
    {
        how_anim.SetTrigger("up");
        how_anim.SetBool("down", false);
    }

    void ok_fun()
    {
        how_anim.SetTrigger("down");
        how_anim.SetBool("up", false);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
