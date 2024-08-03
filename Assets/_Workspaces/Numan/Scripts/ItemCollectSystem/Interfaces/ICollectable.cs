public interface ICollectable
{
    public void TouchEnter(CharacterInventory characterInventory);
    public void TouchExit(CharacterInventory characterInventory);
    public void Collect();
}
