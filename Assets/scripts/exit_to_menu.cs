using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class exit_to_menu : MonoBehaviour
{

    public Button yes, no;
    public Animator escape_anim;
    // Start is called before the first frame update
    void Start()
    {
        yes.onClick.AddListener(yes_fun);
        no.onClick.AddListener(no_fun);
    }

    void yes_fun()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void no_fun()
    {
        escape_anim.SetTrigger("escape_down");
        escape_anim.SetBool("escape_up", false);
        escape_anim.SetBool("back", true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escape_anim.SetTrigger("escape_up");
            escape_anim.SetBool("escape_down", false);
        }
    }
}
