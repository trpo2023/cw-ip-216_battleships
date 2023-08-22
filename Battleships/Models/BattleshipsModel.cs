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
        if (fieldType == FieldType.Player)
            return _playerField.GetField();
        else
        {
            TileState[,] field = new TileState[10, 10];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (_enemyField.GetField()[i, j] == TileState.Ship)
                        field[i, j] = TileState.Empty;
                    else
                        field[i, j] = _enemyField.GetField()[i, j];
            return field;
        }
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
