using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField] private int _team;
    [SerializeField] private HeroData _data;

    public HeroData Data { get => _data; set => _data = value; }
    public int Team { get => _team; set => _team = value; }

    public void Attack(HeroController enemy)
    {
        enemy.GetDamage(Data.ATK);
    }

    public void GetDamage(int damage)
    {
        Data.HP -= damage;
        Debug.Log("Get Damage: " + damage);
        if(Data.HP <= 0)
        {
            if(_team == 0)
            {
                BoardController.Instance._heroAliveBlueTeam--;
            }
            else
            {
                BoardController.Instance._heroAliveRedTeam--;
            }
            Data.HP = 0;
        }
    }
}
