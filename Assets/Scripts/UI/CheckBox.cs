using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject selectedCheckBox;
    public GameObject unselectedCheckBox;

    public void setSelected(bool selected)
    {
        selectedCheckBox.SetActive(selected);
        unselectedCheckBox.SetActive(!selected);
    }
}
