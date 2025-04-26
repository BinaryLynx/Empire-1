using UnityEngine;

[CreateAssetMenu(fileName = "PreviewPlacementActionSO", menuName = "Scriptable Objects/PreviewPlacementActionSO")]
public class PreviewPlacementActionSO : ItemActionSO
{
    public override bool CanExecute(GameObject owner, UsableItemSO item)
    {
        return true;
    }

    public override void Cleanup(GameObject owner, UsableItemSO item)
    {
        //Когда переключается предмет отключить предпросмотр старого предмета?
    }

    public override void Execute(GameObject owner, UsableItemSO usableItemSO)
    {
        PlaceableItemSO item = (PlaceableItemSO)usableItemSO;
        owner.GetComponent<PlayerBuild>().UpdatePreview(item.previewPrefab);
    }
}
