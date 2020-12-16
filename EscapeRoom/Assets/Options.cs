using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    private bool isMute;
    // Start is called before the first frame update
    void Start()
    {
        isMute = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Back()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void isMutePressed()
    {
        isMute = !isMute;
        AudioListener.pause = isMute;    
    }
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void Credits()
    {
        SceneManager.LoadScene("credits");
    }
}
