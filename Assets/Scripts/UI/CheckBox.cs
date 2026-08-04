using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject selectedCheckBox;
    public GameObject unselectedCheckBox;
    public bool selected = false;

    public void Awake()
    {
        setSelected(selected);    
    }

    public void setSelected(bool selected)
    {
        this.selected = selected;
        
        selectedCheckBox.SetActive(selected);
        unselectedCheckBox.SetActive(!selected);
    }
}
