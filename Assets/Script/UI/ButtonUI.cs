using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class ButtonUI : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] AudioSource speaker;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip entered;
    [SerializeField] AudioClip click;
    [SerializeField] AudioMixer mixer;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("mouseEnter", true);
        speaker.PlayOneShot(entered);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("mouseEnter", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        speaker.PlayOneShot(click);
    }
}
