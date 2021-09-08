using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class KnapsackManager : MonoBehaviour
{
    Dictionary<int, Item> allItems = new Dictionary<int, Item>();
    [SerializeField] KnapsackPanelView _knapsackPanelView;
    [SerializeField] TipsView tipsView;
    [SerializeField] DragItemView _dragItemView;
    bool _isShowTips;
    bool _isDrag;
    RectTransform _canvasRect;
    Camera _uiCamera;
    GameObject _dragGameObject;
    private void Awake()
    {
        LoadData();

        GridView.OnEnter += GridView_OnEnter;
        GridView.OnExit += GridView_OnExit;
        GridView.OnLeftBeginDrag += GridView_OnLeftBeginDrag;
        GridView.OnLeftEndDrag += GridView_OnLeftEndDrag;
    }


    private void GridView_OnLeftBeginDrag(GridView dragGrid)
    {
        if (dragGrid.transform.childCount == 0) return;
        Item data = ItemModel.GetItem(dragGrid.GridID);
        if (data != null)
        {
            _dragItemView.SetDragInfo(data.Name);
            _dragGameObject = dragGrid.transform.GetChild(0).gameObject;
            _dragGameObject.SetActive(false);
            _dragItemView.Show();
            _isDrag = true;
        }
    }

    private void GridView_OnLeftEndDrag(GridView dragGrid, GridView targetGrid)
    {
        if(targetGrid != null)
        {
            Item dragItemData = ItemModel.GetItem(dragGrid.GridID);
            Item targetItemData = ItemModel.GetItem(targetGrid.GridID);
            if (targetGrid.transform.childCount == 0)
            { // 目标是空格子
                ItemView itemView = _dragGameObject.GetComponent<ItemView>();
                itemView.transform.parent = targetGrid.transform;
                itemView.transform.localPosition = Vector3.zero;
                itemView.UpdateItemName(dragItemData.Name);
                ItemModel.Add(targetGrid.GridID, dragItemData);
            }
            else
            { //交换
                ItemView dragGridView = dragGrid.transform.GetChild(0).GetComponent<ItemView>();
                ItemView targetGridView = targetGrid.transform.GetChild(0).GetComponent<ItemView>();
                dragGridView.UpdateItemName(targetItemData.Name);
                targetGridView.UpdateItemName(dragItemData.Name);
                ItemModel.Add(targetGrid.GridID, dragItemData);
                ItemModel.Add(dragGrid.GridID, targetItemData);
            }
        }
        _dragItemView.Hide();
        _dragGameObject.SetActive(true);
        _dragGameObject = null;
        _isDrag = false;
        // 
        _isShowTips = false;
        tipsView.ShowOrHideTips(Vector3.zero, false);
    }

    private void Start()
    {
        _canvasRect = transform.GetComponent<RectTransform>();
        _uiCamera = transform.Find("UICamera").GetComponent<Camera>();
    }

    private void GridView_OnExit()
    {
        _isShowTips = false;
        tipsView.ShowOrHideTips(Vector3.zero, false);
    }

    private void GridView_OnEnter(int gridID)
    {
        Item item = ItemModel.GetItem(gridID);
        if (item == null) return;
        //
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<color=red>{0}</color>\n\n", item.Name);
        if (item is Weapon weapon)
        {
            sb.AppendFormat("攻击:{0}\n\n", weapon.Damage);
        }
        else if (item is Armor armor)
        {
            sb.AppendFormat("防御:{0}\n\n", armor.Defend);
        }
        else if (item is Consumable consumable)
        {
            sb.AppendFormat("HP:{0}\nMP:{1}\n\n", consumable.BackHp, consumable.BackMp);
        }
        sb.AppendFormat("<size=25><color=white>购买价格：{0}\n出售价格：{1}</color></size>\n\n<color=yellow><size=20>描述：{2}</size></color>", item.BuyPrice, item.SellPrice, item.Desc);

        tipsView.UpdateTipsContent(sb.ToString());
        _isShowTips = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int index = Random.Range(0, 10);
            GetItem(index);
        }

        if (_isDrag)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, _uiCamera, out Vector2 uguiPos);
            _dragItemView.SetPosition(uguiPos);
        }else if (_isShowTips)
        { //拖拽时不显示Tips
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, _uiCamera, out Vector2 uguiPos);
            tipsView.ShowOrHideTips(uguiPos, true);
        }
    }

    /// <summary>
    /// 获得物品
    /// </summary>
    public void GetItem(int id)
    {
        if (!allItems.ContainsKey(id)) return;
        GridView grid = _knapsackPanelView.GetEmptyGrid();
        if (grid == null)
        {
            Debug.LogError("背包已满");
        }

        Item itemData = allItems[id];

        ItemModel.Add(grid.GridID, itemData);
        ItemView itemView = CreateItem(grid.transform);
        itemView.UpdateItemName(itemData.Name);
    }

    void LoadData()
    {
        Weapon w1 = new Weapon(0, "牛刀", "牛B的刀！", 20, 10, 100);
        Weapon w2 = new Weapon(1, "羊刀", "杀羊刀。", 15, 10, 20);
        Weapon w3 = new Weapon(2, "宝剑", "大宝剑！", 120, 50, 500);
        Weapon w4 = new Weapon(3, "军枪", "可以对敌人射击，很厉害的一把枪。", 1500, 125, 720);

        Consumable c1 = new Consumable(4, "红瓶", "加血", 25, 11, 20, 0);
        Consumable c2 = new Consumable(5, "蓝瓶", "加蓝", 39, 19, 0, 20);

        Armor a1 = new Armor(6, "头盔", "保护脑袋！", 128, 83, 40);
        Armor a2 = new Armor(7, "护肩", "上古护肩，锈迹斑斑。", 1000, 500, 50);
        Armor a3 = new Armor(8, "胸甲", "皇上御赐胸甲。", 153, 10, 60);
        Armor a4 = new Armor(9, "护腿", "预防风寒，从腿做起", 999, 60, 70);

        allItems.Add(w1.Id, w1);
        allItems.Add(w2.Id, w2);
        allItems.Add(w3.Id, w3);
        allItems.Add(w4.Id, w4);
        allItems.Add(c1.Id, c1);
        allItems.Add(c2.Id, c2);
        allItems.Add(a1.Id, a1);
        allItems.Add(a2.Id, a2);
        allItems.Add(a3.Id, a3);
        allItems.Add(a4.Id, a4);
    }

    ItemView CreateItem(Transform parent)
    {
        GameObject itemViewPrefab = Resources.Load<GameObject>("Item");
        ItemView itemView = Instantiate(itemViewPrefab, parent).GetComponent<ItemView>();
        itemView.transform.localPosition = Vector3.zero;
        return itemView;
    }
}
