using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [field: SerializeField, Header("Battle Setup")] public CombatData combatData { get; private set; }
    [SerializeField] EntityDataContainer entityDataContainer;

    [field: SerializeField, Header("Enviroment Variables")] public Transform enemiesContainer { get; private set; }
    [field: SerializeField] public static BattleManager Instance { get; private set; }
    [field: SerializeField] public Player player { get; private set; }
    [field: SerializeField] public List<Enemy> enemies { get; private set; }
    [field: SerializeField] public Transform enemyAreaBegin { get; private set; }
    [field: SerializeField] public Transform enemyAreaEnd { get; private set; }
    [field: SerializeField] public List<GameObject> endScreens { get; private set; }

    [field: SerializeField] public SoundList soundList { get; private set; }

    [field: SerializeField] public GameObject blockingImage { get; private set; }
    /*====EVENTS====*/
    
    public event Action OnBattleEnd = delegate {};

    private void Awake()
    {
        Instance = this;  

        combatData = GameManager.Instance.combatData;
        SetGameMusic();

        GenerateEnemies();

        foreach (Transform child in enemiesContainer) enemies.Add(child.GetComponent<Enemy>());
        foreach (Enemy enemy in enemies) enemy.OnDeath += CheckGameEnd;
        player.OnDeath += CheckGameEnd;

        
    }
    private void GenerateEnemies()
    {
        float space = (Mathf.Abs(enemyAreaBegin.position.x) + Mathf.Abs(enemyAreaEnd.position.x)) / combatData.enemiesData.Count;

        var center = Mathf.Lerp(enemyAreaBegin.position.x, enemyAreaEnd.position.x, 0.5f);

        var startingPoint = center - space * ((combatData.enemiesData.Count - 1) / 2f);

        Debug.Log($"Enemy generation | center point: {center} | start point: {startingPoint} | Info: {space}  {(combatData.enemiesData.Count - 1)/2}");


        for(int i = 0; i < combatData.enemiesData.Count; i++)
        {
            var position = new Vector3(startingPoint + space * i, enemyAreaBegin.position.y, 0.0f);
            entityDataContainer.entityData = combatData.enemiesData[i];
            Instantiate(entityDataContainer, position, new Quaternion(), enemiesContainer);
        }
    }

    private void SetGameMusic() 
    {
        //soundList.musicList.Add(combatData.combatMusic);
        if (combatData.combatMusic == null)
        {
            soundList.PlayMusic("Combat");
            return;
        }
        else
        {
            Debug.Log(combatData.combatMusic.soundName);
            SoundManager.Instance.PlayMusic(combatData.combatMusic);
            
        }
 
    }

    #region Check methods
    public List<Enemy> GetDamagedEnemies(float percentage)
    {
        var damagedEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemies) if (enemy.IsDamaged(percentage)) damagedEnemies.Add(enemy);
        return damagedEnemies;
    }
    #endregion

    #region Game State Methods
    private void CheckGameEnd()
    {
        if (player.IsDead())
        {
            endScreens[1].SetActive(true);
            return;
        }

        foreach (Enemy enemy in enemies) if (!enemy.IsDead()) return;
        endScreens[0].SetActive(true);
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void SetInteraction(bool interaction) { blockingImage.SetActive(!interaction); }
    #endregion

    private void OnDestroy()
    {
        foreach (Enemy enemy in enemies) enemy.OnDeath -= CheckGameEnd;
        player.OnDeath -= CheckGameEnd;
    }
}
