using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform characterContainer;
    private Animator animator;
    private void Start()
    {
        for (int i = 0; i < characterContainer.childCount; i++)
        {
            Transform child = characterContainer.GetChild(i);
            if (child.gameObject.TryGetComponent<Animator>(out animator) && child.gameObject.GetComponent<CharacterConfig>() != null)
            {
                break;
            }
        }
    }
    public void SetFloat(string name, float value)
    {
        animator.SetFloat(name, value);
    }
    public void SetTrigger(string name)
    {
        animator.SetTrigger(name);
    }
    public void SetBool(string name, bool value)
    {
        animator.SetBool(name, value);
    }
    public void SetInteger(string name, int value)
    {
        animator.SetInteger(name, value);
    }
}
