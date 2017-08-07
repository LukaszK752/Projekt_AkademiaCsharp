using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Warships_project
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = SystemParameters.WorkArea.Width / 2 - 600;
            this.Top = SystemParameters.WorkArea.Height / 2 - 350;
        }

        public bool GameOver = false;
        //Default board settings
        public int SizeOfBoard = 10;
        public static int[] AmountOfShips = new int[4] {1, 2, 3, 4};
        public static int[] UserShips = new int[4] { 1, 2, 3, 4 };
        public static int[] ComputerShips = new int[4] { 1, 2, 3, 4 };
        public static int[] AmountOfShipsLeftToCreate = new int[4] { 1, 2, 3, 4 };
        public static int[] AmountOfShipsLeftToCreateComputer = new int[4] { 1, 2, 3, 4 };
        public int SizeOfShip = 4;
        Position position = Position.Horizontal;
        int numberOfUserShip = 0;
        int Turn;

        //Create board fields variables for computer and user
        List<SingleField> ComputerFieldsList = new List<SingleField>();
        Dictionary<Button, SingleField> FirstUserFieldsDictionary = new Dictionary<Button, SingleField>();
        Dictionary<Button, SingleField> ComputerFieldsDictionary = new Dictionary<Button, SingleField>();

        //Changing size of board, and amount of ships for specified size board
        private void ChangeBoardSize_Click(object sender, RoutedEventArgs e)
        {
            var ChoosenObject = (MenuItem)sender;
            if (ChoosenObject.Header.ToString() == "10 x 10 fields")
            {
                SizeOfBoard = 10;
                SmallAmount.Header = "1, 2, 2, 3";
                NormalAmount.Header = "1, 2, 3, 4";
                BigAmount.Header = "1, 2, 3, 6";
                AmountOfShips = new int [4]{1, 2, 3, 4 };
            }
            else if (ChoosenObject.Header.ToString() == "15 x 15 fields")
            {
                SizeOfBoard = 15;
                SmallAmount.Header = "2, 3, 4, 6";
                NormalAmount.Header = "2, 4, 6, 8";
                BigAmount.Header = "2, 5, 7, 13";
                AmountOfShips = new int[4] { 2, 4, 6, 8 };
            }
            else
            {
                SizeOfBoard = 20;
                SmallAmount.Header = "3, 6, 8, 10";
                NormalAmount.Header = "4, 8, 12, 16";
                BigAmount.Header = "5, 9, 14, 18";
                AmountOfShips = new int[4] { 4, 8, 12, 16 };
            }
            AmountOfShipsLeftToCreate = (int[])AmountOfShips.Clone();
            AmountOfShipsLeftToCreateComputer = (int[])AmountOfShips.Clone();
            UserShips = (int[])AmountOfShips.Clone();
            ComputerShips = (int[])AmountOfShips.Clone();
            DrawUserBoard();
        }

        //Changing amount of ships to create
        private void ChangeAmountShips_Click(object sender, RoutedEventArgs e)
        {
            var ChoosenObject = (MenuItem)sender;
            AmountOfShips = ChoosenObject.Header.ToString().Split(',').Select(Int32.Parse).ToArray();
            AmountOfShipsLeftToCreate = (int[])AmountOfShips.Clone();
            AmountOfShipsLeftToCreateComputer = (int[])AmountOfShips.Clone();
            UserShips = (int[])AmountOfShips.Clone();
            ComputerShips = (int[])AmountOfShips.Clone();
            DrawUserBoard();
        }

        // Create empty user board
        private void DrawUserBoard()
        {
            FirstUserGrid.Children.RemoveRange(0, FirstUserGrid.Children.Count);
            FirstUserFieldsDictionary.Clear();
            for (int i = 0; i < SizeOfBoard; i++)
            {
                for (int j = 0; j < SizeOfBoard; j++)
                {
                    FirstUserFieldsDictionary.Add(new Button(), new SingleField(j, i));
                    FirstUserGrid.Children.Add(FirstUserFieldsDictionary.Last().Key);

                    // Adding button events
                    FirstUserFieldsDictionary.Last().Key.MouseEnter += new MouseEventHandler(ShipPositioningFocus);
                    FirstUserFieldsDictionary.Last().Key.MouseRightButtonDown += ChangeShipPosition;
                    FirstUserFieldsDictionary.Last().Key.Click += PutShipOnBaord;
                }
            }
            RescanUserBoard();
            ReloadRectanlges();
        }

        // Checking is ship can be placed in mouse position
        private void ShipPositioningFocus(object sender, RoutedEventArgs e)
        {
            var ChoosenObject = (Button)sender;
            bool CanBePlaced = true;

            // Clear previous focus
            FirstUserFieldsDictionary.Values.ToList().Where(p => p.StateOfField == SingleField.StateField.FocusedField).ToList().ForEach(r => r.SetEmptyField());
            if (SizeOfShip > 0)
            {
                foreach (var item in FirstUserFieldsDictionary)
                {
                    if (item.Key == ChoosenObject)
                    {
                        int PositionX = item.Value.PositionX;
                        int PositionY = item.Value.PositionY;
                        int iterX = 0;
                        int iterY = 0;
                        try
                        {
                            for (int i = 0; i < SizeOfShip; i++)
                            {
                                if (position == Position.Horizontal)
                                    iterX = i;
                                else
                                    iterY = i;

                                var fieldAvailability = (int)FirstUserFieldsDictionary.Where(p => p.Value.PositionX == PositionX + iterX && p.Value.PositionY == PositionY + iterY).First().Value.StateOfField;
                                if (fieldAvailability > 0)
                                {
                                    CanBePlaced = false;
                                    break;
                                }
                            }

                            if (CanBePlaced)
                            {
                                for (int i = 0; i < SizeOfShip; i++)
                                {
                                    if (position == Position.Horizontal)
                                        iterX = i;
                                    else
                                        iterY = i;

                                    FirstUserFieldsDictionary.Where(p => p.Value.PositionX == PositionX + iterX && p.Value.PositionY == PositionY + iterY).First().Value.SetFocus();
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            RescanUserBoard();
        }

        // Set ship position
        private void PutShipOnBaord(object sender, RoutedEventArgs e)
        {
            SizeOfBoardSettings.IsEnabled = false;
            AmountOfShipsSettings.IsEnabled = false;

            var ChoosenObject = (Button)sender;
            if (SizeOfShip > 0)
            {
                var FocusedFieldsList = FirstUserFieldsDictionary.Where(p => p.Value.StateOfField == SingleField.StateField.FocusedField).ToList();

                int FocusedFieldsAmount = FocusedFieldsList.Count;
                if (FocusedFieldsAmount == SizeOfShip)
                {
                    FocusedFieldsList.ForEach(p => p.Value.SetShip(numberOfUserShip));
                }
                else
                    return;

                int MinPosX = FocusedFieldsList[0].Value.PositionX;
                int MaxPosX = FocusedFieldsList[SizeOfShip - 1].Value.PositionX;
                int MinPosY = FocusedFieldsList[0].Value.PositionY;
                int MaxPosY = FocusedFieldsList[SizeOfShip - 1].Value.PositionY;
                if (MinPosX == MaxPosX)
                {
                    for (int i = MinPosX - 1; i <= MinPosX + 1; i++)
                    {
                        for (int j = MinPosY-1; j <= MaxPosY + 1; j++)
                        {
                            if (i >= 0 && i < SizeOfBoard && j >= 0 && j < SizeOfBoard)
                            {
                                try
                                {
                                    FirstUserFieldsDictionary.Where(p => p.Value.PositionX == i && p.Value.PositionY == j && p.Value.StateOfField != SingleField.StateField.ShipField).First().Value.SetBoardingWater(numberOfUserShip);
                                }
                                catch { }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = MinPosX - 1; i <= MaxPosX + 1; i++)
                    {
                        for (int j = MinPosY - 1; j <= MinPosY + 1; j++)
                        {
                            if (i >= 0 && i < SizeOfBoard && j >= 0 && j < SizeOfBoard)
                            {
                                try
                                {
                                    FirstUserFieldsDictionary.Where(p => p.Value.PositionX == i && p.Value.PositionY == j && p.Value.StateOfField != SingleField.StateField.ShipField).First().Value.SetBoardingWater(numberOfUserShip);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            switch (SizeOfShip)
            {
                case 1:
                    AmountOfShipsLeftToCreate[3]--;
                    if (AmountOfShipsLeftToCreate[3] <= 0)
                    {
                        SizeOfShip = 0;
                        MessageBox.Show("Creating of ships is over!");
                        DrawComputerBoard();
                    }
                    break;
                case 2:
                    AmountOfShipsLeftToCreate[2]--;
                    if (AmountOfShipsLeftToCreate[2] <= 0)
                    {
                        SizeOfShip = 1;
                    }
                    break;
                case 3:
                    AmountOfShipsLeftToCreate[1]--;
                    if (AmountOfShipsLeftToCreate[1] <= 0)
                    {
                        SizeOfShip = 2;
                    }
                    break;
                case 4:
                    AmountOfShipsLeftToCreate[0]--;
                    if (AmountOfShipsLeftToCreate[0] <= 0)
                    {
                        SizeOfShip = 3;
                    }
                    break;
                default:
                    break;
            }
            numberOfUserShip++;
            RescanUserBoard();
        }

        public enum Position
        {
            Horizontal = 0,
            Vertical = 1
        }

        // Rescan user board button colors
        private void RescanUserBoard()
        {
            foreach (var field in FirstUserFieldsDictionary)
            {
                switch ((int)field.Value.StateOfField)
                {
                    case 0:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        break;
                    case 1:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        break;
                    case 2:
                        field.Key.Background = Brushes.Gray;
                        break;
                    case 3:
                        field.Key.Background = Brushes.Red;
                        break;
                    case 4:
                        field.Key.Background = Brushes.Beige;
                        break;
                    case 5:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        field.Key.Content = "X";
                        field.Key.Foreground = Brushes.White;
                        break;
                    case 6:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        field.Key.Content = "X";
                        field.Key.Foreground = Brushes.White;
                        break;
                    case 7:
                        field.Key.Background = Brushes.Black;
                        break;
                    default:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        break;
                }
                if (ComputerFieldsDictionary.Count > 0 && (int)field.Value.StateOfField == 1)
                {
                    field.Key.Background = Brushes.DarkSlateBlue;
                }
                ReloadRectanlges();
            }
        }

        // Rescan computer board button colors
        private void RescanComputerBoard()
        {
            foreach (var field in ComputerFieldsDictionary)
            {
                switch ((int)field.Value.StateOfField)
                {
                    case 0:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        break;
                    case 3:
                        field.Key.Background = Brushes.Red;
                        break;
                    case 5:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        field.Key.Content = "X";
                        field.Key.Foreground = Brushes.White;
                        break;
                    case 6:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        field.Key.Content = "X";
                        field.Key.Foreground = Brushes.White;
                        break;
                    case 7:
                        field.Key.Background = Brushes.Black;
                        break;
                    default:
                        field.Key.Background = Brushes.DarkSlateBlue;
                        break;
                }
                ReloadRectanlges();
            }
        }

        // Change ship position by clicking righ button
        private void ChangeShipPosition(object sender, RoutedEventArgs e)
        {
            if (position == Position.Horizontal)
                position = Position.Vertical;
            else
                position = Position.Horizontal;
            (sender as Button).MouseRightButtonUp += ShipPositioningFocus;
        }

        void DrawComputerBoard()
        {
            ComputerGrid.Children.RemoveRange(0, FirstUserGrid.Children.Count);
            for (int y = 0; y < SizeOfBoard; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    ComputerFieldsList.Add(new SingleField(x, y));
                }
            }

            int i = 0;
            foreach (var item in ComputerFieldsList)
            {

                Button tempButton = new Button();
                tempButton.Click += AttackComputerShip;
                ComputerGrid.Children.Add(tempButton);
                ComputerFieldsDictionary.Add(tempButton, ComputerFieldsList[i]);
                i++;
            }
            CreateShipsOnComputerBoard();

        }

        // Automatic creating of computer ships
        void CreateShipsOnComputerBoard()
        {
            int NumberOfShip = 1;

            while (AmountOfShipsLeftToCreateComputer.Sum() > 0)
            {
                if (AmountOfShipsLeftToCreateComputer[0] > 0)
                {
                    if(CreateComputerShip(4, NumberOfShip))
                    {
                        AmountOfShipsLeftToCreateComputer[0]--;
                        NumberOfShip++;
                    }    
                }
                else if (AmountOfShipsLeftToCreateComputer[1] > 0)
                {
                    if (CreateComputerShip(3, NumberOfShip))
                    {
                        AmountOfShipsLeftToCreateComputer[1]--;
                        NumberOfShip++;
                    }
                }
                else if (AmountOfShipsLeftToCreateComputer[2] > 0)
                {
                    if (CreateComputerShip(2, NumberOfShip))
                    {
                        AmountOfShipsLeftToCreateComputer[2]--;
                        NumberOfShip++;
                    }
                }
                else if (AmountOfShipsLeftToCreateComputer[3] > 0)
                {
                    if (CreateComputerShip(1, NumberOfShip))
                    {
                        AmountOfShipsLeftToCreateComputer[3]--;
                        NumberOfShip++;
                    }
                }
            }
            RescanComputerBoard();
        }
        // Creating of single computer ship
        bool CreateComputerShip (int SizeOfShip, int NumberOfShip)
        {
            List<SingleField> AvailableLocations = new List<SingleField>();
            // vertical
            for (int y = 0; y < SizeOfBoard; y++)
            {
                for (int x = 0; x <= SizeOfBoard - SizeOfShip; x++)
                {
                    int Availability = 0;
                    for (int i = 0; i < SizeOfShip; i++)
                    {
                        Availability += (int)ComputerFieldsList.Where(p => p.PositionX == x + i && p.PositionY == y).ToList().First().StateOfField;
                    }

                    if (Availability == 0)
                    {
                        AvailableLocations.Add(new SingleField(x, y));
                    }
                }
            }
            int positionsVertical = 0;
            positionsVertical = AvailableLocations.Count;

            // horizontal
            for (int y = 0; y <= SizeOfBoard - SizeOfShip; y++)
            {
                for (int x = 0; x < SizeOfBoard; x++)
                {
                    int Availability = 0;
                    for (int i = 0; i < SizeOfShip; i++)
                    {
                        Availability += (int)ComputerFieldsList.Where(p => p.PositionX == x && p.PositionY == y + i).ToList().First().StateOfField;
                    }

                    if (Availability == 0)
                    {
                        AvailableLocations.Add(new SingleField(x, y));
                    }
                }
            }
            Random randomPosition = new Random();
            if (AvailableLocations.Count == 0)
            {
                return false;
            }
            var positionNumber = randomPosition.Next(0, AvailableLocations.Count);
            int PosX = AvailableLocations[positionNumber].PositionX;
            int PosY = AvailableLocations[positionNumber].PositionY;
            for (int i = 0; i < SizeOfShip; i++)
            {
                if (positionNumber <= positionsVertical)
                {
                    ComputerFieldsDictionary.Where(p => p.Value.PositionX == PosX + i && p.Value.PositionY == PosY).First().Value.SetShip(NumberOfShip);
                }
                else
                {
                    ComputerFieldsDictionary.Where(p => p.Value.PositionX == PosX && p.Value.PositionY == PosY + i).First().Value.SetShip(NumberOfShip);
                }
            }
            if (positionNumber <= positionsVertical)
            {
                for (int i = PosX - 1; i <= PosX + SizeOfShip; i++)
                {
                    for (int j = PosY - 1; j <= PosY + 1; j++)
                    {
                        if (i >= 0 && j >= 0 && i < SizeOfBoard && j < SizeOfBoard && (ComputerFieldsDictionary.Where(p => p.Value.PositionX == i && p.Value.PositionY == j).First().Value.StateOfField == 0 || (int)ComputerFieldsDictionary.Where(p => p.Value.PositionX == i && p.Value.PositionY == j).First().Value.StateOfField == 1))
                        {
                            ComputerFieldsDictionary.Where(p => p.Value.PositionX == i && p.Value.PositionY == j).First().Value.SetBoardingWater(NumberOfShip);
                        }
                    }
                }
            }
            else
            {
                for (int i = PosX - 1; i <= PosX + 1; i++)
                {
                    for (int j = PosY - 1; j <= PosY + SizeOfShip; j++)
                    {
                        if (i >= 0 && j >= 0 && i < SizeOfBoard && j < SizeOfBoard && (ComputerFieldsDictionary.Where(p => p.Value.PositionX == i && p.Value.PositionY == j).First().Value.StateOfField == 0 || (int)ComputerFieldsDictionary.Where(p => p.Value.PositionX == i && p.Value.PositionY == j).First().Value.StateOfField == 1))
                        {
                            ComputerFieldsDictionary.Where(p => p.Value.PositionX == i && p.Value.PositionY == j).First().Value.SetBoardingWater(NumberOfShip);
                        }
                    }
                }
            }
            Thread.Sleep(12);

            return true;
        }

        // Attacking computer ship
        private void AttackComputerShip(object sender, RoutedEventArgs e)
        {
            var ChoosenObject = (Button)sender;
            foreach (var item in ComputerFieldsDictionary)
            {
                if (ChoosenObject == item.Key)
                {
                    var FieldToAttack = item;
                    AttackComputer(FieldToAttack);
                }
            }
        }

        void AttackComputer(KeyValuePair<Button, SingleField> Field)
        {
            if (Turn % 2 == 0 && !GameOver)
            {
                if (Field.Value.StateOfField == SingleField.StateField.EmptyField)
                {
                    Field.Value.SetMissedShot();
                    Turn++;
                    AttackUser();
                }
                else if (Field.Value.StateOfField == SingleField.StateField.BoardingWaterField)
                {
                    Field.Value.SetMissedShotButBoardingWater();
                    Turn++;
                    AttackUser();
                }
                else if (Field.Value.StateOfField == SingleField.StateField.ShipField)
                {
                    Field.Value.SetSinkingShip();
                    int NumberOfShip = Field.Value.NumberOfShip[0];
                    int ShipFieldsLeft = ComputerFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.ShipField && p.NumberOfShip.Exists(s => s.Equals(NumberOfShip) == true)).ToList().Count;
                    if (ShipFieldsLeft == 0)
                    {
                        ComputerFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.BoardingWaterField && p.NumberOfShip.Exists(s => s.Equals(NumberOfShip) == true)).ToList().ForEach(p => p.SetMissedShotButBoardingWater());
                        int ShipsSize = ComputerFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.SinkingShipField && p.NumberOfShip[0] == NumberOfShip).ToList().Count;
                        ComputerFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.SinkingShipField && p.NumberOfShip[0] == NumberOfShip).ToList().ForEach(r => r.SetShipSank());

                        if (ShipsSize == 4)
                            ComputerShips[0]--;
                        else if (ShipsSize == 3)
                            ComputerShips[1]--;
                        else if (ShipsSize == 2)
                            ComputerShips[2]--;
                        else if (ShipsSize == 1)
                            ComputerShips[3]--;
                    }
                }

                RescanComputerBoard();
                if (ComputerShips.Sum() <= 0)
                {
                    GameOver = true;
                    MessageBox.Show("Game is finished!! User won");
                }
            }
        }

        // Attacking user ships
        private async Task<int> AttackUser()
        {
            List<SingleField> AvailableLocations = new List<SingleField>();
            var SinkingShipList = FirstUserFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.SinkingShipField).ToList();
            if (SinkingShipList.Count == 1)
            {
                int PosX = SinkingShipList[0].PositionX;
                int PosY = SinkingShipList[0].PositionY;

                if (PosX + 1 < SizeOfBoard)
                {
                    var temp = FirstUserFieldsDictionary.Values.Where(p => p.PositionX == PosX + 1 && p.PositionY == PosY && (int)p.StateOfField < 5).FirstOrDefault();
                    if (temp != null)
                        AvailableLocations.Add(temp);
                }  
                if (PosX - 1 >= 0)
                {
                    var temp = FirstUserFieldsDictionary.Values.Where(p => p.PositionX == PosX - 1 && p.PositionY == PosY && (int)p.StateOfField < 5).FirstOrDefault();
                    if (temp != null)
                        AvailableLocations.Add(temp);
                }  
                if (PosY + 1 < SizeOfBoard)
                {
                    var temp = FirstUserFieldsDictionary.Values.Where(p => p.PositionX == PosX && p.PositionY == PosY + 1 && (int)p.StateOfField < 5).FirstOrDefault();
                    if (temp != null)
                        AvailableLocations.Add(temp);
                }   
                if (PosY - 1 >= 0)
                {
                    var temp = FirstUserFieldsDictionary.Values.Where(p => p.PositionX == PosX && p.PositionY == PosY - 1 && (int)p.StateOfField < 5).FirstOrDefault();
                    if (temp != null)
                        AvailableLocations.Add(temp);
                }
                    
            }
            else if (SinkingShipList.Count > 1)
            {
                int PositionXmin, PositionXmax, PositionYmin, PositionYmax = 0;
                int PositionX1 = SinkingShipList[0].PositionX;
                int PositionX2 = SinkingShipList[SinkingShipList.Count - 1].PositionX;
                int PositionY1 = SinkingShipList[0].PositionY;
                int PositionY2 = SinkingShipList[SinkingShipList.Count - 1].PositionY;

                if (PositionX1 > PositionX2)
                {
                    PositionXmax = PositionX1;
                    PositionXmin = PositionX2;
                }
                else
                {
                    PositionXmax = PositionX2;
                    PositionXmin = PositionX1;
                }

                if (PositionY1 > PositionY2)
                {
                    PositionYmax = PositionY1;
                    PositionYmin = PositionY2;
                }
                else
                {
                    PositionYmax = PositionY2;
                    PositionYmin = PositionY1;
                }


                if (PositionXmin == PositionXmax)
                {
                    if (PositionYmin-1 >= 0)
                    {
                        
                        var temp = FirstUserFieldsDictionary.Values.Where(p => p.PositionX == PositionXmin && p.PositionY == PositionYmin - 1 && (int)p.StateOfField < 5).FirstOrDefault();
                        if (temp != null)
                            AvailableLocations.Add(temp);
                    }
                    if (PositionYmax+1 < SizeOfBoard)
                    {
                        var temp = FirstUserFieldsDictionary.Values.Where(p => p.PositionX == PositionXmin && p.PositionY == PositionYmax + 1 && (int)p.StateOfField < 5).FirstOrDefault();
                        if (temp != null)
                            AvailableLocations.Add(temp);
                    }
                }
                else
                {

                    if (PositionXmin - 1 >= 0)
                    {
                        var temp = FirstUserFieldsDictionary.Values.Where(p => p.PositionX == PositionXmin -1 && p.PositionY == PositionYmin&& (int)p.StateOfField < 5).FirstOrDefault();
                        if (temp != null)
                            AvailableLocations.Add(temp);
                    }
                    if (PositionXmax + 1 < SizeOfBoard)
                    {
                        var temp = FirstUserFieldsDictionary.Values.Where(p => p.PositionX == PositionXmax +1 && p.PositionY == PositionYmax&& (int)p.StateOfField < 5).FirstOrDefault();
                        if (temp != null)
                            AvailableLocations.Add(temp);
                    }
                }
            }
            else
            {
                AvailableLocations = FirstUserFieldsDictionary.Values.Where(p => (int)p.StateOfField < 5).ToList();
            }

            Random RandomPosition = new Random();
            int Position = RandomPosition.Next(0, AvailableLocations.Count);

            int PositionX = AvailableLocations[Position].PositionX;
            int PositionY = AvailableLocations[Position].PositionY;

            var SelectedField = FirstUserFieldsDictionary.Where(p => p.Value.PositionX == PositionX && p.Value.PositionY == PositionY).First();
            if (SelectedField.Value.StateOfField == SingleField.StateField.EmptyField)
            {
                SelectedField.Value.SetMissedShot();
                RescanUserBoard();
            }
            else if (SelectedField.Value.StateOfField == SingleField.StateField.ShipField)
            {
                int waited = await Wait();
                SelectedField.Value.SetSinkingShip();
                int NumberOfShip = SelectedField.Value.NumberOfShip[0];
                int ShipFieldsLeft = FirstUserFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.ShipField && p.NumberOfShip.Exists(s => s.Equals(NumberOfShip) == true)).ToList().Count;
                if (ShipFieldsLeft == 0)
                {
                    FirstUserFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.BoardingWaterField && p.NumberOfShip.Exists(s => s.Equals(NumberOfShip) == true)).ToList().ForEach(p => p.SetMissedShotButBoardingWater());
                    int ShipsSize = FirstUserFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.SinkingShipField && p.NumberOfShip[0] == NumberOfShip).ToList().Count;
                    FirstUserFieldsDictionary.Values.Where(p => p.StateOfField == SingleField.StateField.SinkingShipField && p.NumberOfShip[0] == NumberOfShip).ToList().ForEach(r => r.SetShipSank());

                    if (ShipsSize == 4)
                        UserShips[0]--;
                    else if (ShipsSize == 3)
                        UserShips[1]--;
                    else if (ShipsSize == 2)
                        UserShips[2]--;
                    else if (ShipsSize == 1)
                        UserShips[3]--;
                }

                if (UserShips.Sum() <= 0)
                {
                    GameOver = true;
                    RescanUserBoard();
                    MessageBox.Show("Game is finished, Computer won!!");
                    return 0;
                }
                else
                {
                    RescanUserBoard();
                    AttackUser();
                    return 0;
                }
                
            }
            else if (SelectedField.Value.StateOfField == SingleField.StateField.BoardingWaterField)
            {
                SelectedField.Value.SetMissedShotButBoardingWater();
                RescanUserBoard();
            }
            Turn++;
            return 0;
        }

        // Async procedure to wait after computer attack - to display changes of board after every single attack
        private async Task<int> Wait()
        {
            return await Task.Run(() =>
            {
                Thread.Sleep(500);
                return 0;
            });
        }

        // Reload amount of ships left indication
        void ReloadRectanlges()
        {
            r1a.Text = UserShips[3].ToString() + " / " + AmountOfShips[3].ToString();
            r2a.Text = UserShips[2].ToString() + " / " + AmountOfShips[2].ToString();
            r3a.Text = UserShips[1].ToString() + " / " + AmountOfShips[1].ToString();
            r4a.Text = UserShips[0].ToString() + " / " + AmountOfShips[0].ToString();
            r1b.Text = ComputerShips[3].ToString() + " / " + AmountOfShips[3].ToString();
            r2b.Text = ComputerShips[2].ToString() + " / " + AmountOfShips[2].ToString();
            r3b.Text = ComputerShips[1].ToString() + " / " + AmountOfShips[1].ToString();
            r4b.Text = ComputerShips[0].ToString() + " / " + AmountOfShips[0].ToString();

            if (UserShips[0] <= 0)
                r4.Fill = Brushes.Gray;
            if (UserShips[1] <= 0)
                r3.Fill = Brushes.Gray;
            if (UserShips[2] <= 0)
                r2.Fill = Brushes.Gray;
            if (UserShips[3] <= 0)
                r1.Fill = Brushes.Gray;
            if (ComputerShips[0] <= 0)
                s4.Fill = Brushes.Gray;
            if (ComputerShips[1] <= 0)
                s3.Fill = Brushes.Gray;
            if (ComputerShips[2] <= 0)
                s2.Fill = Brushes.Gray;
            if (ComputerShips[3] <= 0)
                s1.Fill = Brushes.Gray;
        }

        // Reset boards
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ReloadDefaults();
        }

        // Exit application
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        //Help display
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow Help = new HelpWindow();
            Help.Show();
        }

        //Reload default values after click of reset button
        void ReloadDefaults()
        {
            GameOver = false;
            SizeOfBoard = 10;
            AmountOfShips = new int[4] { 1, 2, 3, 4 };
            UserShips = new int[4] { 1, 2, 3, 4 };
            ComputerShips = new int[4] { 1, 2, 3, 4 };
            AmountOfShipsLeftToCreate = new int[4] { 1, 2, 3, 4 };
            AmountOfShipsLeftToCreateComputer = new int[4] { 1, 2, 3, 4 };
            SizeOfShip = 4;
            position = Position.Horizontal;
            Turn = 0;
            ComputerFieldsDictionary.Clear();
            FirstUserFieldsDictionary.Clear();
            FirstUserGrid.Children.Clear();
            ComputerFieldsList.Clear();
            ComputerGrid.Children.RemoveRange(0, ComputerGrid.Children.Count);
            SizeOfBoardSettings.IsEnabled = true;
            AmountOfShipsSettings.IsEnabled = true;
            ReloadRectanlges();
            r1.Fill = r2.Fill = r3.Fill = r4.Fill = s1.Fill = s2.Fill = s3.Fill = s4.Fill = Brushes.SteelBlue;
        }


    }
}
