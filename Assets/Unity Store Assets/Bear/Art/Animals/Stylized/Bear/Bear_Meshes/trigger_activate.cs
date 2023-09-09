using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_activate : MonoBehaviour
{
    private Animator mAnimator;
    public GameObject player;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mAnimator!=null)
        {
            if (Vector3.Distance(enemy.transform.position, player.transform.position) < 1)
            {
                //mAnimator.SetTrigger("AttackTR");
            }
        }
    }
}
