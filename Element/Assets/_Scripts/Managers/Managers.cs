using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
  void Awake()
  {
    if (FindObjectsOfType(typeof(Managers)).Length > 1)
    {
      Debug.LogWarning("Destroyed managers object");
      Destroy(this.gameObject);
    }

    DontDestroyOnLoad(this.gameObject);
  }
}
