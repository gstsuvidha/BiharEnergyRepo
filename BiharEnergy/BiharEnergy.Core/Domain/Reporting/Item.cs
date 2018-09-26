using System;

namespace BiharEnergy.Core.Domain.Reporting
{
    public class Item
    {
        public int Num { get; set; }
        public ItemDetail ItmDet { get; set; }
       
    }

    public class ItemDetail
    {
        public double Txval { get; set; }
        public double Rt { get; set; }
        public double? Iamt { get; set; }
        public double? Camt { get; set; }
        public double? Samt { get; set; }
        public double? Csamt { get; set; }

        
    }
    public class AdvanceItem
    {
        public int Num { get; set; }
        public AdvanceItemDetail ItmDet { get; set; }

    }

    public class AdvanceItemDetail :ItemDetail
    {
        public double AdAmt { get; set; }


    }


}