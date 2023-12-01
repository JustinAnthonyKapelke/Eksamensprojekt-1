using Microsoft.Data.SqlClient.DataClassification;
using Nordlangelands_Tækkemand.Model;
using Nordlangelands_Tækkemand.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UnitTest
{


    [TestClass]
    public class ThatchingTest
    {
        // CreateDelegate-metoden bruges som mock for ThatchingRepository.CreateMaterial i testen
        public ThatchingMaterial InitializeCreateDelegate(int materialID, string name, string description, string imagepath, int storageStockCount, int typeID, int storageID)
        {
            return new ThatchingMaterial(materialID, name, description, imagepath, storageStockCount, typeID, storageID);
        }

        public ThatchingMaterial CreateDelegate(string name, string description, string imagepath, int storageStockCount, int typeID, int storageID)
        {
            return new ThatchingMaterial(name, description, imagepath, storageStockCount, typeID, storageID);
        }

        ThatchingRepository thatchingRepo;
        ThatchingViewModel tvm;
        int MaterialID;

        [TestMethod]
        public void CreateAndInsertMaterialTest()
        {
            // ARRANGE
            thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
            tvm = new ThatchingViewModel();

            // ACT

            //Create material without ID
            //Insert material in database
            //Material is given ID in databas
            thatchingRepo.CreateMaterialInDatabase("Tækkerør", "Tækkerør fra danmark","url", 1, 1);
            thatchingRepo.InitializeMaterials();

            thatchingRepo.ReadLastAddedMaterialFromDatabase();

            // Skal have et ID
            ThatchingMaterial createdMaterial = thatchingRepo.ReadLastAddedMaterialFromDatabase();


            // ASSERT
            Assert.IsNotNull(createdMaterial);
            Assert.AreEqual("Tækkerør", createdMaterial.MaterialName);
            Assert.AreEqual("Tækkerør fra danmark", createdMaterial.MaterialDescription);
            Assert.AreEqual("url", createdMaterial.MaterialImagePath);
            //Assert.AreEqual(1, createdMaterial.MaterialStockCount);
            Assert.AreEqual(1, createdMaterial.MaterialTypeID);
            Assert.AreEqual(1, createdMaterial.StorageID);
        }

        //[TestCleanup]
        //public void Cleanup()
        //{
        //    ThatchingMaterial createdMaterial = thatchingRepo.ReadLastAddedMaterialFromDatabase();

        //    MaterialID = createdMaterial.MaterialID;

        //    thatchingRepo.DeleteMaterialFromDatabase(MaterialID);
        //}
    }
}

