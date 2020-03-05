using OnlineApprovals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineApprovals.Utilities
{
    public static class MatematicalCalculations
    {
        
        public static List< decimal> CalculateGrandTotal(ALLRequestDataDTO AllRequestData)
        {
            decimal TotalValue=0;

          foreach(var item in  AllRequestData.DemandedDrugs)
            {
                TotalValue = TotalValue +( item.UnitPrice * (item.DemandedQuantity/item.UnitQuantity)).Value;


            }



            decimal TotalAfterCoinsurance =Convert.ToDecimal( TotalValue * (AllRequestData.Request.CoInsurancePerc/100));

            List<decimal> returnedValues = new List<decimal>();
            returnedValues.Add(TotalValue);
            returnedValues.Add(TotalAfterCoinsurance);


            return returnedValues; 
        }



    }
}
