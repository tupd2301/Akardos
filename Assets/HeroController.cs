using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    [SerializeField] private int _team;
    [SerializeField] private HeroData _data;
    [SerializeField] private int _hpMax;

    public HeroData Data { get => _data; set => _data = value; }
    public int Team { get => _team; set => _team = value; }

    private void Start()
    {
        _hpMax = _data.HP;
        GetComponentInChildren<Slider>().value = 1;
    }
    public void Attack(HeroController enemy)
    {
        enemy.GetDamage(Data.ATK);
    }

    public void GetDamage(int damage)
    {
        Data.HP -= damage;
        GetComponentInChildren<Slider>().value = Data.HP*1f / _hpMax;
        BoardController.Instance.ShowDamage(transform.position, damage);
        if(Data.HP <= 0)
        {
            GetComponent<Image>().color = new Color32(100, 100, 100, 255);
            if(_team == 0)
            {
                BoardController.Instance.HeroAliveBlueTeam--;
            }
            else
            {
                BoardController.Instance.HeroAliveRedTeam--;
            }
            Data.HP = 0;
        }
    }
}
