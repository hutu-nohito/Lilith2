using UnityEngine;
using System.Collections;

public class Search_Element : MonoBehaviour {

    private Enemy_ControllerZ ecZ;//敵の状態
    private GameObject Player;

    // Use this for initialization
    void Start()
    {

        ecZ = GetComponentInParent<Enemy_ControllerZ>();
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {

        if (col.tag == "Enemy")
        {
            switch (ecZ.GetHabit())
            {
                case Enemy_ControllerZ.Enemy_Habit.Positive:
                    ecZ.SetState(Enemy_ControllerZ.Enemy_State.Attack);
                    break;
                case Enemy_ControllerZ.Enemy_Habit.Normal:
                    break;
                case Enemy_ControllerZ.Enemy_Habit.Negative:
                    ecZ.SetState(Enemy_ControllerZ.Enemy_State.Run);
                    break;

            }
        }
        if (col.tag == "Bullet")
        {
            if (col.GetComponent<Attack_Parameter>().GetParent() == Player)
                if (ecZ.GetSearch() == Enemy_ControllerZ.Enemy_Search.Magic) ecZ.SetState(Enemy_ControllerZ.Enemy_State.Attack); ;
        }
    }

    void OnTriggerExit(Collider col)
    {

        if (col.tag == "Player")
        {
            switch (ecZ.GetHabit())
            {
                case Enemy_ControllerZ.Enemy_Habit.Positive:
                    ecZ.SetState(Enemy_ControllerZ.Enemy_State.Search);
                    break;
                case Enemy_ControllerZ.Enemy_Habit.Normal:
                    break;
                case Enemy_ControllerZ.Enemy_Habit.Negative:
                    ecZ.SetState(Enemy_ControllerZ.Enemy_State.Search);
                    break;

            }
        }
    }
}
