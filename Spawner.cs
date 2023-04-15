using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  public Enemy EnemyPrefab;
  public Enemy1 EnemyPrefab1;
  public float TimeToSpawn; // частота (промежуток времени) создания новых юнитов
  public Transform[] Points;
    public Transform[] Points1;// массив точек движения юнита
    private float timer;
 
  public float MainHP, IncreaseHP; // основное и дельта здоровья для следующих юнитов
  public float MainHP1, IncreaseHP1;

    void Start()
  {
    timer = TimeToSpawn; // время создания 1-го юнита
  }
    
  void Update()
  {
    timer -= Time.deltaTime; // из "timer" вычитаем время рендеринга/отрисовки

    if (timer <= 0)
    {
      
               
                
      Enemy enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
      enemy.Points = Points;
      enemy.HP = MainHP;
            // создаём объект (новый юнит)
            // "EnemyPrefab" - что создаём/клонируем (исходный объект)
            // "transform.position" - где создаём, в какой позиции (координаты об."Spawner")
            // "Quaternion.identity" - как повёрнут, какой поворот/разворот кватерниона (поворот прф."Enemy")
            // передаём созданному объекту (новому юниту) массив точек маршрута движения

      Enemy1 enemy1 = Instantiate(EnemyPrefab1, transform.position, Quaternion.identity);
      enemy1.Points = Points1;
      enemy1.HP = MainHP1;

            
      TimeToSpawn -= 0.1f;
      timer = TimeToSpawn; // восстанавливаем время создания новых юнитов

      // реализация увеличения здоровья с новым юнитом
       // передаём "новое" здоровье экз.прф."Enemy"
      MainHP += IncreaseHP;
      MainHP1 += IncreaseHP1;// увеличиваем здоровье для каждого нового юнита
    }
  }
}