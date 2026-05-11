public class GameTipsManager : TipsManager
{
    private LocalDataManager dataManager;

    GameTipsManager(LocalDataManager dataManager) {
        this.dataManager = dataManager;
    }

    public void setTipWasShown(GameTipType tipType, bool wasShown) {
        UserTipsState state = dataManager.getUserTipsState();
        switch (tipType) {
            case GameTipType.parameterCalculatorTip: {
                state = state.copy(parameterCalculatorTipShown: wasShown);
                break;
            }
            case GameTipType.foodCollectionTip: {
                state = state.copy(foodCollectionTipShown: wasShown);
                break;
            }
            case GameTipType.fuelCollectionTip: {
                state = state.copy(fuelCollectionTipShown: wasShown);
                break;
            }
            case GameTipType.armorCollectionTip: {
                state = state.copy(armorCollectionTipShown: wasShown);
                break;
            }
            case GameTipType.bypassSchemaTip: {
                state = state.copy(bypassSchemaTipShown: wasShown);
                break;
            }
            case GameTipType.socobanTip: {
                state = state.copy(socobanTipShown: wasShown);
                break;
            }
        }        
        dataManager.saveUserTipsState(state);
    }

    public bool tipWasShown(GameTipType tipType) {
        UserTipsState state = dataManager.getUserTipsState();
        switch (tipType) {
            case GameTipType.armorCollectionTip: {
                return state.armorCollectionTipShown;
            }                
            case GameTipType.parameterCalculatorTip: {                
                return state.parameterCalculatorTipShown;
            }
            case GameTipType.foodCollectionTip: {
                return state.foodCollectionTipShown;
            }
            case GameTipType.fuelCollectionTip: {
                return state.fuelCollectionTipShown;
            }
            case GameTipType.bypassSchemaTip: {
                return state.bypassSchemaTipShown;
            }
            case GameTipType.socobanTip: {
                return state.socobanTipShown;
            }
            default: {
                return false;
            }
        }
    }
}
