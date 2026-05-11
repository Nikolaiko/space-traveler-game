using UnityEngine;

public class TipsScreenUIFactory : MonoBehaviour
{
    public GameObject parameterCalculatorTip;
    public GameObject foodCollectionTip;
    public GameObject fuelCollectionTip;
    public GameObject armorCollectionTip;
    public GameObject socobanTip;
    public GameObject bypassSchemaTip;
    
    public TipsScreenUI buildTipScreen(GameTipType type) {
        switch (type)  {
            case GameTipType.parameterCalculatorTip: {
                GameObject screenObject = Instantiate(parameterCalculatorTip);
                return screenObject.GetComponent<TipsScreenUI>();
            }
            case GameTipType.foodCollectionTip: {
                GameObject screenObject = Instantiate(foodCollectionTip);
                return screenObject.GetComponent<TipsScreenUI>();
            }
            case GameTipType.fuelCollectionTip: {
                GameObject screenObject = Instantiate(fuelCollectionTip);
                return screenObject.GetComponent<TipsScreenUI>();
            }
            case GameTipType.armorCollectionTip: {
                GameObject screenObject = Instantiate(armorCollectionTip);
                return screenObject.GetComponent<TipsScreenUI>();
            }
            case GameTipType.socobanTip: {
                GameObject screenObject = Instantiate(socobanTip);
                return screenObject.GetComponent<TipsScreenUI>();
            }
            case GameTipType.bypassSchemaTip: {
                GameObject screenObject = Instantiate(bypassSchemaTip);
                return screenObject.GetComponent<TipsScreenUI>();
            }
            default: {
                return null;
            }
        }
    }
}
