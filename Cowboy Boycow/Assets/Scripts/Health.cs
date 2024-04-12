using TMPro;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] TextMeshProUGUI printer;
    [SerializeField] GameObject gameOverMenu;


    private void start()
    {
        printer.text = "Health: " + health;
    }



    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddHealth(float healtToAdd)
    {
        printer.text = "Health: " + health;
        health += healtToAdd;

        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
        Time.timeScale = 0f;
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(true);
        }
    }
}