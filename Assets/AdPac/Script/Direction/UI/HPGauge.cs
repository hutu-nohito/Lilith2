using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPGauge : MonoBehaviour
{

    private Image circleGauge; // CircleGauge

    private Player_ControllerZ pcZ;
    private float maxHP = 100;
    private float oldHP = 100;

    void Start()
    {
        circleGauge = GetComponent<Image>();
        pcZ = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_ControllerZ>();
        oldHP = pcZ.GetHP();
        maxHP = oldHP;
    }

    void Update()
    {
        changePicturesUpdate();
    }

    void changePicturesUpdate()
    {
        if (pcZ.GetHP() != oldHP)
        {
            // FillAmount
            circleGauge.fillAmount = pcZ.GetHP() / maxHP;

        }

        oldHP = pcZ.GetHP();
    }
}
