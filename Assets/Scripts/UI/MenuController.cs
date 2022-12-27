using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Initialize inactive menu.
    public virtual void SetUp()
    {
        

    }

    public virtual void Show(CharacterController invoker)
    {
        this.gameObject.SetActive(true);
        // Lock player interaction to this.
        invoker.GetComponent<Awareness>().enabled = false;
        invoker.GetComponent<CharacterMovement>().enabled = false;
        //invoker.GetComponent<InteractionController>().Target = this;
        //invoker.GetComponent<PlayerControls>().Input.Menu.Escape.started += _ => Hide(invoker);
    }

    public virtual void Hide(CharacterController invoker)
    {
        this.gameObject.SetActive(false);
        invoker.GetComponent<Awareness>().enabled = true;
        invoker.GetComponent<CharacterMovement>().enabled = true;
    }
}
