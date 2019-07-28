using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public int coinCount { get; set; }
    public int damageBuffCount { get; set; }
    public int rangeBuffCount { get; set; }
    public int healthBuffCount { get; set; }
    public int speedBuffCount { get; set; }
    public int defaultBuffCount { get; set; }
    public int specialBuffCount { get; set; }
    public int abilityBuffCount { get; set; }
    public int lives { get; set; }

    public PlayerStats(int coinCount, int damageBuffCount, int rangeBuffCount, int healthBuffCount, int speedBuffCount, int defaultBuffCount, int specialBuffCount, int abilityBuffCount, int lives)
    {
        this.coinCount = coinCount;
        this.damageBuffCount = damageBuffCount;
        this.rangeBuffCount = rangeBuffCount;
        this.healthBuffCount = healthBuffCount;
        this.speedBuffCount = speedBuffCount;
        this.defaultBuffCount = defaultBuffCount;
        this.specialBuffCount = specialBuffCount;
        this.abilityBuffCount = abilityBuffCount;
        this.lives = lives;
    }
}
