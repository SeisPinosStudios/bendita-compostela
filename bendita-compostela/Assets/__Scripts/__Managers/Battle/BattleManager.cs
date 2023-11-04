using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [field: SerializeField, Header("Battle Setup")] public CombatData combatData { get; private set; }
    [SerializeField] EntityDataContainer entityDataContainer;
    BattleManagerState state;
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
    
    public event Action OnBattleEnd, OnEnemiesGenerated;

    private void Awake()
    {
        Instance = this;

        ChangeBattleManagerState(BattleManagerState.GeneratingEnemies);
    }
    private void OnDestroy() {
        RemoveListeners();
    }

    private void SetGameMusic() 
    {
        //soundList.musicList.Add(combatData.combatMusic);
        if (combatData.combatMusic.soundName == "")
        {
            soundList.PlayMusic("Combat");
            return;
        }
        else
        {
            SoundManager.Instance.PlayMusic(combatData.combatMusic);
        }
 
    }

    #region Initilization
    private IEnumerator GenerateEnemies() {

        /*  Calculate the needed points and spaces to spawn the enemies. The generation
            takes the two points that delimit the enemy spawn area, gets the middle point
            and distributes the enemies along that space, centered on the middle.   */
        float space = (Mathf.Abs(enemyAreaBegin.position.x) + Mathf.Abs(enemyAreaEnd.position.x)) / combatData.enemiesData.Count;
        var center = Mathf.Lerp(enemyAreaBegin.position.x, enemyAreaEnd.position.x, 0.5f);
        var startingPoint = center - space * ((combatData.enemiesData.Count - 1) / 2f);

        DebugManager.StaticDebug("System", $"Enemy generation | center point: {center} | start point: {startingPoint} | Info: {space}  {(combatData.enemiesData.Count - 1) / 2}");

        /*  Instantiate the prefab of the enemies and then add them to the enemy list. */
        for (int i = 0; i < combatData.enemiesData.Count; i++) {
            var position = new Vector3(startingPoint + space * i, enemyAreaBegin.position.y, 0.0f);
            entityDataContainer.entityData = combatData.enemiesData[i];
            enemies.Add(Instantiate(entityDataContainer, position, new Quaternion(), enemiesContainer).GetComponent<Enemy>());
            yield return null;
        }

        ChangeBattleManagerState(BattleManagerState.EnemiesGenerated);
        yield return null;
    }
    #endregion

    #region Check methods
    public List<Enemy> GetDamagedEnemies(float percentage)
    {
        var damagedEnemies = new List<Enemy>();
        foreach (Enemy enemy in enemies) if (enemy.IsDamaged(percentage)) damagedEnemies.Add(enemy);
        return damagedEnemies;
    }
    private void CheckGameEnd() {
        if (player.IsDead()) {
            endScreens[1].SetActive(true);
            return;
        }

        foreach (Enemy enemy in enemies) if (!enemy.IsDead()) return;
        endScreens[0].SetActive(true);
    }
    public void LoadScene(string scene) {
        SceneManager.LoadScene(scene);
    }
    public void SetInteraction(bool interaction) { blockingImage.SetActive(!interaction); }
    #endregion

    #region Listeners
    private void AddListeners() {
        foreach (Enemy enemy in enemies) enemy.OnDeath += CheckGameEnd;
        player.OnDeath += CheckGameEnd;
    }
    private void RemoveListeners() {
        foreach (Enemy enemy in enemies) enemy.OnDeath -= CheckGameEnd;
        player.OnDeath -= CheckGameEnd;
    }
    #endregion

    private void ChangeBattleManagerState(BattleManagerState newState) {
        state = newState;
        switch (state) {
            case BattleManagerState.GeneratingEnemies:
                combatData = GameManager.Instance.combatData;
                StartCoroutine(GenerateEnemies());
                break;
            case BattleManagerState.EnemiesGenerated:
                AddListeners();
                OnEnemiesGenerated?.Invoke();
                break;
            case BattleManagerState.Battle:
                SetGameMusic();
                break;
            case BattleManagerState.BattleEnd:

                break;
        }
    }
}

public enum BattleManagerState {
    GeneratingEnemies,
    EnemiesGenerated,
    Battle,
    BattleEnd
}
