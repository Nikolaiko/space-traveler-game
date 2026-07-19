using System.Collections.Generic;
using UnityEngine;

public class SocobanLevelManager : MonoBehaviour
{
    public string easyFileName;
    public string normalFileName;
    public string hardFileName;
    public List<SocobanLevel> easyLevels;
    public List<SocobanLevel> normalLevels;
    public List<SocobanLevel> hardLevels;
    
    public void Awake() {

        #region Easy Levels Parsing

        TextAsset text = (TextAsset)Resources.Load(easyFileName);
        if (!text) {
            Debug.Log("Levels file:" + easyFileName + ".txt does not exist!");
            return;
        } else {
            Debug.Log("Levels imported!");
        }

        string levelsText = text.text;
        string[] lines;

        lines = levelsText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        easyLevels.Add(new SocobanLevel());

        for (long i = 0; i < lines.LongLength; i++) {
            string line = lines[i];
            if (line.StartsWith(";")) {                
                easyLevels.Add(new SocobanLevel());
                continue;
            }
            easyLevels[easyLevels.Count - 1]._rows.Add(line);
        }

        #endregion

        #region Hard Levels Parsing

        text = (TextAsset)Resources.Load(hardFileName);
        if (!text) {
            Debug.Log("Levels file:" + hardFileName + ".txt does not exist!");
            return;
        } else {
            Debug.Log("Levels imported!");
        }

        levelsText = text.text;
        lines = levelsText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        hardLevels.Add(new SocobanLevel());

        for (long i = 0; i < lines.LongLength; i++) {
            string line = lines[i];
            if (line.StartsWith(";")) {
                Debug.Log("New level added");
                hardLevels.Add(new SocobanLevel());
                continue;
            }
            hardLevels[hardLevels.Count - 1]._rows.Add(line);
        }

        #endregion

        #region Normal Levels Parsing

        text = (TextAsset)Resources.Load(normalFileName);
        if (!text) {
            Debug.Log("Levels file:" + normalFileName + ".txt does not exist!");
            return;
        } else {
            Debug.Log("Levels imported!");
        }

        levelsText = text.text;
        lines = levelsText.Split(new string[] { "\n" }, System.StringSplitOptions.None);
        normalLevels.Add(new SocobanLevel());

        for (long i = 0; i < lines.LongLength; i++) {
            string line = lines[i];
            if (line.StartsWith(";")) {
                Debug.Log("New level added");
                normalLevels.Add(new SocobanLevel());
                continue;
            }
            normalLevels[normalLevels.Count - 1]._rows.Add(line);
        }

        #endregion
    }
}
