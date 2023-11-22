using Microsoft.Data.SqlClient.DataClassification;
using Nordlangelands_Tækkemand.Model;
using System.Collections.ObjectModel;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        BaseRepository<ThatchingMaterial>.CreateMaterialDelegate<ThatchingMaterial> thatchingDelegate = (materialID, materialName, materialDescription, materialStorageIndex, materialPrice) =>
        {
            // Opret en ny WoodMaterial med de givne parametre
            return new ThatchingMaterial(materialID, materialName, materialDescription, materialStorageIndex, materialPrice);
        };

        BaseRepository<VariousMaterial>.CreateMaterialDelegate<VariousMaterial> variousDelegate = (materialID, materialName, materialDescription, materialStorageIndex, materialPrice) =>
        {
            // Opret en ny WoodMaterial med de givne parametre
            return new VariousMaterial(materialID, materialName, materialDescription, materialStorageIndex, materialPrice);
        };

        BaseRepository<WoodMaterial>.CreateMaterialDelegate<WoodMaterial> woodDelegate = (materialID, materialName, materialDescription, materialStorageIndex, materialPrice) =>
        {
            // Opret en ny WoodMaterial med de givne parametre
            return new WoodMaterial(materialID, materialName, materialDescription, materialStorageIndex, materialPrice);
        };

        //Database Operation
        [TestMethod]
        public void InitializeTest()
        {
            //ARRANGE
            ThatchingRepository thatchingRepo = new ThatchingRepository(thatchingDelegate);
            VariousRepository variousRepo = new VariousRepository(variousDelegate);
            WoodRepository woodRepo = new WoodRepository(woodDelegate);

            //ACT
            int expectedNumberOfThatchingMaterials = 4;
            int actualNumberOfThatchingMaterials = thatchingRepo.GetAll().Count();

            int expectedNumberOfVariousMaterials = 4;
            int actualNumberOfVariousMaterials = variousRepo.GetAll().Count();

            int expectedNumberOfWoodMaterials = 4;
            int actualNumberOfWoodMaterials = woodRepo.GetAll().Count();   

            //ASSERT something
            Assert.AreEqual(expectedNumberOfThatchingMaterials, actualNumberOfThatchingMaterials);
            Assert.AreEqual(expectedNumberOfVariousMaterials, actualNumberOfVariousMaterials);
            Assert.AreEqual(expectedNumberOfWoodMaterials, actualNumberOfWoodMaterials);
        }
    }
}