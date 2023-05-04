using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime.OUUN._2DTestProject
{
    public class GameManager : MonoBehaviour
    {
        // Singleton
        public static GameManager Instance = null;
        
        public bool isGameOver = false;

        [SerializeField] private TextMeshProUGUI coin;
        [SerializeField] private TextMeshProUGUI hp;
        [SerializeField] private TextMeshProUGUI lv;
        [SerializeField] private TextMeshProUGUI stats;
        [SerializeField] private GameObject gameOver;
        [SerializeField] private TextMeshProUGUI win;
        [SerializeField] private TextMeshProUGUI score;
        
        [SerializeField] private Player player;
        [SerializeField] private EnemySpawn enemySpawn;

        private int _coin = 0;
        private float _playerHp = 100f;
        private int _level = 1;
        private readonly int _maxLevel = 10;

        private int _stage;
        private int _stageMax = 3;
        
        private Camera _camera;
        private Vector2 _viewportSize;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            
            LoadCurrentViewport();
        }

        private void Update()
        {
            coin.SetText(_coin.ToString());
            hp.SetText("HP: " + ((int)_playerHp).ToString());
            lv.SetText("Lv." + _level.ToString());
            if (_level == _maxLevel)
            {
                lv.SetText("lv.Max");
            }
            stats.SetText(
                $"Damage: {player.bulletDamage}\n" +
                $"Bullet Speed: {player.bulletSpeed}\n" +
                $"Reload: {player.reloadingTime:0.00}s\n" +
                $"Move Speed: {player.speed:0.00}\n" +
                $"Invulnerable: {player.invulnerableTime:0.00}s"
            );

            if (CheckUpgrade())
            {
                var bulletUpgrade = _level % 3 == 1 & _level != 1;
                player.Upgrade(bulletUpgrade);
                _level++;
            }
        }

        public void AddCoin(int amount)
        {
            _coin += amount;
        }

        public float ChangePlayerHp(float amount)
        {
            _playerHp += amount;
            if (_playerHp <= 0)
            {
                _playerHp = 0;
            }
            
            return _playerHp;
        }
        
        public Camera GetCamera()
        {
            return _camera;
        }
        
        public Vector2 GetViewportSize()
        {
            return _viewportSize;
        }

        public void SetGameOver(bool won)
        {
            enemySpawn.StopEnemyRoutine();
            isGameOver = true;
            win.SetText(won ? "You Win!" : "Game Over");
            score.SetText(_coin.ToString());
            Invoke(nameof(ShowGameOver), 1f);
        }
        
        public void Retry()
        {
            SceneManager.LoadScene("Stage");
        }

        public void NextStage()
        {
            _stage++;
        }

        public int GetStage()
        {
            return _stage;
        }

        public int GetMaxStage()
        {
            return _stageMax;
        }

        private void LoadCurrentViewport()
        {
            _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            var viewportHeight = _camera.orthographicSize;
            _viewportSize = new Vector2(viewportHeight * _camera.aspect, viewportHeight);
        }
        
        private bool CheckUpgrade()
        {
            if (_level >= _maxLevel) return false;
            
            var tmp = (int)(Mathf.Sqrt(_coin) / 7);
            return tmp == _level;
        }

        private void ShowGameOver()
        {
            gameOver.SetActive(true);
        }
    }
}