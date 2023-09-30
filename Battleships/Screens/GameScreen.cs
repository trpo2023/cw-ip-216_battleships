using Battleships.Foundation;
using Battleships.Models;
using Microsoft.Xna.Framework;
using Battleships.Models.Primitive;
using Battleships.Models.Changes;
using System;

namespace Battleships.Screens;

public class GameScreen : Screen
{
    private BattleshipsModel _model = new();

    private Sprite[,] _playerFieldSprites = new Sprite[10, 10];
    private Sprite[,] _enemyFieldSprites = new Sprite[10, 10];

    public GameScreen(Navigator navigator)
        : base(navigator) { }

    private Vector2 GetScreenPos(Vector2 value, FieldType fieldType)
    {
        if (fieldType == FieldType.Player)
            return new Vector2(50 + value.X * 50, 50 + value.Y * 50);
        else
            return new Vector2(650 + value.X * 50, 50 + value.Y * 50);
    }

    private Vector2 GetFieldPos(Vector2 value, FieldType fieldType)
    {
        value.X -= value.X % 50;
        value.Y -= value.Y % 50;
        if (fieldType == FieldType.Player)
            return new Vector2((value.X - 50) / 50, (value.Y - 50) / 50);
        else
            return new Vector2((value.X - 650) / 50, (value.Y - 50) / 50);
    }

    private SpriteName GetSpriteNameByTileState(TileState tileState)
    {
        return tileState switch
        {
            TileState.Empty => SpriteName.Empty,
            TileState.Ship => SpriteName.Ship,
            TileState.Miss => SpriteName.ShootMiss,
            TileState.Destroy => SpriteName.ShootDestroy,
            TileState.Hit => SpriteName.ShootShip,
            _ => throw new ArgumentException(),
        };
    }

    protected override void SpritesInit()
    {
        base.SpritesInit();
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
            {
                _playerFieldSprites[i, j] = new Sprite(
                    GetSpriteNameByTileState(_model.GetTileField(FieldType.Player)[i, j]),
                    GetScreenPos(new Vector2(i, j), FieldType.Player)
                );
                _enemyFieldSprites[i, j] = new Sprite(
                    GetSpriteNameByTileState(_model.GetTileField(FieldType.Enemy)[i, j]),
                    GetScreenPos(new Vector2(i, j), FieldType.Enemy)
                );
            }

        _spriteList.Add(new Sprite(SpriteName.GameBackground, new Vector2(0, 0)));
        _spriteList.Add(new Sprite(SpriteName.PlayerField, new Vector2(50, 50)));
        _spriteList.Add(new Sprite(SpriteName.EnemyField, new Vector2(650, 50)));

        foreach (var sprite in _playerFieldSprites)
            _spriteList.Add(sprite);
        foreach (var sprite in _enemyFieldSprites)
            _spriteList.Add(sprite);
    }

    protected override void EventInit()
    {
        base.EventInit();
        _model.OnGameOver += (FieldType FieldType) =>
        {
            _navigator.StartEndScreen(FieldType == FieldType.Player);
        };
        _model.OnFieldsChanged += (FieldsChanges fc) =>
        {
            foreach (var change in fc.playerChanges)
                _playerFieldSprites[change.position.x, change.position.y].name =
                    GetSpriteNameByTileState(change.tileState);
            foreach (var change in fc.enemyChanges)
                _enemyFieldSprites[change.position.x, change.position.y].name =
                    GetSpriteNameByTileState(change.tileState);
        };

        _events.Add(
            new ScreenEvent(
                new Rectangle(650, 50, 500, 500),
                (Point point) =>
                {
                    Vector2 fieldPos = GetFieldPos(point.ToVector2(), FieldType.Enemy);
                    _model.TryShoot(new Vector2i(fieldPos));
                }
            )
        );
    }
}
