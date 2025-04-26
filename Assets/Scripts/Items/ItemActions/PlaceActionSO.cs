using UnityEngine;

[CreateAssetMenu(fileName = "PlaceActionSO", menuName = "Scriptable Objects/PlaceActionSO")]
public class PlaceActionSO : ItemActionSO
{

    public override bool CanExecute(GameObject owner, UsableItemSO item)
    {
        //Проверить что в рамках сетки.
        //! проверить что позиция последней клетки не слишком далеко (чтобы нечаянно не строить на другом конце карты если lastpos не обнулилась)
        return true;
    }

    public override void Cleanup(GameObject owner, UsableItemSO item)
    {

    }

    public override void Execute(GameObject owner, UsableItemSO usableItemSO)
    {
        PlaceableItemSO item = (PlaceableItemSO)usableItemSO;
        owner.GetComponent<PlayerBuild>().PlaceItemOnGrid(item);
    }

}
