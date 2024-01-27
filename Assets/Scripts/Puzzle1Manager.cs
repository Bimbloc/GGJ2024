using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Puzzle1Manager : MonoBehaviour
{
    [SerializeField] GoteraResinaHandler[] goteras;

    int next = 0;


    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (var gotera in goteras)
        {
            if (gotera.completed)
            {
                if (i > next)
                {
                    reset();
                    break;
                }
                else if (i == next) 
                {
                    ++next;
                }
            }
            ++i;
        }

        if (next == goteras.Length)
        {
            complete();
        }
    }

    void complete()
    {
        GameManager.GetInstance().CompletePuzzle(1);
        this.enabled = false;
    }

    void reset()
    {
        foreach (var gotera in goteras)
        {
            gotera.reset();
            next = 0;
        }
    }
}
