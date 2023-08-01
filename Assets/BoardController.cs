using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;
    [SerializeField] private List<HeroController> _blueTeam;
    [SerializeField] private List<HeroController> _redTeam;

    [SerializeField] private List<HeroController> _turnFight;
    [SerializeField] private FloatingText _normalDamageText;
    [SerializeField] private FloatingText _roundText;

    private int _heroAliveRedTeam;
    private int _heroAliveBlueTeam;

    public int round;

    public int HeroAliveRedTeam { get => _heroAliveRedTeam; set => _heroAliveRedTeam = value; }
    public int HeroAliveBlueTeam { get => _heroAliveBlueTeam; set => _heroAliveBlueTeam = value; }

    private void Awake()
    {
        BoardController.Instance = this;
    }
    private void Start()
    {
        round = 0;
        InitTurnFight();
        StartCoroutine(Combat());
    }

    private void InitTurnFight()
    {
        HeroAliveRedTeam = 0;
        HeroAliveBlueTeam = 0;
        _turnFight = new List<HeroController>();
        for (int i = 0; i < _blueTeam.Count; i++)
        {
            if (_blueTeam[i].Data.HP > 0)
            {
                HeroAliveBlueTeam++;
                _turnFight.Add(_blueTeam[i]);
            }
        }
        for (int i = 0; i < _redTeam.Count; i++)
        {
            if (_redTeam[i].Data.HP > 0)
            {
                HeroAliveRedTeam++;
                _turnFight.Add(_redTeam[i]);
            }
        }
        _turnFight.Sort((a, b) => b.Data.SPD - a.Data.SPD);
    }

    bool CheckEnd()
    {
        if (_blueTeam.Where(hero => hero.Data.HP > 0).Count() == 0)
        {
            Debug.Log("Red Win");
            return true;
        }
        if (_redTeam.Where(hero => hero.Data.HP > 0).Count() == 0)
        {
            Debug.Log("Blue Win");
            return true;
        }
        return false;
    }
    IEnumerator Combat()
    {
        yield return new WaitForSeconds(1f);
        InitTurnFight();
        ShowRound(++round);
        for (int i = 0; i < _turnFight.Count(); i++)
        {
            _normalDamageText.gameObject.SetActive(false);
            if (_turnFight[i].Data.HP > 0)
            {
                if (_turnFight[i].Team == 0)
                {
                    if (HeroAliveRedTeam > 0)
                        _turnFight[i].Attack(_redTeam.Where(hero => hero.Data.HP > 0).ToList()[0]);
                }
                else
                {
                    if (HeroAliveBlueTeam > 0)
                        _turnFight[i].Attack(_blueTeam.Where(hero => hero.Data.HP > 0).ToList()[0]);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
        if (!CheckEnd())
        {
            _roundText.gameObject.SetActive(false);
            StartCoroutine(Combat());
        }
    }

    public void ShowRound(int round)
    {
        _roundText.transform.position = Vector3.zero;
        _roundText.GetComponentInChildren<TextMeshPro>().text = "Round " + round.ToString();
        _roundText.gameObject.SetActive(true);
    }

    public void ShowDamage(Vector3 pos, int damage)
    {
        _normalDamageText.transform.position = new Vector3(pos.x, pos.y);
        _normalDamageText.GetComponentInChildren<TextMeshPro>().text = "-" + damage.ToString();
        _normalDamageText.gameObject.SetActive(true);
    }
}
