using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressstart : MonoBehaviour
{
    public GameObject Screen;
    public void pressed ()
    {
        Screen.SetActive(true);
    }

}
