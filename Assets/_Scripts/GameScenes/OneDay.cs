using UnityEngine;

public class OneDay : MonoBehaviour
{
    private void Update()
    {
        if (InputManager.GetInstance().GetInteractPressed())
            {
                ExternalFunctionHelper.GetInstance().blackOut();
            }
    }
}
