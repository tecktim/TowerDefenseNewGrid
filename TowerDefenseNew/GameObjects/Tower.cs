﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace TowerDefenseNew.GameObjects
{
    internal class Tower : GameObject
    {


        internal Tower(Vector2 center, float attackRadius, int damage, int attackSpeed, List<Enemy> enemies, List<Bullet> bullets, uint type) : base(center, attackRadius)
        {
            Center = center;
            Enemies = enemies;
            this.attackSpeed = attackSpeed;
            this.damage = damage;
            Radius = attackRadius;
            Bullets = bullets;
            Timer = new Timer(attackSpeed);
            AsTimer(true);
            Type = type;
        }

        public void AsTimer(bool active)
        {
            // Creating timer with attackSpeed (millis) as interval
            if (active)
            {
                // Hook up elapsed event for the timer
                Timer.Elapsed += OnTimedEvent;
                Timer.AutoReset = true;
                Timer.Enabled = true;
            }
            else
            {
                Timer.Enabled = false;
                Timer.Stop();
                Timer.Close();
                Timer.Dispose();
            }
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            CheckRange();
        }

        private void CheckRange()
        {
            //type 0 sniper
            //type 1 rifle 
            //type 2 bounce
            try
            {
                foreach (Enemy enemy in Enemies.ToList())
                {
                    if (Intersects(enemy))
                    {
                        Bullet bullet;
                        if (Type == 0) { bullet = new Bullet(Center + new Vector2(0.5f, 0.5f), Radius / 17.5f, damage, Bullets, Enemies, Type); }
                        else { bullet = new Bullet(Center + new Vector2(0.5f, 0.5f), Radius / 35, damage, Bullets, Enemies, Type); }

                        aimAtEnemy = enemy;
                        //Correction of Starting Point of Bullets, damit Schüsse aus dem Mund der Tower kommen
                        if (bullet.TowerType == 0)
                        {
                            if (aimAtEnemy.Center.X < Center.X)
                            {
                                bullet.Center += new Vector2(-0.8f, -0.5f);
                            }
                            else
                            {
                                bullet.Center += new Vector2(-0.2f, -0.5f);
                            }
                        }
                        if (bullet.TowerType == 1)
                        {
                            bullet.Center += new Vector2(-0.5f, -0.15f);
                        }
                        bullet.BulletVelocity(enemy);
                        return;
                    }
                }
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("checkRange exception");
            }
        }
        internal Enemy aimAtEnemy { get; set; }
        private int attackSpeed { get; set; }
        internal int damage { get; set; }
        private List<Enemy> Enemies { get; set; }
        private List<Bullet> Bullets { get; set; }
        private Timer Timer { get; }
        private uint Type { get; set; }
    }
}
