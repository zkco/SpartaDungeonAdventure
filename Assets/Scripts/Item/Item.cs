using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData ItemData;
    public void UseItem()
    {
        switch(ItemData.ItemType)
        {
            case (ItemType.Temp):
                break;

        }
    }
}