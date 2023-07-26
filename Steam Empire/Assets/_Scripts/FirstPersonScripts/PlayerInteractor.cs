using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] CustomInput input;
    [SerializeField] Camera camera;
    [SerializeField] float interactDistance = 1.5f;
    [SerializeField] TextMeshProUGUI interactTxt;

    bool canInteract = false;

    void Update()
    {
        canInteract = false;

        string _interactMsg = string.Empty;

        RaycastHit hit;
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            Interactable _interactable = hit.transform.GetComponent<Interactable>();
            canInteract = _interactable != null;

            if (canInteract)
            {
                _interactMsg = _interactable.interactMessage != string.Empty ? _interactable.interactMessage + " [E]" : "Interact [E]";


                if (input.isInteracting)
                {
                    _interactable.Interact();
                }
            }
        }

        if (interactTxt == null) return;

        interactTxt.text = _interactMsg;
    }
}
