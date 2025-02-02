﻿using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System;
using System.Linq;
using TowerDefenseNew.GameObjects;
using Zenseless.OpenTK;

namespace TowerDefenseNew
{
    internal class Control
    {
        private readonly Model _model;
        private readonly View _view;

        public Control(Model model, View view)
        {
            _model = model;
            _view = view;
        }

        internal void Update(float deltaTime, KeyboardState keyboard)
        {
            if (keyboard.IsKeyReleased(Keys.D5))
            {
                _model.giveCash();
            }
        }

        internal void Click(float x, float y, KeyboardState keyboard)
        {
            var cam = _view.Camera;
            var fromViewportToWorld = Transformation2d.Combine(cam.InvViewportMatrix, cam.CameraMatrix.Inverted());
            var pixelCoordinates = new Vector2(x, y);
            var world = pixelCoordinates.Transform(fromViewportToWorld);
            if (world.X < 0 || _model.Grid.Columns < world.X) return;
            if (world.Y < 0 || _model.Grid.Rows < world.Y) return;
            var column = (int)Math.Truncate(world.X);
            var row = (int)Math.Truncate(world.Y);
            var cell = _model.CheckCell(column, row);
            if (_model.gameOver == false)
            {
                //Sniper verkaufen
                if (cell == Grid.CellType.Sniper && keyboard.IsKeyDown(Keys.Delete))
                {
                    foreach (Tower tower in _model.towers.ToList())
                    {
                        if (tower.Center.X == column && tower.Center.Y == row)
                        {
                            _model.ClearCell(column, row, tower);
                            Console.WriteLine("sold sniper, new balance: " + _model.cash);
                        }
                        else continue;
                    }
                    return;
                }

                //Rifle verkaufen
                if (cell == Grid.CellType.Rifle && keyboard.IsKeyDown(Keys.Delete))
                {
                    //Sell Rifle
                    foreach (Tower tower in _model.towers.ToList())
                    {
                        if (tower.Center.X == column && tower.Center.Y == row)
                        {
                            _model.ClearCell(column, row, tower);
                            Console.WriteLine("sold rifle, new balance: " + _model.cash);
                        }
                        else continue;
                    }
                    return;
                }

                //Bouncer verkaufen
                if (cell == Grid.CellType.Bouncer && keyboard.IsKeyDown(Keys.Delete))
                {
                    foreach (Tower tower in _model.towers.ToList())
                    {
                        if (tower.Center.X == column && tower.Center.Y == row)
                        {
                            _model.ClearCell(column, row, tower);
                            Console.WriteLine("sold sniper, new balance: " + _model.cash);
                        }
                        else continue;
                    }
                    return;
                }

                //Schauen ob Cell leer ist
                if (cell == Grid.CellType.Empty)
                {
                    //Sniper kaufen
                    if (keyboard.IsKeyDown(Keys.D2))
                    {
                        if (cell != Grid.CellType.Empty) { return; }
                        else { _model.PlaceSniper(column, row); }
                        return;
                    }
                    //Rifle kaufen
                    if (keyboard.IsKeyDown(Keys.D1))
                    {
                        if (cell != Grid.CellType.Empty) { return; }
                        else { _model.PlaceRifle(column, row); }
                        return;
                    }
                    //Bouncer kaufen
                    if (keyboard.IsKeyDown(Keys.D3))
                    {
                        if (cell != Grid.CellType.Empty) { return; }
                        else { _model.PlaceBouncer(column, row); }
                        return;
                    }
                }
            }
        }

        internal void PlacePath(float x, float y, KeyboardState keyboard)
        {
            var cam = _view.Camera;
            var fromViewportToWorld = Transformation2d.Combine(cam.InvViewportMatrix, cam.CameraMatrix.Inverted());
            var pixelCoordinates = new Vector2(x, y);
            var world = pixelCoordinates.Transform(fromViewportToWorld);
            if (world.X < 0 || _model.Grid.Columns < world.X) return;
            if (world.Y < 0 || _model.Grid.Rows < world.Y) return;
            var column = (int)Math.Truncate(world.X);
            var row = (int)Math.Truncate(world.Y);
            var cell = _model.CheckCell(column, row);
            if (cell == Grid.CellType.Empty)
            {
                //Path setzen
                if (keyboard.IsKeyDown(Keys.Space))
                {
                    if (cell != Grid.CellType.Empty) { return; }
                    else
                    {
                        if (_model.PlacePath(column, row))
                        {
                            return;
                        }
                    }
                    return;
                }
            }
        }

        internal void ShowTowerSample(float x, float y, KeyboardState keyboard)
        {
            var cam = _view.Camera;
            var fromViewportToWorld = Transformation2d.Combine(cam.InvViewportMatrix, cam.CameraMatrix.Inverted());
            var pixelCoordinates = new Vector2(x, y);
            var world = pixelCoordinates.Transform(fromViewportToWorld);
            if (world.X < 0 || _model.Grid.Columns < world.X) return;
            if (world.Y < 0 || _model.Grid.Rows < world.Y) return;
            var column = (int)Math.Truncate(world.X);
            var row = (int)Math.Truncate(world.Y);
            var cell = _model.CheckCell(column, row);

            if (cell == Grid.CellType.Empty)
            {
                if (keyboard.IsKeyDown(Keys.D2))
                {
                    if (cell != Grid.CellType.Empty) { return; }
                    else
                    {
                        _view.sampleSniper = true;
                        _view.sampleColRow = new Vector2(column, row);
                    }//Snake
                    return;
                }
                else _view.sampleSniper = false;
                if (keyboard.IsKeyDown(Keys.D1))
                {
                    if (cell != Grid.CellType.Empty) { return; }
                    else
                    {
                        _view.sampleRifle = true;
                        _view.sampleColRow = new Vector2(column, row);
                    }//Snake 
                    return;
                }
                else _view.sampleRifle = false;
                if (keyboard.IsKeyDown(Keys.D3))
                {
                    if (cell != Grid.CellType.Empty) { return; }
                    else
                    {
                        _view.sampleBouncer = true;
                        _view.sampleColRow = new Vector2(column, row);
                    }//Snake 
                    return;
                }
                else _view.sampleBouncer = false;
            }

        }
    }
}
