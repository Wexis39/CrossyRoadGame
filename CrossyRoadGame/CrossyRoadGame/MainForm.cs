using System.Windows.Forms;

namespace CrossyRoadGame
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        List<Image> carResources = new List<Image>
        {
            Properties.Resources.Car1, Properties.Resources.Car2,Properties.Resources.Car3,
            Properties.Resources.Car4, Properties.Resources.Car5,Properties.Resources.Car6,
            Properties.Resources.Car7, Properties.Resources.Car8
        };

        List<Image> truckResources = new List<Image>
        {
            Properties.Resources.Truck1, Properties.Resources.Truck2,Properties.Resources.Truck3
        };

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.KeyDown += KeyControl;
            picRoad.Size = ClientSize;
            picRoad.Location = new System.Drawing.Point(0, 0);

            List<Control> controlsParent = new List<Control> {
                picChicken, lblLevel, labelLevel, labelLevel
            };

            controlsParent.ForEach(control => control.Parent = picRoad);
            picChickenCollision.Hide();
        }

        bool isWalk = false;
        bool isFlipX = false;
        byte playerSpeed = 6;
        int level = 1;

        private void KeyControl(object? sender, KeyEventArgs e)
        {
            Point oldLocation = picChicken.Location;
            Point oldCollisionLocation = picChickenCollision.Location;

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                picChicken.Top -= playerSpeed;
                picChickenCollision.Top -= playerSpeed;
                isWalk = true;
            }
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                picChicken.Top += playerSpeed;
                picChickenCollision.Top += playerSpeed;
                isWalk = true;
            }
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                picChicken.Left -= playerSpeed;
                picChickenCollision.Left -= playerSpeed;
                isFlipX = true;
                isWalk = true;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                picChicken.Left += playerSpeed;
                picChickenCollision.Left += playerSpeed;
                isFlipX = false;
                isWalk = true;
            }

            if (picChicken.Location.X >= 900 || picChicken.Location.X <= -35
                || picChicken.Location.Y >= 556 || picChicken.Location.Y <= -55)
            {
                picChicken.Location = oldLocation;
                picChickenCollision.Location = oldCollisionLocation;
            }
            if (picChickenCollision.Location.Y <= 84)
            {
                StartNextLevel();
            }
        }

        byte chickenWalkAnim = 1;
        byte chickenIdleAnim = 1;

        private void tmrChickenAnim_Tick(object sender, EventArgs e)
        {
            if (isWalk)
            {
                switch (chickenWalkAnim)
                {
                    case 1:
                        picChicken.BackgroundImage = Properties.Resources.ChickenWalk1;
                        break;
                    case 2:
                        picChicken.BackgroundImage = Properties.Resources.ChickenWalk2;
                        break;
                    case 3:
                        picChicken.BackgroundImage = Properties.Resources.ChickenWalk3;
                        break;
                    case 4:
                        picChicken.BackgroundImage = Properties.Resources.ChickenWalk4;
                        chickenWalkAnim = 0;
                        break;

                }
                chickenWalkAnim++;
                isWalk = false;
            }
            else
            {
                switch (chickenIdleAnim)
                {
                    case 1:
                        picChicken.BackgroundImage = Properties.Resources.ChickenIdle1;
                        break;
                    case 2:
                        picChicken.BackgroundImage = Properties.Resources.ChickenIdle2;
                        chickenIdleAnim = 0;
                        break;

                }
                chickenIdleAnim++;
            }

            if (picChicken.BackgroundImage != null && isFlipX)
            {
                Bitmap flippedImage = new Bitmap(picChicken.BackgroundImage);

                flippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

                picChicken.BackgroundImage = flippedImage;
            }

            try
            {
                foreach (var vehicle in vehicles)
                {
                    if (picChickenCollision.Bounds.IntersectsWith(vehicle.Bounds))
                    {
                        tmrChickenAnim.Stop();
                        GameOverForm gameOverForm = new GameOverForm(level);
                        gameOverForm.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception)
            {
                //We use try catch to access the list.
            }

        }

        List<PictureBox> removeVehicles = new List<PictureBox>();
        Dictionary<PictureBox, byte> dicVehicleSpeedLeft = new Dictionary<PictureBox, byte>();
        Dictionary<PictureBox, byte> dicVehicleSpeedRight = new Dictionary<PictureBox, byte>();
        private void tmrVehicleMovement_Tick(object sender, EventArgs e)
        {
            foreach (var vehicle in dicVehicleSpeedLeft)
            {
                vehicle.Key.Left -= vehicle.Value;
                if (vehicle.Key.Location.X <= -300)
                {
                    removeVehicles.Add(vehicle.Key);
                }
            }

            foreach (var vehicle in dicVehicleSpeedRight)
            {
                vehicle.Key.Left += vehicle.Value;
                if (vehicle.Key.Location.X >= 1000)
                {
                    removeVehicles.Add(vehicle.Key);
                }
            }

            foreach (var item in removeVehicles)
            {
                picRoad.Controls.Remove(item);
                vehicles.Remove(item);
                dicVehicleSpeedLeft.Remove(item);
                dicVehicleSpeedRight.Remove(item);
                vehiclesSprite.Remove(item);
            }

            removeVehicles.Clear();
            vehicles.ForEach(vehicles => vehicles.BringToFront());
        }

        byte createVehicleTime = 4;
        private void tmrCreateVehicle_Tick(object sender, EventArgs e)
        {
            if (createVehicleTime == 0)
            {
                tmrCreateVehicle.Interval = (new Random().Next(840,1040));
                CreateVehicle();
                tmrVehicleMovement.Start();
                createVehicleTime = 4;

                foreach (var vehicle in dicVehicleSpeedLeft)
                {
                    if (vehicle.Key == vehicleLine1 || vehicle.Key == vehicleLine3)
                    {
                        Bitmap flippedImage = new Bitmap(vehicle.Key.BackgroundImage);

                        flippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);

                        vehicle.Key.BackgroundImage = flippedImage;
                    }
                }
            }

            createVehicleTime--;
        }

        byte carOrTruckChance; // 0 = car, 1 = car, 2 = truck

        List<PictureBox> vehicles = new List<PictureBox>();
        List<PictureBox> vehiclesSprite = new List<PictureBox>();
        PictureBox vehicleLine1;
        PictureBox vehicleLine2;
        PictureBox vehicleLine3;
        PictureBox vehicleLine4;
        private void CreateVehicle()
        {
            vehicleLine1 = new PictureBox
            {
                Location = new Point(1300, 423),
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Stretch,
                Parent = picRoad,
            };
            vehicles.Add(vehicleLine1);
            vehiclesSprite.Add(vehicleLine1);
            dicVehicleSpeedLeft.Add(vehicleLine1, Convert.ToByte(new Random().Next(10, 12)));

            vehicleLine2 = new PictureBox
            {
                Location = new Point(-260, 340),
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Stretch,
                Parent = picRoad
            };
            vehicles.Add(vehicleLine2);
            vehiclesSprite.Add(vehicleLine2);
            dicVehicleSpeedRight.Add(vehicleLine2, Convert.ToByte(new Random().Next(8,11)));

            vehicleLine3 = new PictureBox
            {
                Location = new Point(1100, 251),
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Stretch,
                Parent = picRoad
            };
            vehicles.Add(vehicleLine3);
            vehiclesSprite.Add(vehicleLine3);
            dicVehicleSpeedLeft.Add(vehicleLine3, Convert.ToByte(new Random().Next(6, 8)));

            vehicleLine4 = new PictureBox
            {
                Location = new Point(-300, 168),
                BackColor = Color.Transparent,
                BackgroundImageLayout = ImageLayout.Stretch,
                Parent = picRoad
            };
            vehicles.Add(vehicleLine4);
            vehiclesSprite.Add(vehicleLine4);
            dicVehicleSpeedRight.Add(vehicleLine4, Convert.ToByte(new Random().Next(12, 14)));

            for (int i = 0; i < vehiclesSprite.Count; i++)
            {
                carOrTruckChance = (byte)new Random().Next(0, 3);

                if (carOrTruckChance == 0 || carOrTruckChance == 1)
                {
                    vehiclesSprite[i].BackgroundImage = carResources[new Random().Next(0, carResources.Count)];
                    vehiclesSprite[i].Size = new Size(140, 70);
                    //vehiclesSprite[i].Location = new Point(vehiclesSprite[i].Location.X - (new Random().Next(145, 220)), vehiclesSprite[i].Location.Y);
                }
                else
                {
                    vehiclesSprite[i].BackgroundImage = truckResources[new Random().Next(0, truckResources.Count)];
                    vehiclesSprite[i].Size = new Size(238, 90);
                    vehiclesSprite[i].Location = new Point(vehiclesSprite[i].Location.X, vehiclesSprite[i].Location.Y - 10);
                    //vehiclesSprite[i].Location = new Point(vehiclesSprite[i].Location.X - (new Random().Next(243, 300)), vehiclesSprite[i].Location.Y);
                }
            }

            vehiclesSprite.Clear();

        }

        Label lblNextLevel;
        PictureBox picNextLevel;
        private void StartNextLevel()
        {
            level++;

            lblLevel.Text = level.ToString();
            picChicken.Location = new Point(435, 539);
            picChickenCollision.Location = new Point(481, 594);

            picNextLevel = new PictureBox()
            {
                Size = new Size(1000, 1000),
                Location = new Point(0, 0),
                BackColor = Color.Bisque,
            };

            lblNextLevel = new Label()
            {
                Text = $"Congratulations! Next level is {level}",
                Font = new Font("Arial", 40, FontStyle.Bold),
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
                Parent = picRoad,
                AutoSize = true,
                Location = new Point(80, 240)
            };

            this.Controls.Add(lblNextLevel);
            this.Controls.Add(picNextLevel);

            lblNextLevel.Parent = picNextLevel;
            picNextLevel.BringToFront();
            lblNextLevel.BringToFront();

            this.KeyDown -= KeyControl;

            tmrCreateVehicle.Stop();
            tmrVehicleMovement.Stop();
            tmrStartNextLevel.Start();

            removeVehicles.Clear();;
        }
        private void tmrStartNextLevel_Tick(object sender, EventArgs e)
        {
            tmrStartNextLevel.Stop();
            lblNextLevel.Hide();
            picNextLevel.Hide();

            tmrCreateVehicle.Start();
            tmrVehicleMovement.Start();

            this.KeyDown += KeyControl;
        }
    }
}
