using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
  // Changes scene to parameter specified.
  public void changeScene(string sceneTo)
  {
    SceneManager.LoadScene(sceneTo);
  }
}
