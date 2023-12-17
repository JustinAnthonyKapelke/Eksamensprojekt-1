using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.IdentityModel.Tokens;
using Nordlangelands_T�kkemand.Model;
using Nordlangelands_T�kkemand.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        public ThatchingMaterial InitializeCreateDelegate(int materialID, string materialName, string materialDescription, string materialImagepath, int materialStorageStockCount, string materialType, int materialStorageID)
        {
            return new ThatchingMaterial(materialID, materialName, materialDescription, materialImagepath, materialStorageStockCount, materialType, materialStorageID);
        }

        public ThatchingMaterial CreateDelegate(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            return new ThatchingMaterial(materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);
        }

        //Variables
        ThatchingRepository thatchingRepo;
        ThatchingViewModel tvm;
        ThatchingMaterial oldMaterialStockCount;
        int updateMaterialID;
        int newMaterialAmount;

        //Initialize variables
        [TestInitialize]
        public void TestInitialize()
        {
            //ARRANGE
            thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
            tvm = new ThatchingViewModel();
            updateMaterialID = 72;
            newMaterialAmount = 4;
            oldMaterialStockCount = thatchingRepo.ReadMaterialByIDFromDatabase(updateMaterialID);
        }

        [TestMethod]
        public void TestReadMaterialByIDFromDatabase()
        {           
            //ACT
            var readMaterial = thatchingRepo.ReadMaterialByIDFromDatabase(updateMaterialID);                  

            //ASSERT
            Assert.IsNotNull(readMaterial);
            Assert.AreEqual("T�kker�r K", readMaterial.MaterialName);
            Assert.AreEqual("Korte t�kker�r fra Polen", readMaterial.MaterialDescription);
            Assert.AreEqual("/Images/Thatching.png", readMaterial.MaterialImagePath);
            Assert.AreEqual(758, readMaterial.MaterialStockCount);
            Assert.AreEqual("T�kke", readMaterial.MaterialType);
            Assert.AreEqual(1, readMaterial.StorageID);
        }

        [TestMethod]
        public void TestUpdateMaterialCountInDatabase()
        {
            //ACT           
            thatchingRepo.InitializeMaterials();
            thatchingRepo.UpdateStockCountInDatabase(updateMaterialID, newMaterialAmount);
            ThatchingMaterial NewMaterialStockCount = thatchingRepo.ReadMaterialByIDFromDatabase(updateMaterialID);

            //ASSERT
            Assert.AreEqual(oldMaterialStockCount.MaterialStockCount + newMaterialAmount, NewMaterialStockCount.MaterialStockCount);
        }

        [TestMethod]
        public void TestUpdateMaterialCountInDatabaseCleanup()        
        {      
            thatchingRepo.UpdateStockCountInDatabase(updateMaterialID, -newMaterialAmount);
            ThatchingMaterial newMaterialStockCount = thatchingRepo.ReadMaterialByIDFromDatabase(updateMaterialID);
        }

        [TestMethod]
        public void TestReadLogTextFromDatabase()        
        {
           //ACT
            var logText = thatchingRepo.ReadLogTextFromDatabase();

            //ASSERT
            Assert.AreEqual($"Redigeret: Materiale med ID: 72. Lagerantal �ndret fra {oldMaterialStockCount.MaterialStockCount + newMaterialAmount} til {oldMaterialStockCount.MaterialStockCount}", logText);
        }
    }
}
