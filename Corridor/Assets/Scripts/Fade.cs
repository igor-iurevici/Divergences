using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public float endPosition = 400;
    private bool flag = false;

    void Update()
    {
        float currentPosition = player.transform.position.x;
        if (currentPosition > endPosition && !flag)
        {
           FadeToBeginning();
           flag = true;
        } 
    }

    public void FadeToBeginning()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(0);
    }
}
