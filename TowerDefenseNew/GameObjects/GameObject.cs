﻿using OpenTK.Mathematics;
using System;

namespace TowerDefenseNew.GameObjects
{
    internal class GameObject : IReadOnlyCircle
    {
        public GameObject(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
            IsAlive = true;
        }

        public bool IsAlive { get; protected set; }
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public bool Intersects(IReadOnlyCircle obj)
        {
            //circlecollider
            if (obj != null)
            {
                float radius = Radius + obj.Radius;
                float deltaX = Center.X - obj.Center.X;
                float deltaY = Center.Y - obj.Center.Y;
                float distance = (float)Math.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
                if (distance < radius)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }



    }
}
