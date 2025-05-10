using UnityEngine;

public class UIAnimation_MenuToGame : MonoBehaviour
{

    // turn objects on and off - page flip
    // start transition into game scene

    public GameObject pageAFront;

    void DeactivatePageAFront()
    {
        pageAFront.SetActive(false);
    }


    void ToScene_GameIntro()
    {


    }
}
