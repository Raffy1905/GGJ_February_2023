using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public enum DevolutionState
    {
        HUMAN, CAVEMEN, MONKEY
    }

    public DevolutionState state = DevolutionState.HUMAN;

    public GunBullet gunBullet;
    public StoneBullet stoneBullet;
    public BananaBullet bananaBullet;

    public float humanSpeed = 3, cavemenSpeed = 5, monkeySpeed = 7;
    public float humanJump = 5, cavemenJump = 8, monkeyJump = 10;

    protected override void Attack()
    {
        // Attack is not used -> PlayerController.Shoot();
    }

    public DevolutionState GetDevolutionState()
    {
        return state;
    }

    public GameObject GetBullet()
    {
        switch (state)
        {
            case DevolutionState.HUMAN: return gunBullet.gameObject;
            case DevolutionState.CAVEMEN: return stoneBullet.gameObject;
            case DevolutionState.MONKEY: return bananaBullet.gameObject;
            default: return null;
        }
    }

    public float GetWalkingSpeed()
    {
        switch (state)
        {
            case DevolutionState.HUMAN: return humanSpeed;
            case DevolutionState.CAVEMEN: return cavemenSpeed;
            case DevolutionState.MONKEY: return monkeySpeed;
            default: return 0;
        }
    }

    public float GetJumpPower()
    {
        switch (state)
        {
            case DevolutionState.HUMAN: return humanJump;
            case DevolutionState.CAVEMEN: return cavemenJump;
            case DevolutionState.MONKEY: return monkeyJump;
            default: return 0;
        }
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
