using UnityEngine;
using Ink.Runtime;

public class InkExternalFunctions
{
    public void Bind(Story story, ExternalFunctionHelper externalFunctionHelper)
    {
        story.BindExternalFunction("nextScene", () => externalFunctionHelper.nextScene());
        story.BindExternalFunction("blackOut", () => externalFunctionHelper.blackOut());
        story.BindExternalFunction("nameCat", () => externalFunctionHelper.nameCat());
    }


    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("nextScene");
        story.UnbindExternalFunction("blackOut");
        story.UnbindExternalFunction("nameCat");
    }

    public void playEmote(string emoteName, Animator emoteAnimator)
    {
        Debug.Log("playing emote");
        if (emoteAnimator != null)
        {
            emoteAnimator.Play(emoteName);
        }
        else
        {
            Debug.LogWarning("Tried to play emote, but emote animator was "
                + "not initialized when entering dialogue mode.");
        }
    }

}