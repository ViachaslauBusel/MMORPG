using Items;
using RUCP;

public class CharacterArmor : Armor
{
    public void Initialize(Packet packet)
    {
        Initialize();


        for (int i = 0; i < 7; i++)
        {
            UpdateArmor(packet);
        }
    }

    public void UpdateArmor(Packet packet)
    {
        ItemType type = (ItemType)packet.ReadInt();
        ArmorPart part = (ArmorPart)packet.ReadInt();
        int id_item = packet.ReadInt();

        Item _item = Inventory.CreateItem(id_item);

        PutItem(type, _item);
    }
}
