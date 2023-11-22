using GDL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotHp : MonoBehaviour, IHealth
{
    private Slider slider;
    public float maxHp = 10;
    public float hp;
    [SerializeField] Animator anim;
    EnemyBrain enemyBrain;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        anim = GetComponent<Animator>();
        enemyBrain = GetComponent<EnemyBrain>();
        slider.value = 1;
        hp = maxHp;
    }
    public void ReduceHp(float hp)
    {
        this.hp -= hp;
        slider.value -= hp/maxHp;

        if (this.hp<0) Dead();
        enemyBrain.isChasing = true;
    }

    private void Dead()
    {
        enemyBrain.IsAlive = false;
        anim.SetBool("alive", false);
        Destroy(gameObject, 5f);
    }
}
