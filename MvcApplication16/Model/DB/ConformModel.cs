using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Model.DB
{
    public class ConformModel
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public EnumButtons But { get; set; }

        public ConformModel() { }
        public ConformModel (int tID, string TName)
        {
            TestId = tID;
            TestName = TName;
        }

        public ConformModel(int tID, string TName,EnumButtons ButValue) : this (tID,TName)
        {
            But = ButValue;
            
        }
        
    }
}