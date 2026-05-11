namespace Assets.Scripts
{
    public class ItemMovementDetails
    {
        public int NewRow { get; set; }
        public int NewColumn { get; set; }

        public ItemMovementDetails(
            int newRow,
            int newColumn) {
            NewRow = newRow;
            NewColumn = newColumn;            
        }
    }
}
