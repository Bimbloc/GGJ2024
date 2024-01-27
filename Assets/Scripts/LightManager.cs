using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] private GameObject luz1;
    [SerializeField] private GameObject luz2;
    [SerializeField] private GameObject luz3;

    private Stack<GameObject> stack = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().setLightManager(this);

        stack.Push(luz3);
        stack.Push(luz2);
        stack.Push(luz1);
        luz2.SetActive(false);
        luz3.SetActive(false);
    }

    public void CompletePuzzle()
    {
        if (stack.Count > 1)
        {
            stack.Pop();
            stack.Peek().SetActive(true);
        }
    }
}
