using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//holds values for the class you create
[CreateAssetMenuAttribute(fileName="New Custom Class Data", menuName ="Custom Class Data")]
public class CustomClassData : ScriptableObject
{
    public string className;
    public int level = 1;

    [Header("Stats of Class")]
    public int healthStat;
    public int attackStat;
    public int defenseStat;
    public int speedStat;        

    [Header("Chance Stats will Increase on Level Up")]
    public int chanceToIncreaseHealth;
    public int chanceToIncreaseAttack;
    public int chanceToIncreseDefense;
    public int chanceToIncreaseSpeed;

    [Header("Base Stats of Class at Level 1")]
    public int originallevel = 1;
    public int originalhealth;
    public int originalattack;
    public int originaldefense;
    public int originalspeed;
}
