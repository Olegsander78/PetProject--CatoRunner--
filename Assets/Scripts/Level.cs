using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public const float LENGHT_SEGMENT = 30.7f;
    [SerializeField] private List<LevelSegment> _levelSegments = new List<LevelSegment>();
    [SerializeField] private float _startSpeedLevel = 3f;
    [SerializeField] private float _speedLevel;
    [SerializeField] private float _minCoordLevelSegX;
    [SerializeField] private float _maxCoordLevelSegX;
    [SerializeField] private int _numberLevel;
    [SerializeField] private BGMusic.MusicType _bgMusicType;

    public float MinCoordLevelSegX { get => _minCoordLevelSegX; set => _minCoordLevelSegX = value; }

    public float MaxCoordLevelSegX { get => _maxCoordLevelSegX; set => _maxCoordLevelSegX = value; }
    public float StartSpeedLevel => _startSpeedLevel; 
    public float SpeedLevel { get => _speedLevel; set => _speedLevel = value; }

    public int NumberLevel => _numberLevel;

    [SerializeField] private PlayerCharacter _playerCharacter;
    public PlayerCharacter PlayerCharacter => _playerCharacter;

    [SerializeField] private Quest _quest;

    public BGMusic.MusicType BGMusicType => _bgMusicType;

    private void Awake()
    {
        SpeedLevel = StartSpeedLevel;
        
        MinCoordLevelSegX = _levelSegments[0].transform.position.x;
        MaxCoordLevelSegX = _levelSegments[0].transform.position.x;

        for (int i = 1; i < _levelSegments.Count; i++)
        {
            if (MinCoordLevelSegX > _levelSegments[i].transform.position.x) MinCoordLevelSegX = _levelSegments[i].transform.position.x;
            if (MaxCoordLevelSegX < _levelSegments[i].transform.position.x) MaxCoordLevelSegX = _levelSegments[i].transform.position.x;
        }

        GameController.Instance.LevelController.SetLevel(this);
        _quest.BeginQuest();
        GameController.Instance.SoundController.StopBGMusic();
        GameController.Instance.SoundController.PlayBGMusic(GameController.Instance.LevelController.CurrentLevel);
    }
    private void FixedUpdate()
    {
        float displacement = SpeedLevel * Time.deltaTime;

        foreach (var segment in _levelSegments)
        {
            var pos = segment.transform.position;
            pos.x -= displacement;
            if (pos.x < MinCoordLevelSegX)
            {
                pos.x = MaxCoordLevelSegX - (MinCoordLevelSegX - pos.x) + LENGHT_SEGMENT;
                //segment.Clear();
                segment.RollVariantLevelSegment();
                GameController.Instance.EventBus.OnLevelSegmentFinishted(segment);
            }
            segment.Rigidbody.MovePosition(pos);
        }
        if (_quest.IsQuestFinisheted())
        {
            GameController.Instance.ScreenController.PushScreen<WinScreen>();
            //music
        }
    } 
    
    public void ChangeSpeedLevel(float speedUp)
    {
        SpeedLevel *= speedUp;
    }
}
