using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.IdentityModel.Tokens;
using Nordlangelands_T�kkemand.Model;
using Nordlangelands_T�kkemand.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UnitTest
{


    [TestClass]
    public class ThatchingTest
    {
        // CreateDelegate-metoden bruges som mock for ThatchingRepository.CreateMaterial i testen
        public ThatchingMaterial InitializeCreateDelegate(int materialID, string materialName, string materialDescription, string materialImagepath, int materialStorageStockCount, string materialType, int materialStorageID)
        {
            return new ThatchingMaterial(materialID, materialName, materialDescription, materialImagepath, materialStorageStockCount, materialType, materialStorageID);
        }

        public ThatchingMaterial CreateDelegate(string name, string description, string imagepath, int storageStockCount, string type, int storageID)
        {
            return new ThatchingMaterial(name, description, imagepath, storageStockCount, type, storageID);
        }

        ThatchingRepository thatchingRepo;
        ThatchingViewModel tvm;
        int MaterialID;

        int UpdateMaterialID = 38;
        int NewMaterialAmount = 4;
        
        [TestMethod]
        public void CreateAndInsertMaterialTest()
        {
            //ARRANGE
           thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
            tvm = new ThatchingViewModel();

           // ACT

           //Create material without ID
           //Insert material in database
           // Material is given ID in databas
            thatchingRepo.CreateMaterialInDatabase("T�kker�r", "T�kker�r fra danmark", 1, 1, 1);
            thatchingRepo.InitializeMaterials();

            thatchingRepo.ReadLastAddedMaterialFromDatabase();

            //Skal have et ID
            ThatchingMaterial createdMaterial = thatchingRepo.ReadLastAddedMaterialFromDatabase();


            //ASSERT
            Assert.IsNotNull(createdMaterial);
            Assert.AreEqual("T�kker�r", createdMaterial.MaterialName);
            Assert.AreEqual("T�kker�r fra danmark", createdMaterial.MaterialDescription);
            Assert.AreEqual("/Images/T�kke.png", createdMaterial.MaterialImagePath);
            Assert.AreEqual(1, createdMaterial.MaterialStockCount);
            Assert.AreEqual("T�kke", createdMaterial.MaterialType);
            Assert.AreEqual(1, createdMaterial.StorageID);
        }         




        [TestMethod]
        public void UpdateMaterialCountInDatabase()
        {
            // ARRANGE            
            thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
            thatchingRepo.InitializeMaterials(); // Initial material load

            // ACT

            ThatchingMaterial OldMaterialStockCount = thatchingRepo.ReadMaterialByIDFromDatabase(UpdateMaterialID);


            thatchingRepo.UpdateStockCountInDatabase(UpdateMaterialID, NewMaterialAmount);

            ThatchingMaterial NewMaterialStockCount = thatchingRepo.ReadMaterialByIDFromDatabase(UpdateMaterialID);

            // ASSERT
            Assert.AreEqual(OldMaterialStockCount.MaterialStockCount + NewMaterialAmount, NewMaterialStockCount.MaterialStockCount);


        }


        //[TestCleanup]
        //public void Cleanup()
        //{
        //    ThatchingMaterial createdMaterial = thatchingRepo.ReadLastAddedMaterialFromDatabase();

        //    MaterialID = createdMaterial.MaterialID;

        //    thatchingRepo.DeleteMaterialFromDatabase(MaterialID);

        //    //Cleanup UpdateMaterialCountInDatabase test

        //    //thatchingRepo.UpdateStockCountInDatabase(newMaterialID, NewMaterialAmount);
        //}





    }
}

