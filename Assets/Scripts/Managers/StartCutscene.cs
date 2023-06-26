using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(CutSceneCoroutine(collision));
           
        }
    }
    IEnumerator CutSceneCoroutine(Collider2D collider)
    {
        collider.GetComponent<PlayerControllerChat>().Freeze(true);
        animator.SetBool("CutScene1", true);
        yield return new WaitForSeconds(5);
        collider.GetComponent<PlayerControllerChat>().Freeze(false);
        animator.SetBool("CutScene1", false);
        Destroy(gameObject);
    }
}
