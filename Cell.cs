using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
  public Material MainMaterial;	// исходный материал (материал по умолчанию)
  public Material CanMaterial;
    public Material CanMaterial1;// материал, если можем строить (при наведении курсора на объект)
  public Material CantMaterial; // материал, если не можем строить (при наведении курсора на объект)
  public Material CantMaterial1;
    public Material CantMaterial2;
    public bool CanBuild;
    private Tower tower;
    public bool CanMod = false;// можем ли строить
  public GameObject TowerPrefab; // экземпляр башни
  private ResourceManager rm; // ссылка на скрипт "ResourceManager"

  void Start()
  {
    rm = FindObjectOfType<ResourceManager>(); // создаём указатель/ссылку на скрипт "ResourceManager"
  }

  private void OnMouseOver() // курсор находится над объектом (курсор наведён на объект)
  {
    if (CanBuild && rm.Gold < rm.TowerCost) // если строительство разрешено
    {
      GetComponent<Renderer>().material = CantMaterial1;
	  // "GetComponent<Renderer>.material" - ссылка на материал (свойство "Materials") кмп."Mesh Renderer"
    }
    else if (CanBuild)
    {
      GetComponent<Renderer>().material = CanMaterial;
    }
    else if (CanMod && rm.Gold < rm.TowerCostMod)
        {
            GetComponent<Renderer>().material = CantMaterial2;
        }
    else if (CanMod)
        {
            GetComponent<Renderer>().material = CanMaterial1;
        }
    else // если строительство не разрешено
    {
      GetComponent<Renderer>().material = CantMaterial;
    }
  }

  private void OnMouseExit() // курсор покидает объект, выходит за его границы
  {
    GetComponent<Renderer>().material = MainMaterial; // возвратить объекту исходный материал
  }

  private void OnMouseUp() // отжата кнопка мыши над ячейкой игрового поля
  {
    if (CanBuild && rm.Gold >= rm.TowerCost) // если строительство разрешено и хватает золота для строительства
    {
      tower = Instantiate (TowerPrefab, transform.position, Quaternion.Euler (0, Random.Range(0, 360), 0)).GetComponent<Tower>();
	  // создаём объект "TowerPrefab" в центре текущей ячейки с координатами "transform.position"
	  // "Quaternion.Euler" - поворот со случайной координатой "Random.Range(0,360)" по оси "Y"
	  // ".GetComponent<Tower>()" - ссылка на экземпляр башни "TowerPrefab" (т.к. "TowerPrefab" типа "GameObject")
	  CanBuild = false;
            CanMod = true;
            
      rm.BuildTower(); // уменьшаем значение золота
    }
    else if (CanBuild == false && CanMod && rm.Gold >= rm.TowerCostMod)
    {
            rm.BuildTowerMod();
            tower.Damage *= 2;
            tower.FireDelay /= 2;
            CanMod = false;
    }
  }
}