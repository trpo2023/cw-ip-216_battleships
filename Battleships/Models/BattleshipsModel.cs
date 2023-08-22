using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using Battleships.Models.Changes;
using Battleships.Models.Primitive;

namespace Battleships.Models;

public class BattleshipsModel
{
    private Battlefield _playerField = new();
    private Battlefield _enemyField = new();
    private bool _gameOver = false;

    public delegate void GameOverHandler(FieldType fieldType);
    public event GameOverHandler OnGameOver;

    public delegate void FieldsChangedHandler(FieldsChanges fieldsChanges);
    public event FieldsChangedHandler OnFieldsChanged;

    // return true when need additional player turn
    private bool PlayerShoot(Vector2i position)
    {
        if (_enemyField.TryShoot(position) == ShootResult.Miss)
            return false;
        else
            return true;
    }

    // return true when need additional enemy turn
    private bool EnemyShoot()
    {
        if (_playerField.ShootRandom() == ShootResult.Miss)
            return false;
        else
            return true;
    }

    public BattleshipsModel()
    {
        _playerField.OnFieldChanged += (List<TileChanges> changes) =>
        {
            OnFieldsChanged.Invoke(new FieldsChanges() { playerChanges = changes });
        };
        _playerField.OnGameOver += () =>
        {
            _gameOver = true;
            OnGameOver.Invoke(FieldType.Enemy);
        };

        _enemyField.OnFieldChanged += (List<TileChanges> changes) =>
        {
            OnFieldsChanged.Invoke(new FieldsChanges() { enemyChanges = changes });
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

    public void TryShoot(Vector2i position)
    {
        if (_gameOver)
            return;

        Console.WriteLine("Player");
        if (PlayerShoot(position))
        {
            return;
        }

        if (_gameOver)
            return;
        Console.WriteLine("Enemy");
        while (true)
        {
            bool needTurn = EnemyShoot();
            if (!needTurn)
                break;
        }
    }
}
