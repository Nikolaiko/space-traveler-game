using System.ComponentModel;

[ImmutableObject(true)]
public struct UserTipsState
{
    public UserTipsState(
        bool parameterCalculatorTipShown = false,
        bool foodCollectionTipShown = false,
        bool fuelCollectionTipShown = false,
        bool armorCollectionTipShown = false,
        bool socobanTipShown = false,
        bool bypassSchemaTipShown = false
    ) {
        this.parameterCalculatorTipShown = parameterCalculatorTipShown;
        this.foodCollectionTipShown = foodCollectionTipShown;
        this.fuelCollectionTipShown = fuelCollectionTipShown;
        this.armorCollectionTipShown = armorCollectionTipShown;
        this.socobanTipShown = socobanTipShown;
        this.bypassSchemaTipShown = bypassSchemaTipShown;
    }

    public UserTipsState copy(
        bool? parameterCalculatorTipShown = null,
        bool? foodCollectionTipShown = null,
        bool? fuelCollectionTipShown = null,
        bool? armorCollectionTipShown = null,
        bool? socobanTipShown = null,
        bool? bypassSchemaTipShown = null
    ) {
        return new UserTipsState(
            foodCollectionTipShown: foodCollectionTipShown ?? this.foodCollectionTipShown,
            parameterCalculatorTipShown: parameterCalculatorTipShown ?? this.parameterCalculatorTipShown,
            fuelCollectionTipShown: fuelCollectionTipShown ?? this.fuelCollectionTipShown,
            armorCollectionTipShown: armorCollectionTipShown ?? this.armorCollectionTipShown,
            socobanTipShown: socobanTipShown ?? this.socobanTipShown,
            bypassSchemaTipShown: bypassSchemaTipShown ?? this.bypassSchemaTipShown
        );
    }

    public readonly bool parameterCalculatorTipShown;
    public readonly bool foodCollectionTipShown;
    public readonly bool fuelCollectionTipShown;
    public readonly bool armorCollectionTipShown;
    public readonly bool socobanTipShown;
    public readonly bool bypassSchemaTipShown;
}
