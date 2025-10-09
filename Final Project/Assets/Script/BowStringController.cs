using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BowStringController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;

    private XRGrabInteractable interactable;

    [SerializeField]
    private Transform midPointGrabObject;        // the cube you actually grab
    [SerializeField]
    private Transform midPointVisualObject;      // the small visual point that follows the grab cube

    private Transform interactor;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        interactable.selectEntered.AddListener(PrepareBowString);
        interactable.selectExited.AddListener(ResetBowString);
    }

    private void ResetBowString(SelectExitEventArgs arg0)
    {
        interactor = null;

        // Reset positions when you release the string
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;

        bowStringRenderer.CreateString(null);
    }

    private void PrepareBowString(SelectEnterEventArgs arg0)
    {
        interactor = arg0.interactorObject.transform;
    }

    private void Update()
    {
        if (interactor != null)
        {
            // Make visual midpoint follow the grab midpoint
            midPointVisualObject.position = midPointGrabObject.position;

            // Update the string using the visual midpoint
            bowStringRenderer.CreateString(midPointVisualObject.position);
        }
    }
}
