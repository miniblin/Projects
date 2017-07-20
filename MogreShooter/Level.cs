using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
namespace RaceGame
{
    /// <summary>
    /// level class creates environemnt and loads all objects within it. keeps track of items in level such as gems enemies adn powerups
    /// </summary>
    class Level
    {
       
        SceneManager mSceneMgr;  


        Player player;
        Environment environment;
        List<Gem> gems;
        List<Gem> gemsToRemove;
        List<PowerUp> powerUps;
        List<PowerUp> powerUpsToRemove;
        List<Enemy> enemies;
        List<Enemy> enemiesToRemove;
        List<CollectableGun> collectableGuns;
        RenderWindow mWindow;
        public int level;
        int maxLevel = 3;
        public bool levelRunning;
        public bool win = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public Level(SceneManager mSceneMgr, Player player, RenderWindow mWindow)
        {
            this.mSceneMgr = mSceneMgr;
            this.player = player;
            this.mWindow = mWindow;
            level = 1;
            levelRunning = true;
            LoadLevel();
            
        }

        /// <summary>
        /// load the correct level
        /// </summary>
        public void LoadLevel()
        {
            switch (level)
            {
                case 1:
                    Level1();
                    break;
                case 2:
                    Level2();
                    break;
                default:
                    Level3();
                    break;
            }

        }

        /// <summary>
        /// updaytes all objects within level removes those that have had collsions with player/are dead
        /// </summary>    
        ///  <param name="objName">name of colliding object to check</param>
        public void Update(FrameEvent evt)
        {
            if (!player.PlayerDead)
            {
                foreach (Enemy robot in enemies)
                {
                   
                    robot.Update(evt);
                    if (robot.toRemove)
                    {
                        enemiesToRemove.Add(robot);
                    }
                }

                foreach (Enemy robot in enemiesToRemove)
                {

                    enemies.Remove(robot);
                    robot.Dispose();

                }
                enemiesToRemove.Clear();

               
                foreach (PowerUp powerUp in powerUps)
                {
                   
                    powerUp.Update(evt);
                    if (powerUp.toRemove)
                    {
                        powerUpsToRemove.Add(powerUp);
                    }
                }

                foreach (PowerUp powerUp in powerUpsToRemove)
                {

                    powerUps.Remove(powerUp);
                    powerUp.Dispose();

                }
                powerUpsToRemove.Clear();
                ///////////
                foreach (Gem gem in gems)
                {

                    gem.Update(evt);
                    if (gem.ToRemove)
                    {
                        player.Stats.Score.Increase(gem.Increase);
                        gemsToRemove.Add(gem);
                    }
                }

                foreach (Gem gem in gemsToRemove)
                {

                    gems.Remove(gem);
                    gem.Dispose();
                }
                foreach (CollectableGun colGun in collectableGuns)
                {
                    if (!colGun.toRemove)
                    {
                        colGun.Update(evt);
                    }
                    gemsToRemove.Clear();
                }

                if (checkLevelComplete())
                {
                    if (level < maxLevel)
                    {
                        level++;
                        clearLevel();
                        LoadLevel();
                    }
                    else
                    {
                        win = true;
                        levelRunning = false;
                    }
                }

            }
            else
            {
                levelRunning = false;
            }
        }


        public bool checkLevelComplete()
        {
            return(gems.Count==0);
        }

        public void clearLevel()
        {
            foreach (Gem gem in gems)
            {
               
                gem.Dispose();
               
                
            }
            gems.Clear();
            foreach (Enemy enemy in enemies)
            {

                enemy.Dispose();

            }
            enemies.Clear();
           /* foreach (CollectableGun colGun in collectableGuns)
            {

                colGun.Dispose();

            }
            * */
           // collectableGuns.Clear();
          //  environment.Dispose();

            //delet all objects an empty lists;
        }

        /// <summary>
        /// load level 1 resources
        /// </summary>
        public void Level1()
        {
            BuildWalls();
            collectableGuns = new List<CollectableGun>();
            enemies = new List<Enemy>();
            environment = new RaceGame.Environment(mSceneMgr, mWindow);
            gems = new List<Gem>();
            gemsToRemove = new List<Gem>();
            enemiesToRemove = new List<Enemy>();
            powerUpsToRemove = new List<PowerUp>();
           
            
            int numGems = 3;
            for (int i = 0; i < numGems; i++)
            {
                gems.Add(new RaceGame.RedGem(mSceneMgr, new RaceGame.Stat()));
                gems.Add(new RaceGame.BlueGem(mSceneMgr, new RaceGame.Stat()));
               
            }
           

            powerUps = new List<PowerUp>();
            powerUps.Add(new LifePU(mSceneMgr, player.Stats.Lives));
            powerUps.Add(new LifePU(mSceneMgr, player.Stats.Lives));
            powerUps.Add(new LifePU(mSceneMgr, player.Stats.Lives));
            powerUps.Add(new HealthPU(mSceneMgr, player.Stats.Health));
            powerUps.Add(new ShieldPU(mSceneMgr, player.Stats.Shield));
                

            
           
        }

        /// <summary>
        /// load level 2 resources
        /// </summary>
        public void Level2()
        {
            int numGems = 4;
            for (int i = 0; i < numGems; i++)
            {
                gems.Add(new RaceGame.RedGem(mSceneMgr, new RaceGame.Stat()));
                gems.Add(new RaceGame.BlueGem(mSceneMgr, new RaceGame.Stat()));
            }

                       
           collectableGuns.Add(new CollectableGun(mSceneMgr, new Cannon(mSceneMgr), player.PlayerArmoury));
           collectableGuns[0].SetPosition(new Vector3(150, 10, 200));
           
            
           
        }


        /// <summary>
        /// load level 3 resources
        /// </summary>
        public void Level3()
        {
            int numGems = 5;
            for (int i = 0; i < numGems; i++)
            {
                gems.Add(new RaceGame.RedGem(mSceneMgr, new RaceGame.Stat()));
                gems.Add(new RaceGame.BlueGem(mSceneMgr, new RaceGame.Stat()));
            }

            collectableGuns.Add(new CollectableGun(mSceneMgr, new BombDropper(mSceneMgr), player.PlayerArmoury));
            collectableGuns[1].SetPosition(new Vector3(350, 1, 300));
          
            int numRobots = 5;
            for (int i = 0; i < numRobots; i++)
            {
                enemies.Add(new Robot(mSceneMgr, player));
            }

            powerUps.Add(new LifePU(mSceneMgr, player.Stats.Lives));
            powerUps.Add(new HealthPU(mSceneMgr, player.Stats.Health));
            powerUps.Add(new ShieldPU(mSceneMgr, player.Stats.Shield));
        }


        /// <summary>
        /// creates the 3d mesh objects for walls from a fence model. positions the accordingly
        /// </summary>
        public void BuildWalls()
        {
            ModelElement Fence = new ModelElement(mSceneMgr, "3BarFence.mesh");
            ModelElement Fence2 = new ModelElement(mSceneMgr, "3BarFence.mesh");
            ModelElement Fence3 = new ModelElement(mSceneMgr, "3BarFence.mesh");
            ModelElement Fence4 = new ModelElement(mSceneMgr, "3BarFence.mesh");
            Degree deg = 0;
            Fence.GameEntity.SetMaterialName("3BarFence");
            Fence.GameNode.Scale(0.7f, 0.1f, 0.1f);
            mSceneMgr.RootSceneNode.AddChild(Fence.GameNode);
          
            Fence.SetPosition(new Vector3(0, 20, -500));

            Fence2.GameEntity.SetMaterialName("3BarFence");
            Fence2.GameNode.Scale(0.7f, 0.1f, 0.1f);
            mSceneMgr.RootSceneNode.AddChild(Fence2.GameNode);
            Fence2.SetPosition(new Vector3(20, 20, 500));



            Fence3.GameEntity.SetMaterialName("3BarFence");
            Fence3.GameNode.Scale(0.7f, 0.1f, 0.1f);
            mSceneMgr.RootSceneNode.AddChild(Fence3.GameNode);
            Fence3.SetPosition(new Vector3(-500, 20, -20));
            deg = 90;
            Fence3.GameNode.Yaw(deg);
            deg = 90;
            Fence4.GameEntity.SetMaterialName("3BarFence");
            Fence4.GameNode.Scale(0.7f, 0.1f, 0.1f);
            mSceneMgr.RootSceneNode.AddChild(Fence4.GameNode);
            Fence4.SetPosition(new Vector3(500, 20, -20));
            Fence4.GameNode.Yaw(deg);
        }
    }



    
}


