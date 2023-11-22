using Microsoft.Data.SqlClient.DataClassification;
using Nordlangelands_Tækkemand.Model;
using System.Collections.ObjectModel;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {

        BaseRepository<WoodMaterial>.CreateMaterialDelegate<WoodMaterial> createDelegate = (materialID, materialName, materialDescription, materialStorageIndex, materialPrice) =>
        {
            // Opret en ny WoodMaterial med de givne parametre
            return new WoodMaterial(materialID, materialName, materialDescription, materialStorageIndex, materialPrice);
        };

        //Database Operation
        [TestMethod]
        public void InitializeTest()
        {
            //ARRANGE
            WoodRepository woodRepo = new WoodRepository(createDelegate);
            
            //ACT
            int expectedNumberOfMaterials = 4;
            int actualNumberOfMaterials = woodRepo.GetAll().Count();   

            //ASSERT something
            Assert.AreEqual(expectedNumberOfMaterials, actualNumberOfMaterials);
        }
    }
}