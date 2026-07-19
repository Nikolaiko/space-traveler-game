using System.ComponentModel;

[ImmutableObject(true)]
public struct SpaceShipState
{
    public SpaceShipState(
        DestinationPlanetType planetType,
        int? armorNeeded = null,
        int? foodNeeded = null,
        int? fuelNeeded = null,
        int foodCollected = 0,
        int fuelCollected = 0,
        int armorCollected = 0,
        int armorWeight = 0,
        int foodWeight = 0 
    ) {
        this.armorNeeded = armorNeeded;
        this.foodNeeded = foodNeeded;
        this.fuelNeeded = fuelNeeded;
        this.planetType = planetType;

        this.armorCollected = armorCollected;
        this.fuelCollected = fuelCollected;
        this.foodCollected = foodCollected;

        this.armorWeight = armorWeight;
        this.foodWeight = foodWeight;
    }

    public SpaceShipState copy(
        DestinationPlanetType? planetType = null,
        int? armorNeeded = null,
        int? foodNeeded = null,
        int? fuelNeeded = null,
        int? foodCollected = null,
        int? fuelCollected = null,
        int? armorCollected = null,
        int? armorWeight = null,
        int? foodWeight = null 
    ) {
        return new SpaceShipState(
            planetType: planetType ?? this.planetType,
            armorNeeded: armorNeeded ?? this.armorNeeded,
            foodNeeded: foodNeeded ?? this.foodNeeded,
            fuelNeeded: fuelNeeded ?? this.fuelNeeded,
            foodCollected:foodCollected ?? this.foodCollected,
            armorCollected:armorCollected ?? this.armorCollected,
            fuelCollected: fuelCollected ?? this.fuelCollected,
            armorWeight: armorWeight ?? this.armorWeight,
            foodWeight: foodWeight ?? this.foodWeight            
        );
    }

    public CollectionState armorCollectionState() {        
        return armorCollected >= armorNeeded ? CollectionState.collected : CollectionState.inProgress;
    }

    public CollectionState foodCollectionState() {
        return foodCollected >= foodNeeded ? CollectionState.collected : CollectionState.inProgress;
    }

    public CollectionState fuelCollectionState() {
        return fuelCollected >= fuelNeeded ? CollectionState.collected : CollectionState.inProgress;
    }

    public bool readyToFly() {        
        return foodCollectionState() == CollectionState.collected
            && fuelCollectionState() == CollectionState.collected;
    }

    public readonly int? armorNeeded;
    public readonly int? foodNeeded;
    public readonly int? fuelNeeded;
    public readonly int fuelCollected;
    public readonly int foodCollected;
    public readonly int foodWeight;
    public readonly int armorCollected;
    public readonly int armorWeight;
    public readonly DestinationPlanetType planetType;
}
