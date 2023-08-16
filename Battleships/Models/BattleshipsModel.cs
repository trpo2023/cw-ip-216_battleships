using System.Collections.Generic;

using Battleships.Models.Changes;
using Battleships.Models.Primitive;

namespace Battleships.Models;
public class BattleshipsModel
{
    private Battlefield _playerField;
    private Battlefield _enemyField;

    private FieldsChanges _currentChanges;

    public delegate void GameOverHandler(bool isPlayerWin);
    public event GameOverHandler OnGameOver;

    public delegate void FieldsChangedHandler(FieldsChanges fieldsChanges);
    public event FieldsChangedHandler OnFieldsChanged;
    public BattleshipsModel()
    {
        _playerField = new();
        _enemyField = new();

        _playerField.OnFieldChanged += (List<TileChanges> changes) =>
        {
            _currentChanges.playerChanges = changes;
        };

        _enemyField.OnFieldChanged += (List<TileChanges> changes) =>
        {
            _currentChanges.enemyChanges = changes;
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
        if (!_enemyField.TryShoot(position))
            return false;
        _playerField.ShootRandom();
        OnFieldsChanged.Invoke(_currentChanges);
        return true;
    }

}