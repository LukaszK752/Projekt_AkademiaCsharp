using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Warships_project
{
    public class SingleField
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public List<int> NumberOfShip;
        public StateField StateOfField { get; private set; }

        public SingleField()
        {
            StateOfField = StateField.EmptyField;
        }

        public SingleField(int PositionX, int PositionY)
        {
            this.PositionX = PositionX;
            this.PositionY = PositionY;
            NumberOfShip = new List<int>();
        }

        public enum StateField : int
        {
            EmptyField,
            BoardingWaterField,
            ShipField,
            SinkingShipField,
            FocusedField,
            Missed,
            MissedButBoardingWater,
            ShipSank
        }

        public void SetFocus()
        {
            StateOfField = StateField.FocusedField;
        }

        public void SetShip(int number)
        {
            StateOfField = StateField.ShipField;
            NumberOfShip.Add(number);
        }

        public void SetBoardingWater(int number)
        {
            StateOfField = StateField.BoardingWaterField;
            NumberOfShip.Add(number);
        }

        public void SetEmptyField()
        {
            StateOfField = StateField.EmptyField;
        }

        public void SetSinkingShip()
        {
            StateOfField = StateField.SinkingShipField;
        }

        public void SetMissedShot()
        {
            StateOfField = StateField.Missed;
        }

        public void SetMissedShotButBoardingWater()
        {
            StateOfField = StateField.MissedButBoardingWater;
        }

        public void SetShipSank()
        {
            StateOfField = StateField.ShipSank;
        }
    }
}
