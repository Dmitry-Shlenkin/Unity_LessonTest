using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ReactiveTarget : MonoBehaviour
{    
    public Image health_raw_image;
    private float health;
    private GameObject MashRefreser;

    private void Start()
    {
    }

    public void ReactToHit(int damage)
    {
        if (health - damage <= 0)
        {
            health_raw_image.fillAmount = 0;
            StartCoroutine(Die());  
        }
        else
        {
            health -= damage;
            health_raw_image.fillAmount = health /100;
            //this.gameObject.SendMessage(health.ToString());
        }
    }
    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
