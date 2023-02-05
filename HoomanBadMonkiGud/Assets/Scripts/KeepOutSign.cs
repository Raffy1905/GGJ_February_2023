using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOutSign : MonoBehaviour
{
    public GameObject blocker;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            //TODO: Dialogue
            if(Player.Instance.GetDevolutionState() == Player.DevolutionState.HUMAN)
            {
                Player.Instance.ownRigidbody.AddForce(new Vector2(-5, 2), ForceMode2D.Impulse);
            }
            Destroy(blocker);
        }
    }
}
