using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public void OnClickCharacte1()
    {
        SceneManagerEX.Instance.selectChar = SceneManagerEX.Instance.CharacterPrefab[0];
        SceneManagerEX.Instance.LoadStage1();
    }
    public void OnClickCharacte2()
    {
        SceneManagerEX.Instance.selectChar = SceneManagerEX.Instance.CharacterPrefab[1];
        SceneManagerEX.Instance.LoadStage1();
    }
    public void OnClickCharacte3()
    {
        SceneManagerEX.Instance.selectChar = SceneManagerEX.Instance.CharacterPrefab[2];
        SceneManagerEX.Instance.LoadStage1();
    }
}
