using System.Collections.Generic;

using Battleships.Models.Changes;
using Battleships.Models.Primitive;

namespace Battleships.Models;

public class BattleshipsModel
{
    private Battlefield _playerField = new();
    private Battlefield _enemyField = new();
    private bool _gameOver = false;

    private FieldsChanges _currentChanges;

    public delegate void GameOverHandler(FieldType fieldType);
    public event GameOverHandler OnGameOver;

    public delegate void FieldsChangedHandler(FieldsChanges fieldsChanges);
    public event FieldsChangedHandler OnFieldsChanged;

    public BattleshipsModel()
    {
        _playerField.OnFieldChanged += (List<TileChanges> changes) =>
        {
            _currentChanges.playerChanges = changes;
        };
        _playerField.OnGameOver += () =>
        {
            _gameOver = true;
            OnGameOver.Invoke(FieldType.Enemy);
        };

        _enemyField.OnFieldChanged += (List<TileChanges> changes) =>
        {
            _currentChanges.enemyChanges = changes;
        };
        _enemyField.OnGameOver += () =>
        {
            _gameOver = true;
            OnGameOver.Invoke(FieldType.Player);
        };
    }

    public TileState[,] GetTileField(FieldType fieldType)
    {
        return fieldType switch
        {
            FieldType.Player => _playerField.GetField(),
            FieldType.Enemy => _enemyField.GetField(),
            _ => null,
        };
    }

    public bool TryShoot(Vector2i position)
    {
        if (_gameOver)
            return false;
        if (!_enemyField.TryShoot(position))
            return false;

        if (_gameOver)
            return false;
        _playerField.ShootRandom();
        OnFieldsChanged.Invoke(_currentChanges);
        return true;
    }
}
