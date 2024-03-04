using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;
    public string speechtxt;
    public string actorname;

    private DialogueControl dc;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
    }

    public void Interact()
    {
    }

}
