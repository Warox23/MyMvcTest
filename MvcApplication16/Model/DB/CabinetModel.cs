using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.DB
{
    /// <summary>
    /// Conteiter for test and personal user result
    /// </summary>
    public class CabinetModel
    {
        public List<TestModel> tests { get; set; }
        public List<ResoultSaveModel> RSM { get; set; }

        public CabinetModel( List<TestModel> test,  List<ResoultSaveModel> listResoults )
        {
            tests = test;
            RSM = listResoults;
        }

        public CabinetModel()
        {

        }

    }
}