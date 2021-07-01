using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*
// Health.cs
//*
// Class behaviour
//*
// @category   3rd Person Survival Shooter Pro
// @tutorial   GameDevHQ
// @author     Dave González
// @copyright  2021 Dave González
// @version    CVS: 0.1
// @link       website
//*
public class Health : MonoBehaviour
{

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Inflicts Damage
    // <param name="damageAmount">The damage amount</param>
    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        if (_currentHealth < _minHealth)
            Destroy(this.gameObject);
    }
}
