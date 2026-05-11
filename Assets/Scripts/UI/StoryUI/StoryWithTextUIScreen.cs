using System;
using TMPro;

public class StoryWithTextUIScreen : StoryUIScreen
{ 
    public String[] storyPhrases;
    public TMP_Text storyText;

    override protected void updateComixFrame()
    {
        base.updateComixFrame();
        storyText.text = storyPhrases[currentFrame];
    }
}
