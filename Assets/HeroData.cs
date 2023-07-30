using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HeroData
{
    [SerializeField] private int _hP;
    [SerializeField] private int _mP;
    [SerializeField] private int _aTK;
    [SerializeField] private int _dEF;
    [SerializeField] private int _sPD;
    [SerializeField] private List<int> _skill;

    public int HP { get => _hP; set => _hP = value; }
    public int MP { get => _mP; set => _mP = value; }
    public int ATK { get => _aTK; set => _aTK = value; }
    public int DEF { get => _dEF; set => _dEF = value; }
    public int SPD { get => _sPD; set => _sPD = value; }
    public List<int> Skill { get => _skill; set => _skill = value; }
}
