using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;
    [SerializeField] private List<HeroController> _blueTeam;
    [SerializeField] private List<HeroController> _redTeam;

    [SerializeField] private List<HeroController> _turnFight;

    public int _heroAliveRedTeam;
    public int _heroAliveBlueTeam;

    private void Awake()
    {
        BoardController.Instance = this;
    }
    private void Start()
    {
        InitTurnFight();
        StartCoroutine(Combat());
    }

    private void InitTurnFight()
    {
        _heroAliveRedTeam = 0;
        _heroAliveBlueTeam = 0;
        _turnFight = new List<HeroController>();
        for (int i = 0; i < _blueTeam.Count; i++)
        {
            if (_blueTeam[i].Data.HP > 0)
            {
                _heroAliveBlueTeam++;
                _turnFight.Add(_blueTeam[i]);
            }
        }
        for (int i = 0; i < _redTeam.Count; i++)
        {
            if (_redTeam[i].Data.HP > 0)
            {
                _heroAliveRedTeam++;
                _turnFight.Add(_redTeam[i]);
            }
        }
        _turnFight.Sort((a, b) => a.Data.SPD - b.Data.SPD);
    }

    bool CheckEnd()
    {
        if (_blueTeam.Where(hero => hero.Data.HP > 0).Count() == 0)
        {
            Debug.Log("Lose");
            return true;
        }
        if (_redTeam.Where(hero => hero.Data.HP > 0).Count() == 0)
        {
            Debug.Log("Win");
            return true;
        }
        return false;
    }
    IEnumerator Combat()
    {
        InitTurnFight();
        for (int i = 0; i < _turnFight.Count(); i++)
        {
            if (_turnFight[i].Data.HP > 0)
            {
                if (_turnFight[i].Team == 0)
                {
                    if (_heroAliveRedTeam > 0)
                        _turnFight[i].Attack(_redTeam.Where(hero => hero.Data.HP > 0).ToList()[0]);
                }
                else
                {
                    if (_heroAliveBlueTeam > 0)
                        _turnFight[i].Attack(_blueTeam.Where(hero => hero.Data.HP > 0).ToList()[0]);
                }
            }
            yield return new WaitForSeconds(0f);
        }
        if (!CheckEnd())
            StartCoroutine(Combat());
    }
}
