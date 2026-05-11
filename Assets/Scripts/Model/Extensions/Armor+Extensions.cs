namespace ShipArmorExtensions {
    public static class ShipArmorExtensions {
        private static readonly float bestArmorAmountPerUnit = 1.0f;
        private static readonly float normalArmorAmountPerUnit = 0.5f;
        private static readonly float worstArmorAmountPerUnit = 0.25f;

        public static float getArmorAmountPerUnit(this ArmorType armorType) {
            switch (armorType) {
                case ArmorType.best: {
                    return bestArmorAmountPerUnit;
                }
                case ArmorType.normal: {
                    return normalArmorAmountPerUnit;
                }
                case ArmorType.worst: {
                    return worstArmorAmountPerUnit;
                }
            }
            return worstArmorAmountPerUnit;
        }
    } 
}