using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    

    public enum DevolutionState
    {
        HUMAN, CAVEMEN, MONKE
    }

    public static Player Instance;

    public DevolutionState state = DevolutionState.HUMAN;

    public GunBullet gunBullet;
    public StoneBullet stoneBullet;
    public BananaBullet bananaBullet;

    public float humanSpeed = 3, cavemenSpeed = 5, monkeySpeed = 7;
    public float humanJump = 5, cavemenJump = 8, monkeyJump = 10;
    public float climbingFactor = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Attack()
    {
        // Attack is not used -> PlayerController.Shoot();
    }

    public void Devolve()
    {
        switch (state)
        {
            case DevolutionState.HUMAN:
                {
                    state = DevolutionState.CAVEMEN;
                    break;
                }
            case DevolutionState.CAVEMEN:
                {
                    state = DevolutionState.MONKE;
                    break;
                }
            default: break;
        }
    }

    public DevolutionState GetDevolutionState()
    {
        return state;
    }

    public GameObject GetBullet()
    {
        return state switch
        {
            DevolutionState.HUMAN => gunBullet.gameObject,
            DevolutionState.CAVEMEN => stoneBullet.gameObject,
            DevolutionState.MONKE => bananaBullet.gameObject,
            _ => null,
        };
    }

    public float GetWalkingSpeed()
    {
        return state switch
        {
            DevolutionState.HUMAN => humanSpeed,
            DevolutionState.CAVEMEN => cavemenSpeed,
            DevolutionState.MONKE => monkeySpeed,
            _ => 0,
        };
    }

    public float GetJumpPower()
    {
        return state switch
        {
            DevolutionState.HUMAN => humanJump,
            DevolutionState.CAVEMEN => cavemenJump,
            DevolutionState.MONKE => monkeyJump,
            _ => 0,
        };
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
