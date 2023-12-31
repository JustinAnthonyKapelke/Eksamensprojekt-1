﻿using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    //The class implements the IMaterial interface
    public class WoodMaterial : IMaterial
    {
        //Auto-Implemented Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public int MaterialStorageIndex { get; set; }
        public double MaterialPrice { get; set; }

        //Constructor
        public WoodMaterial(int materialID, string materialName, string materialDescription, int materialStorageIndex, double materialPrice)
        {
            MaterialID = materialID;
            MaterialName = materialName;
            MaterialDescription = materialDescription;
            MaterialStorageIndex = materialStorageIndex;
            MaterialPrice = materialPrice;
        }
    }
}
