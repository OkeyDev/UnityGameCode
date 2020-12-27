using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EmemyHP : MonoBehaviour
{
    public static Color GetDamageColor;

    public float DamageWait; 
    public Slider HealthSlider;
    public Image FillImage;
    public Gradient FillGradient;

    //bool isDamageActive = true;
    float HP = 3;
    Rigidbody2D Rigidbody;

    public void GetDamage(float damage)
    {
        //if (!isDamageActive)
        //{
        //    return;
        //}

        //isDamageActive = false;
        HP -= 1;
        HealthSlider.value = HP;
        if (HP <= 0)
        {
            DestroyMe();
        }
        //StartCoroutine(NonAttackWait());
        FillImage.color = FillGradient.Evaluate(1- (HP/3) );

    }
    /*
    public IEnumerator NonAttackWait()
    {
        float count = 0;
        while (count <= DamageWait) {
            yield return new WaitForSeconds(1f);
            Debug.Log("WAIT");
            count += 1;
        }
        isDamageActive = true;
    }
    */
    void DisplayHP()
    {
        HealthSlider.value = HP;
    }

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    void Start()
    {
        DisplayHP();
    }

}