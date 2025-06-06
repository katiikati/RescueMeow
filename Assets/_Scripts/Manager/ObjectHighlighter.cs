using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    private InteractableObject currentHover;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                if (currentHover != interactable)
                {
                    if (currentHover != null) currentHover.OnMouseExit();
                    currentHover = interactable;
                    currentHover.OnMouseEnter();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    currentHover.OnMouseDown();
                }
            }
            else
            {
                if (currentHover != null)
                {
                    currentHover.OnMouseExit();
                    currentHover = null;
                }
            }
        }
        else
        {
            if (currentHover != null)
            {
                currentHover.OnMouseExit();
                currentHover = null;
            }
        }
    }
}
